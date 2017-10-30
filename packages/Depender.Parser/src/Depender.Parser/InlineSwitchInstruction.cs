// Type: ClrTest.Reflection.InlineSwitchInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineSwitchInstruction : ILInstruction
  {
    private int[] m_deltas;
    private int[] m_targetOffsets;

    public int[] Deltas
    {
      get
      {
        return (int[]) this.m_deltas.Clone();
      }
    }

    public int[] TargetOffsets
    {
      get
      {
        if (this.m_targetOffsets == null)
        {
          int length = this.m_deltas.Length;
          int num = 5 + 4 * length;
          this.m_targetOffsets = new int[length];
          for (int index = 0; index < length; ++index)
            this.m_targetOffsets[index] = this.m_offset + this.m_deltas[index] + num;
        }
        return this.m_targetOffsets;
      }
    }

    internal InlineSwitchInstruction(int offset, OpCode opCode, int[] deltas)
      : base(offset, opCode)
    {
      this.m_deltas = deltas;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineSwitchInstruction(this);
    }
  }
}
