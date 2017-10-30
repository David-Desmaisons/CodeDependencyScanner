// Type: ClrTest.Reflection.ShortInlineRInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class ShortInlineRInstruction : ILInstruction
  {
    private float m_value;

    public float Single
    {
      get
      {
        return this.m_value;
      }
    }

    internal ShortInlineRInstruction(int offset, OpCode opCode, float value)
      : base(offset, opCode)
    {
      this.m_value = value;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitShortInlineRInstruction(this);
    }
  }
}
