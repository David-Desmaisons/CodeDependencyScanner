// Type: ClrTest.Reflection.InlineStringInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineStringInstruction : ILInstruction
  {
    private ITokenResolver m_resolver;
    private int m_token;
    private string m_string;

    public string String
    {
      get
      {
        if (this.m_string == null)
          this.m_string = this.m_resolver.AsString(this.Token);
        return this.m_string;
      }
    }

    public int Token
    {
      get
      {
        return this.m_token;
      }
    }

    internal InlineStringInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver)
      : base(offset, opCode)
    {
      this.m_resolver = resolver;
      this.m_token = token;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineStringInstruction(this);
    }
  }
}
