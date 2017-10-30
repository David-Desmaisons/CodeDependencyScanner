// Type: ClrTest.Reflection.IFormatProvider
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

namespace ClrTest.Reflection
{
  public interface IFormatProvider
  {
    string Int32ToHex(int int32);

    string Int16ToHex(int int16);

    string Int8ToHex(int int8);

    string Argument(int ordinal);

    string EscapedString(string str);

    string Label(int offset);

    string MultipleLabels(int[] offsets);

    string SigByteArrayToString(byte[] sig);
  }
}
