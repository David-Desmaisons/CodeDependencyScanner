// Type: ClrTest.Reflection.InlineTokInstruction
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System.Reflection;
using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public class InlineTokInstruction : ILInstruction
  {
    private ITokenResolver m_resolver;
    private int m_token;
    private MemberInfo m_member;

    public MemberInfo Member
    {
      get
      {
        if (this.m_member == null)
          this.m_member = this.m_resolver.AsMember(this.Token);
        return this.m_member;
      }
    }

    public int Token
    {
      get
      {
        return this.m_token;
      }
    }

    internal InlineTokInstruction(int offset, OpCode opCode, int token, ITokenResolver resolver)
      : base(offset, opCode)
    {
      this.m_resolver = resolver;
      this.m_token = token;
    }

    public override void Accept(ILInstructionVisitor vistor)
    {
      vistor.VisitInlineTokInstruction(this);
    }
  }
}
