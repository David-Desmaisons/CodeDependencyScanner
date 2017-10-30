// Type: ClrTest.Reflection.InlineVarInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineVarInstruction : ILInstruction
  {
    private ushort m_ordinal;

    public ushort Ordinal
    {
      get
      {
        return this.m_ordinal;
      }
    }

    internal InlineVarInstruction(int offset, OpCode opCode, ushort ordinal)
      : base(offset, opCode)
    {
      this.m_ordinal = ordinal;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineVarInstruction(this);
    }
  }
}
