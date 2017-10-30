// Type: ClrTest.Reflection.ShortInlineBrTargetInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class ShortInlineBrTargetInstruction : ILInstruction
  {
    private sbyte m_delta;

    public sbyte Delta
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
        return this.m_offset + (int) this.m_delta + 1 + 1;
      }
    }

    internal ShortInlineBrTargetInstruction(int offset, OpCode opCode, sbyte delta)
      : base(offset, opCode)
    {
      this.m_delta = delta;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitShortInlineBrTargetInstruction(this);
    }
  }
}
