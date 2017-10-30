// Type: ClrTest.Reflection.ILInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public abstract class ILInstruction
  {
    protected int m_offset;
    protected OpCode m_opCode;

    public int Offset
    {
      get
      {
        return this.m_offset;
      }
    }

    public OpCode OpCode
    {
      get
      {
        return this.m_opCode;
      }
    }

    internal ILInstruction(int offset, OpCode opCode)
    {
      this.m_offset = offset;
      this.m_opCode = opCode;
    }

    public abstract void Accept(ILInstructionVisitor vistor);
  }
}
