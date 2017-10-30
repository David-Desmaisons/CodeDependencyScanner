// Type: ClrTest.Reflection.InlineI8Instruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineI8Instruction : ILInstruction
  {
    private long m_int64;

    public long Int64
    {
      get
      {
        return this.m_int64;
      }
    }

    internal InlineI8Instruction(int offset, OpCode opCode, long value)
      : base(offset, opCode)
    {
      this.m_int64 = value;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineI8Instruction(this);
    }
  }
}
