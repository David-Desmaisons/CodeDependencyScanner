// Type: ClrTest.Reflection.InlineIInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineIInstruction : ILInstruction
  {
    private int m_int32;

    public int Int32
    {
      get
      {
        return this.m_int32;
      }
    }

    internal InlineIInstruction(int offset, OpCode opCode, int value)
      : base(offset, opCode)
    {
      this.m_int32 = value;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineIInstruction(this);
    }
  }
}
