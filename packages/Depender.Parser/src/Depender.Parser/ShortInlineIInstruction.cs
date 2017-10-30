// Type: ClrTest.Reflection.ShortInlineIInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class ShortInlineIInstruction : ILInstruction
  {
    private byte m_int8;

    public byte Byte
    {
      get
      {
        return this.m_int8;
      }
    }

    internal ShortInlineIInstruction(int offset, OpCode opCode, byte value)
      : base(offset, opCode)
    {
      this.m_int8 = value;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitShortInlineIInstruction(this);
    }
  }
}
