// Type: ClrTest.Reflection.ILReader
// Assembly: Depender.Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7BFB306E-D2F0-493B-8C2F-5C3CEA04F731
// Assembly location: D:\temp\NSplit\Depender\Depender.Parser.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ClrTest.Reflection
{
  public sealed class ILReader : IEnumerable<ILInstruction>, IEnumerable
  {
    private static Type s_runtimeMethodInfoType = Type.GetType("System.Reflection.RuntimeMethodInfo");
    private static Type s_runtimeConstructorInfoType = Type.GetType("System.Reflection.RuntimeConstructorInfo");
    private static OpCode[] s_OneByteOpCodes = new OpCode[256];
    private static OpCode[] s_TwoByteOpCodes = new OpCode[256];
    private int m_position;
    private ITokenResolver m_resolver;
    private IILProvider m_ilProvider;
    private byte[] m_byteArray;

    static ILReader()
    {
      foreach (FieldInfo fieldInfo in typeof (OpCodes).GetFields(BindingFlags.Static | BindingFlags.Public))
      {
        OpCode opCode = (OpCode) fieldInfo.GetValue((object) null);
        ushort num = (ushort) opCode.Value;
        if ((int) num < 256)
          ILReader.s_OneByteOpCodes[(int) num] = opCode;
        else if (((int) num & 65280) == 65024)
          ILReader.s_TwoByteOpCodes[(int) num & (int) byte.MaxValue] = opCode;
      }
    }

    public ILReader(MethodBase method)
    {
      if (method == null)
        throw new ArgumentNullException("method");
      Type type = method.GetType();
      if (type != ILReader.s_runtimeMethodInfoType && type != ILReader.s_runtimeConstructorInfoType)
        throw new ArgumentException("method must be RuntimeMethodInfo or RuntimeConstructorInfo for this constructor.");
      this.m_ilProvider = (IILProvider) new MethodBaseILProvider(method);
      this.m_resolver = (ITokenResolver) new ModuleScopeTokenResolver(method);
      this.m_byteArray = this.m_ilProvider.GetByteArray();
      this.m_position = 0;
    }

    public ILReader(IILProvider ilProvider, ITokenResolver tokenResolver)
    {
      if (ilProvider == null)
        throw new ArgumentNullException("ilProvider");
      this.m_resolver = tokenResolver;
      this.m_ilProvider = ilProvider;
      this.m_byteArray = this.m_ilProvider.GetByteArray();
      this.m_position = 0;
    }

    public IEnumerator<ILInstruction> GetEnumerator()
    {
      while (this.m_position < this.m_byteArray.Length)
        yield return this.Next();
      this.m_position = 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    private ILInstruction Next()
    {
      int offset = this.m_position;
      OpCode opCode1 = OpCodes.Nop;
      byte num1 = this.ReadByte();
      OpCode opCode2;
      if ((int) num1 != 254)
      {
        opCode2 = ILReader.s_OneByteOpCodes[(int) num1];
      }
      else
      {
        byte num2 = this.ReadByte();
        opCode2 = ILReader.s_TwoByteOpCodes[(int) num2];
      }
      switch (opCode2.OperandType)
      {
        case OperandType.InlineBrTarget:
          int delta1 = this.ReadInt32();
          return (ILInstruction) new InlineBrTargetInstruction(offset, opCode2, delta1);
        case OperandType.InlineField:
          int token1 = this.ReadInt32();
          return (ILInstruction) new InlineFieldInstruction(this.m_resolver, offset, opCode2, token1);
        case OperandType.InlineI:
          int num3 = this.ReadInt32();
          return (ILInstruction) new InlineIInstruction(offset, opCode2, num3);
        case OperandType.InlineI8:
          long num4 = this.ReadInt64();
          return (ILInstruction) new InlineI8Instruction(offset, opCode2, num4);
        case OperandType.InlineMethod:
          int token2 = this.ReadInt32();
          return (ILInstruction) new InlineMethodInstruction(offset, opCode2, token2, this.m_resolver);
        case OperandType.InlineNone:
          return (ILInstruction) new InlineNoneInstruction(offset, opCode2);
        case OperandType.InlineR:
          double num5 = this.ReadDouble();
          return (ILInstruction) new InlineRInstruction(offset, opCode2, num5);
        case OperandType.InlineSig:
          int token3 = this.ReadInt32();
          return (ILInstruction) new InlineSigInstruction(offset, opCode2, token3, this.m_resolver);
        case OperandType.InlineString:
          int token4 = this.ReadInt32();
          return (ILInstruction) new InlineStringInstruction(offset, opCode2, token4, this.m_resolver);
        case OperandType.InlineSwitch:
          int length = this.ReadInt32();
          int[] deltas = new int[length];
          for (int index = 0; index < length; ++index)
            deltas[index] = this.ReadInt32();
          return (ILInstruction) new InlineSwitchInstruction(offset, opCode2, deltas);
        case OperandType.InlineTok:
          int token5 = this.ReadInt32();
          return (ILInstruction) new InlineTokInstruction(offset, opCode2, token5, this.m_resolver);
        case OperandType.InlineType:
          int token6 = this.ReadInt32();
          return (ILInstruction) new InlineTypeInstruction(offset, opCode2, token6, this.m_resolver);
        case OperandType.InlineVar:
          ushort ordinal1 = this.ReadUInt16();
          return (ILInstruction) new InlineVarInstruction(offset, opCode2, ordinal1);
        case OperandType.ShortInlineBrTarget:
          sbyte delta2 = this.ReadSByte();
          return (ILInstruction) new ShortInlineBrTargetInstruction(offset, opCode2, delta2);
        case OperandType.ShortInlineI:
          byte num6 = this.ReadByte();
          return (ILInstruction) new ShortInlineIInstruction(offset, opCode2, num6);
        case OperandType.ShortInlineR:
          float num7 = this.ReadSingle();
          return (ILInstruction) new ShortInlineRInstruction(offset, opCode2, num7);
        case OperandType.ShortInlineVar:
          byte ordinal2 = this.ReadByte();
          return (ILInstruction) new ShortInlineVarInstruction(offset, opCode2, ordinal2);
        default:
          throw new BadImageFormatException("unexpected OperandType " + (object) opCode2.OperandType);
      }
    }

    public void Accept(ILInstructionVisitor visitor)
    {
      if (visitor == null)
        throw new ArgumentNullException("argument 'visitor' can not be null");
      foreach (ILInstruction ilInstruction in this)
        ilInstruction.Accept(visitor);
    }

    private byte ReadByte()
    {
      return this.m_byteArray[this.m_position++];
    }

    private sbyte ReadSByte()
    {
      return (sbyte) this.ReadByte();
    }

    private ushort ReadUInt16()
    {
      int startIndex = this.m_position;
      this.m_position += 2;
      return BitConverter.ToUInt16(this.m_byteArray, startIndex);
    }

    private uint ReadUInt32()
    {
      int startIndex = this.m_position;
      this.m_position += 4;
      return BitConverter.ToUInt32(this.m_byteArray, startIndex);
    }

    private ulong ReadUInt64()
    {
      int startIndex = this.m_position;
      this.m_position += 8;
      return BitConverter.ToUInt64(this.m_byteArray, startIndex);
    }

    private int ReadInt32()
    {
      int startIndex = this.m_position;
      this.m_position += 4;
      return BitConverter.ToInt32(this.m_byteArray, startIndex);
    }

    private long ReadInt64()
    {
      int startIndex = this.m_position;
      this.m_position += 8;
      return BitConverter.ToInt64(this.m_byteArray, startIndex);
    }

    private float ReadSingle()
    {
      int startIndex = this.m_position;
      this.m_position += 4;
      return BitConverter.ToSingle(this.m_byteArray, startIndex);
    }

    private double ReadDouble()
    {
      int startIndex = this.m_position;
      this.m_position += 8;
      return BitConverter.ToDouble(this.m_byteArray, startIndex);
    }
  }
}
