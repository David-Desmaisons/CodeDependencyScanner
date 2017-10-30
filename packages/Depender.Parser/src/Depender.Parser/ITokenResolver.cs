// Type: ClrTest.Reflection.ITokenResolver
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System;
using System.Reflection;

namespace ClrTest.Reflection
{
  public interface ITokenResolver
  {
    MethodBase AsMethod(int token);

    FieldInfo AsField(int token);

    Type AsType(int token);

    string AsString(int token);

    MemberInfo AsMember(int token);

    byte[] AsSignature(int token);
  }
}
