// Type: ClrTest.Reflection.DefaultFormatProvider
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Text;

namespace ClrTest.Reflection
{
  public class DefaultFormatProvider : IFormatProvider
  {
    public static DefaultFormatProvider Instance = new DefaultFormatProvider();

    static DefaultFormatProvider()
    {
    }

    private DefaultFormatProvider()
    {
    }

    public virtual string Int32ToHex(int int32)
    {
      return int32.ToString("X8");
    }

    public virtual string Int16ToHex(int int16)
    {
      return int16.ToString("X4");
    }

    public virtual string Int8ToHex(int int8)
    {
      return int8.ToString("X2");
    }

    public virtual string Argument(int ordinal)
    {
      return string.Format("V_{0}", (object) ordinal);
    }

    public virtual string Label(int offset)
    {
      return string.Format("IL_{0:x4}", (object) offset);
    }

    public virtual string MultipleLabels(int[] offsets)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int length = offsets.Length;
      for (int index = 0; index < length; ++index)
      {
        if (index == 0)
          stringBuilder.AppendFormat("(", new object[0]);
        else
          stringBuilder.AppendFormat(", ", new object[0]);
        stringBuilder.Append(this.Label(offsets[index]));
      }
      stringBuilder.AppendFormat(")", new object[0]);
      return ((object) stringBuilder).ToString();
    }

    public virtual string EscapedString(string str)
    {
      int length = str.Length;
      StringBuilder stringBuilder = new StringBuilder(length * 2);
      for (int index = 0; index < length; ++index)
      {
        char ch = str[index];
        switch (ch)
        {
          case '\t':
            stringBuilder.Append("\\t");
            break;
          case '\n':
            stringBuilder.Append("\\n");
            break;
          case '\r':
            stringBuilder.Append("\\r");
            break;
          case '"':
            stringBuilder.Append("\\\"");
            break;
          case '\\':
            stringBuilder.Append("\\");
            break;
          default:
            if ((int) ch < 32 || (int) ch >= (int) sbyte.MaxValue)
            {
              stringBuilder.AppendFormat("\\u{0:x4}", (object) (int) ch);
              break;
            }
            else
            {
              stringBuilder.Append(ch);
              break;
            }
        }
      }
      return "\"" + ((object) stringBuilder).ToString() + "\"";
    }

    public virtual string SigByteArrayToString(byte[] sig)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int length = sig.Length;
      for (int index = 0; index < length; ++index)
      {
        if (index == 0)
          stringBuilder.AppendFormat("SIG [", new object[0]);
        else
          stringBuilder.AppendFormat(" ", new object[0]);
        stringBuilder.Append(this.Int8ToHex((int) sig[index]));
      }
      stringBuilder.AppendFormat("]", new object[0]);
      return ((object) stringBuilder).ToString();
    }
  }
}
