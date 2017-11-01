// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ClrTest.Reflection;
using Gma.CodeVisuals.Generator.DependencyForceGraph.Do;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph
{
    public static class AnalyzerExtensions
    {
        public static IEnumerable<Type> Types(this Assembly assembly)
        {
            return
                assembly
                    .GetTypes()
                    .Where(t => !t.IsCompilerGenerated())
                    .Where(t => !t.IsNestedPrivate);
        }

        public static IEnumerable<Dependency> Implements(this Type type)
        {
            return
                Once(type.BaseType)
                    .Concat(type.GetInterfaces())
                    .Select(baseType => new Implements(type, GetUnderlyingGeneric(baseType)));
        }

        private static Type GetUnderlyingGeneric(Type type)
        {
            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }

        public static IEnumerable<Dependency> Calls(this Type type)
        {
            return
                type
                    .Methods()
                    .SelectMany(method => method.MethodCalls())
                    .Select(methodCall => new MethodCall(type, GetUnderlyingGeneric(methodCall.ReflectedType), methodCall))
                    .Where(call => call.Target != type);
        }

        public static IEnumerable<Dependency> Uses(this Type type)
        {
            IEnumerable<Type> fieldUses =
                type
                    .Fields()
                    .Select(field => field.FieldType)
                    .SelectMany(x => x.Unroll());

            IEnumerable<Type> methodUses =
                type
                    .Methods()
                    .SelectMany(method => method.UsedTypes());

            return
                fieldUses
                    .Concat(methodUses)
                    .Select(to => new Uses(type, GetUnderlyingGeneric(to)))
                    .Where(uses => uses.Target != type);
        }

        public static IEnumerable<Dependency> Contains(this Type type)
        {
            return
                type
                    .GetNestedTypes()
                    .Select(nested => new Contains(type, GetUnderlyingGeneric(nested)))
                    .Where(contains => contains.Target != type);
        }

        private static IEnumerable<MethodBase> MethodCalls(this MethodInfo methodInfo)
        {
            try
            {
                var mdi = MethodBodyInfo.Create(methodInfo);
                return mdi.Calls();
            }
            catch
            {
                return Enumerable.Empty<MethodBase>();
            }
        }

        private static IEnumerable<T> Once<T>(T element) where T : class
        {
            return
                element == null
                    ? Enumerable.Empty<T>()
                    : Enumerable.Repeat(element, 1);
        }

        private static IEnumerable<MethodInfo> Methods(this Type type)
        {
            const BindingFlags flags =
                BindingFlags.DeclaredOnly |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic;

            return type.GetMethods(flags);
        }

        private static IEnumerable<FieldInfo> Fields(this Type type)
        {
            const BindingFlags flags =
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic;

            return type.GetFields(flags);
        }

        private static IEnumerable<Type> Unroll(this Type type)
        {
            return type.IsConstructedGenericType
                ? Once(type.GetGenericTypeDefinition()).Concat(type.GenericTypeArguments)
                : Once(type);
        }

        private static IEnumerable<Type> UsedTypes(this MethodInfo method)
        {
            IEnumerable<Type> paramTypes = method
                .GetParameters()
                .SelectMany(param => param.ParameterType.Unroll());

            Type[] genericArguments = method
                .GetGenericArguments();

            return
                Once(method.ReturnType)
                    .SelectMany(returnType => returnType.Unroll())
                    .Concat(paramTypes)
                    .Concat(genericArguments);
        }

        private static IEnumerable<MethodBase> Calls(this MethodBodyInfo methodBody)
        {
            return methodBody
                .Instructions
                .Select(instr => instr as InlineMethodInstruction)           
                .Select(GetMethod)
                .Where(met => met != null)
                .Where(call => !call.IsPrivate);
        }

        private static MethodBase GetMethod(InlineMethodInstruction methodinstruction) {
            try {
                return methodinstruction?.Method;
            }
            catch {
                return null;
            }
        }

        private static bool IsCompilerGenerated(this Type type)
        {
            return type.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
        }
    }
}