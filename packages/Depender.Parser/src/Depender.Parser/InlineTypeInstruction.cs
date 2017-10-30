// Type: ClrTest.Reflection.InlineTypeInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System;
using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineTypeInstruction : ILInstruction
  {
    private ITokenResolver m_resolver;
    private int m_token;
    private Type m_type;

    public Type Type
    {
      get
      {
        if (this.m_type == null)
          this.m_type = this.m_resolver.AsType(this.m_token);
        return this.m_type;
      }
    }

    public int Token
    {
      get
      {
        return this.m_token;
      }
    }

    internal InlineTypeInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver)
      : base(offset, opCode)
    {
      this.m_resolver = resolver;
      this.m_token = token;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineTypeInstruction(this);
    }
  }
}
