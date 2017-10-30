// Type: ClrTest.Reflection.InlineSigInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineSigInstruction : ILInstruction
  {
    private ITokenResolver m_resolver;
    private int m_token;
    private byte[] m_signature;

    public byte[] Signature
    {
      get
      {
        if (this.m_signature == null)
          this.m_signature = this.m_resolver.AsSignature(this.m_token);
        return this.m_signature;
      }
    }

    public int Token
    {
      get
      {
        return this.m_token;
      }
    }

    internal InlineSigInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver)
      : base(offset, opCode)
    {
      this.m_resolver = resolver;
      this.m_token = token;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineSigInstruction(this);
    }
  }
}
