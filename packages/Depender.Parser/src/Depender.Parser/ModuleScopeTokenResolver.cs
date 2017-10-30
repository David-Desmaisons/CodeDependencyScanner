// Type: ClrTest.Reflection.ModuleScopeTokenResolver
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System;
using System.Reflection;

namespace ClrTest.Reflection
{
  public class ModuleScopeTokenResolver : ITokenResolver
  {
    private Module m_module;
    private MethodBase m_enclosingMethod;
    private Type[] m_methodContext;
    private Type[] m_typeContext;

    public ModuleScopeTokenResolver(MethodBase method)
    {
      this.m_enclosingMethod = method;
      this.m_module = method.Module;
      this.m_methodContext = method is ConstructorInfo ? (Type[]) null : method.GetGenericArguments();
      this.m_typeContext = method.DeclaringType == null ? (Type[]) null : method.DeclaringType.GetGenericArguments();
    }

    public MethodBase AsMethod(int token)
    {
      return this.m_module.ResolveMethod(token, this.m_typeContext, this.m_methodContext);
    }

    public FieldInfo AsField(int token)
    {
      return this.m_module.ResolveField(token, this.m_typeContext, this.m_methodContext);
    }

    public Type AsType(int token)
    {
      return this.m_module.ResolveType(token, this.m_typeContext, this.m_methodContext);
    }

    public MemberInfo AsMember(int token)
    {
      return this.m_module.ResolveMember(token, this.m_typeContext, this.m_methodContext);
    }

    public string AsString(int token)
    {
      return this.m_module.ResolveString(token);
    }

    public byte[] AsSignature(int token)
    {
      return this.m_module.ResolveSignature(token);
    }
  }
}
