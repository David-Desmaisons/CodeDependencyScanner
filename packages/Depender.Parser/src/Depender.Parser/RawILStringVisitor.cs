// Type: ClrTest.Reflection.RawILStringVisitor
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

namespace ClrTest.Reflection
{
  public class RawILStringVisitor : ReadableILStringVisitor
  {
    public RawILStringVisitor(IILStringCollector collector)
      : this(collector, (IFormatProvider) DefaultFormatProvider.Instance)
    {
    }

    public RawILStringVisitor(IILStringCollector collector, IFormatProvider formatProvider)
      : base(collector, formatProvider)
    {
    }

    public override void VisitInlineBrTargetInstruction(InlineBrTargetInstruction inlineBrTargetInstruction)
    {
      this.collector.Process((ILInstruction) inlineBrTargetInstruction, this.formatProvider.Int32ToHex(inlineBrTargetInstruction.Delta));
    }

    public override void VisitInlineFieldInstruction(InlineFieldInstruction inlineFieldInstruction)
    {
      this.collector.Process((ILInstruction) inlineFieldInstruction, this.formatProvider.Int32ToHex(inlineFieldInstruction.Token));
    }

    public override void VisitInlineMethodInstruction(InlineMethodInstruction inlineMethodInstruction)
    {
      this.collector.Process((ILInstruction) inlineMethodInstruction, this.formatProvider.Int32ToHex(inlineMethodInstruction.Token));
    }

    public override void VisitInlineSigInstruction(InlineSigInstruction inlineSigInstruction)
    {
      this.collector.Process((ILInstruction) inlineSigInstruction, this.formatProvider.Int32ToHex(inlineSigInstruction.Token));
    }

    public override void VisitInlineStringInstruction(InlineStringInstruction inlineStringInstruction)
    {
      this.collector.Process((ILInstruction) inlineStringInstruction, this.formatProvider.Int32ToHex(inlineStringInstruction.Token));
    }

    public override void VisitInlineSwitchInstruction(InlineSwitchInstruction inlineSwitchInstruction)
    {
      this.collector.Process((ILInstruction) inlineSwitchInstruction, "...");
    }

    public override void VisitInlineTokInstruction(InlineTokInstruction inlineTokInstruction)
    {
      this.collector.Process((ILInstruction) inlineTokInstruction, this.formatProvider.Int32ToHex(inlineTokInstruction.Token));
    }

    public override void VisitInlineTypeInstruction(InlineTypeInstruction inlineTypeInstruction)
    {
      this.collector.Process((ILInstruction) inlineTypeInstruction, this.formatProvider.Int32ToHex(inlineTypeInstruction.Token));
    }

    public override void VisitInlineVarInstruction(InlineVarInstruction inlineVarInstruction)
    {
      this.collector.Process((ILInstruction) inlineVarInstruction, this.formatProvider.Int16ToHex((int) inlineVarInstruction.Ordinal));
    }

    public override void VisitShortInlineBrTargetInstruction(ShortInlineBrTargetInstruction shortInlineBrTargetInstruction)
    {
      this.collector.Process((ILInstruction) shortInlineBrTargetInstruction, this.formatProvider.Int8ToHex((int) shortInlineBrTargetInstruction.Delta));
    }

    public override void VisitShortInlineVarInstruction(ShortInlineVarInstruction shortInlineVarInstruction)
    {
      this.collector.Process((ILInstruction) shortInlineVarInstruction, this.formatProvider.Int8ToHex((int) shortInlineVarInstruction.Ordinal));
    }
  }
}
