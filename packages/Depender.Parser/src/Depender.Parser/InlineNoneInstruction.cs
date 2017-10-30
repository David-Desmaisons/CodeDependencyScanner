// Type: ClrTest.Reflection.InlineNoneInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineNoneInstruction : ILInstruction
  {
    internal InlineNoneInstruction(int offset, OpCode opCode)
      : base(offset, opCode)
    {
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineNoneInstruction(this);
    }
  }
}
