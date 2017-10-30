// Type: ClrTest.Reflection.InlineBrTargetInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineBrTargetInstruction : ILInstruction
  {
    private int m_delta;

    public int Delta
    {
      get
      {
        return this.m_delta;
      }
    }

    public int TargetOffset
    {
      get
      {
        return this.m_offset + this.m_delta + 1 + 4;
      }
    }

    internal InlineBrTargetInstruction(int offset, OpCode opCode, int delta)
      : base(offset, opCode)
    {
      this.m_delta = delta;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineBrTargetInstruction(this);
    }
  }
}
