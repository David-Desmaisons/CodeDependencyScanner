// Type: ClrTest.Reflection.MethodBodyInfo
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClrTest.Reflection
{
  [Serializable]
  public class MethodBodyInfo
  {
    private List<string> m_instructions = new List<string>();
    private List<ILInstruction> m_realInstructions = new List<ILInstruction>();
    private int m_methodId;
    private string m_typeName;
    private string m_methodToString;

    public int Identity
    {
      get
      {
        return this.m_methodId;
      }
      set
      {
        this.m_methodId = value;
      }
    }

    public string TypeName
    {
      get
      {
        return this.m_typeName;
      }
      set
      {
        this.m_typeName = value;
      }
    }

    public string MethodToString
    {
      get
      {
        return this.m_methodToString;
      }
      set
      {
        this.m_methodToString = value;
      }
    }

    public List<string> InstructionStrings
    {
      get
      {
        return this.m_instructions;
      }
    }

    public List<ILInstruction> Instructions
    {
      get
      {
        return this.m_realInstructions;
      }
    }

    private void AddInstruction(string inst, ILInstruction instruction)
    {
      this.m_instructions.Add(inst);
      this.m_realInstructions.Add(instruction);
    }

    public static MethodBodyInfo Create(MethodBase method)
    {
      MethodBodyInfo mbi = new MethodBodyInfo();
      mbi.Identity = method.GetHashCode();
      mbi.TypeName = method.GetType().Name;
      mbi.MethodToString = method.ToString();
      ReadableILStringVisitor readableIlStringVisitor = new ReadableILStringVisitor((IILStringCollector) new MethodBodyInfo.MethodBodyInfoBuilder(mbi), (IFormatProvider) DefaultFormatProvider.Instance);
      new ILReader(method).Accept((ILInstructionVisitor) readableIlStringVisitor);
      return mbi;
    }

    private class MethodBodyInfoBuilder : IILStringCollector
    {
      private MethodBodyInfo m_mbi;

      public MethodBodyInfoBuilder(MethodBodyInfo mbi)
      {
        this.m_mbi = mbi;
      }

      public void Process(ILInstruction instruction, string operandString)
      {
        this.m_mbi.AddInstruction(string.Format("IL_{0:x4}: {1,-10} {2}", (object) instruction.Offset, (object) instruction.OpCode.Name, (object) operandString), instruction);
      }
    }
  }
}
