// Type: ClrTest.Reflection.InlineFieldInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection;
using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineFieldInstruction : ILInstruction
  {
    private ITokenResolver m_resolver;
    private int m_token;
    private FieldInfo m_field;

    public FieldInfo Field
    {
      get
      {
        if (this.m_field == null)
          this.m_field = this.m_resolver.AsField(this.m_token);
        return this.m_field;
      }
    }

    public int Token
    {
      get
      {
        return this.m_token;
      }
    }

    internal InlineFieldInstruction(ITokenResolver resolver, int offset, OpCode opCode, int token)
      : base(offset, opCode)
    {
      this.m_resolver = resolver;
      this.m_token = token;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineFieldInstruction(this);
    }
  }
}
