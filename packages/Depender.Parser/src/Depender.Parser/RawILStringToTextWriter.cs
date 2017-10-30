// Type: ClrTest.Reflection.RawILStringToTextWriter
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.IO;

namespace ClrTest.Reflection
{
  public class RawILStringToTextWriter : ReadableILStringToTextWriter
  {
    public RawILStringToTextWriter(TextWriter writer)
      : base(writer)
    {
    }

    public override void Process(ILInstruction ilInstruction, string operandString)
    {
      this.writer.WriteLine("IL_{0:x4}: {1,-4}| {2, -8}", (object) ilInstruction.Offset, (object) ilInstruction.OpCode.Value.ToString("x2"), (object) operandString);
    }
  }
}
