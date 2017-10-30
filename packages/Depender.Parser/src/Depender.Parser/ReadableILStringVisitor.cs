// Type: ClrTest.Reflection.ReadableILStringVisitor
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System;

namespace ClrTest.Reflection
{
  public class ReadableILStringVisitor : ILInstructionVisitor
  {
    protected IFormatProvider formatProvider;
    protected IILStringCollector collector;

    public ReadableILStringVisitor(IILStringCollector collector)
      : this(collector, (IFormatProvider) DefaultFormatProvider.Instance)
    {
    }

    public ReadableILStringVisitor(IILStringCollector collector, IFormatProvider formatProvider)
    {
      this.formatProvider = formatProvider;
      this.collector = collector;
    }

    public override void VisitInlineBrTargetInstruction(InlineBrTargetInstruction inlineBrTargetInstruction)
    {
      this.collector.Process((ILInstruction) inlineBrTargetInstruction, this.formatProvider.Label(inlineBrTargetInstruction.TargetOffset));
    }

    public override void VisitInlineFieldInstruction(InlineFieldInstruction inlineFieldInstruction)
    {
      string operandString;
      try
      {
        operandString = (string) (object) inlineFieldInstruction.Field + (object) "/" + (string) (object) inlineFieldInstruction.Field.DeclaringType;
      }
      catch (Exception ex)
      {
        operandString = "!" + ex.Message + "!";
      }
      this.collector.Process((ILInstruction) inlineFieldInstruction, operandString);
    }

    public override void VisitInlineIInstruction(InlineIInstruction inlineIInstruction)
    {
      this.collector.Process((ILInstruction) inlineIInstruction, inlineIInstruction.Int32.ToString());
    }

    public override void VisitInlineI8Instruction(InlineI8Instruction inlineI8Instruction)
    {
      this.collector.Process((ILInstruction) inlineI8Instruction, inlineI8Instruction.Int64.ToString());
    }

    public override void VisitInlineMethodInstruction(InlineMethodInstruction inlineMethodInstruction)
    {
      string operandString;
      try
      {
        operandString = (string) (object) inlineMethodInstruction.Method + (object) "/" + (string) (object) inlineMethodInstruction.Method.DeclaringType;
      }
      catch (Exception ex)
      {
        operandString = "!" + ex.Message + "!";
      }
      this.collector.Process((ILInstruction) inlineMethodInstruction, operandString);
    }

    public override void VisitInlineNoneInstruction(InlineNoneInstruction inlineNoneInstruction)
    {
      this.collector.Process((ILInstruction) inlineNoneInstruction, string.Empty);
    }

    public override void VisitInlineRInstruction(InlineRInstruction inlineRInstruction)
    {
      this.collector.Process((ILInstruction) inlineRInstruction, inlineRInstruction.Double.ToString());
    }

    public override void VisitInlineSigInstruction(InlineSigInstruction inlineSigInstruction)
    {
      this.collector.Process((ILInstruction) inlineSigInstruction, this.formatProvider.SigByteArrayToString(inlineSigInstruction.Signature));
    }

    public override void VisitInlineStringInstruction(InlineStringInstruction inlineStringInstruction)
    {
      this.collector.Process((ILInstruction) inlineStringInstruction, this.formatProvider.EscapedString(inlineStringInstruction.String));
    }

    public override void VisitInlineSwitchInstruction(InlineSwitchInstruction inlineSwitchInstruction)
    {
      this.collector.Process((ILInstruction) inlineSwitchInstruction, this.formatProvider.MultipleLabels(inlineSwitchInstruction.TargetOffsets));
    }

    public override void VisitInlineTokInstruction(InlineTokInstruction inlineTokInstruction)
    {
      string operandString;
      try
      {
        operandString = (string) (object) inlineTokInstruction.Member + (object) "/" + (string) (object) inlineTokInstruction.Member.DeclaringType;
      }
      catch (Exception ex)
      {
        operandString = "!" + ex.Message + "!";
      }
      this.collector.Process((ILInstruction) inlineTokInstruction, operandString);
    }

    public override void VisitInlineTypeInstruction(InlineTypeInstruction inlineTypeInstruction)
    {
      string operandString;
      try
      {
        operandString = inlineTypeInstruction.Type.Name;
      }
      catch (Exception ex)
      {
        operandString = "!" + ex.Message + "!";
      }
      this.collector.Process((ILInstruction) inlineTypeInstruction, operandString);
    }

    public override void VisitInlineVarInstruction(InlineVarInstruction inlineVarInstruction)
    {
      this.collector.Process((ILInstruction) inlineVarInstruction, this.formatProvider.Argument((int) inlineVarInstruction.Ordinal));
    }

    public override void VisitShortInlineBrTargetInstruction(ShortInlineBrTargetInstruction shortInlineBrTargetInstruction)
    {
      this.collector.Process((ILInstruction) shortInlineBrTargetInstruction, this.formatProvider.Label(shortInlineBrTargetInstruction.TargetOffset));
    }

    public override void VisitShortInlineIInstruction(ShortInlineIInstruction shortInlineIInstruction)
    {
      this.collector.Process((ILInstruction) shortInlineIInstruction, shortInlineIInstruction.Byte.ToString());
    }

    public override void VisitShortInlineRInstruction(ShortInlineRInstruction shortInlineRInstruction)
    {
      this.collector.Process((ILInstruction) shortInlineRInstruction, shortInlineRInstruction.Single.ToString());
    }

    public override void VisitShortInlineVarInstruction(ShortInlineVarInstruction shortInlineVarInstruction)
    {
      this.collector.Process((ILInstruction) shortInlineVarInstruction, this.formatProvider.Argument((int) shortInlineVarInstruction.Ordinal));
    }
  }
}
