// Type: ClrTest.Reflection.MethodBaseILProvider
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection;

namespace ClrTest.Reflection
{
  public class MethodBaseILProvider : IILProvider
  {
    private MethodBase m_method;
    private byte[] m_byteArray;

    public MethodBaseILProvider(MethodBase method)
    {
      this.m_method = method;
    }

    public byte[] GetByteArray()
    {
      if (this.m_byteArray == null)
      {
        MethodBody methodBody = this.m_method.GetMethodBody();
        this.m_byteArray = methodBody == null ? new byte[0] : methodBody.GetILAsByteArray();
      }
      return this.m_byteArray;
    }
  }
}
