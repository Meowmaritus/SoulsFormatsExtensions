using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoulsFormatsExtensions
{
    public partial class FXR1
    {
        [XmlInclude(typeof(FXParam1))]
        [XmlInclude(typeof(FXParam2))]
        [XmlInclude(typeof(FXParam3))]
        [XmlInclude(typeof(FXParam5))]
        [XmlInclude(typeof(FXParam6))]
        [XmlInclude(typeof(FXParam7))]
        [XmlInclude(typeof(FXParam9))]
        [XmlInclude(typeof(FXParam11))]
        [XmlInclude(typeof(FXParam12))]
        [XmlInclude(typeof(FXParam13))]
        [XmlInclude(typeof(FXParam14))]
        [XmlInclude(typeof(FXParam19))]
        [XmlInclude(typeof(FXParam20))]
        [XmlInclude(typeof(FXParam21))]
        [XmlInclude(typeof(FXParam22))]
        [XmlInclude(typeof(FXParam27))]
        [XmlInclude(typeof(FXParam28))]
        [XmlInclude(typeof(FXParam29))]
        [XmlInclude(typeof(InstantiateEffect))]
        [XmlInclude(typeof(CallAction))]
        [XmlInclude(typeof(FXParam41))]
        [XmlInclude(typeof(FXParam44))]
        [XmlInclude(typeof(FXParam45))]
        [XmlInclude(typeof(FXParam46))]
        [XmlInclude(typeof(FXParam47))]
        [XmlInclude(typeof(FXParam59))]
        [XmlInclude(typeof(FXParam60))]
        [XmlInclude(typeof(FXParam66))]
        [XmlInclude(typeof(PlaySound))]
        [XmlInclude(typeof(PlaySoundB))]
        [XmlInclude(typeof(FXParam70))]
        [XmlInclude(typeof(FXParam71))]
        [XmlInclude(typeof(FXParam79))]
        [XmlInclude(typeof(FXParam81))]
        [XmlInclude(typeof(FXParam85))]
        [XmlInclude(typeof(FXParam87))]
        [XmlInclude(typeof(FXParam89))]
        [XmlInclude(typeof(FXParam91))]
        [XmlInclude(typeof(FXParam95))]
        [XmlInclude(typeof(FXParam111))]
        [XmlInclude(typeof(FXParam112))]
        [XmlInclude(typeof(FXParam113))]
        [XmlInclude(typeof(FXParam114))]
        [XmlInclude(typeof(FXParam115))]
        [XmlInclude(typeof(FXParam120))]
        [XmlInclude(typeof(FXParam121))]
        [XmlInclude(typeof(FXParam122))]
        [XmlInclude(typeof(FXParam123))]
        [XmlInclude(typeof(FXParam124))]
        [XmlInclude(typeof(FXParam126))]
        [XmlInclude(typeof(FXParam127))]
        [XmlInclude(typeof(FXParam128))]
        [XmlInclude(typeof(FXParam129))]
        [XmlInclude(typeof(FXParam130))]
        [XmlInclude(typeof(FXParam131))]
        [XmlInclude(typeof(FXParam132))]
        [XmlInclude(typeof(CreateEffect))]
        [XmlInclude(typeof(CreateEffectLite))]
        [XmlInclude(typeof(FXParam136))]
        [XmlInclude(typeof(FXParam137))]
        [XmlInclude(typeof(FXParam138))]
        [XmlInclude(typeof(FXParam139))]
        [XmlInclude(typeof(FXParam140))]
        [XmlInclude(typeof(FXParamRef))]
        public abstract class FXParam : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenFXParams;

            //public readonly long ID;
            //internal FXParam(long id)
            //{
            //    ID = id;
            //}
            internal abstract void ReadInner(BinaryReaderEx br, FxrEnvironment env);
            internal abstract void WriteInner(BinaryWriterEx bw, FxrEnvironment env);

            internal override void ToXIDs(FXR1 fxr)
            {
                InnerToXIDs(fxr);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                InnerFromXIDs(fxr);
            }

            internal virtual void InnerToXIDs(FXR1 fxr)
            {

            }

            internal virtual void InnerFromXIDs(FXR1 fxr)
            {

            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                env.RegisterFXParamOffsetHere();
                WriteInner(bw, env);
            }

            public static FXParam GetProperFXParamType(BinaryReaderEx br, FxrEnvironment env)
            {
                long functionID = br.GetFXR1Varint(br.Position);
                FXParam func = null;
                switch (functionID)
                {
                    case 1: func = new FXParam1(); break;
                    case 2: func = new FXParam2(); break;
                    case 3: func = new FXParam3(); break;
                    case 5: func = new FXParam5(); break;
                    case 6: func = new FXParam6(); break;
                    case 7: func = new FXParam7(); break;
                    case 9: func = new FXParam9(); break;
                    case 11: func = new FXParam11(); break;
                    case 12: func = new FXParam12(); break;
                    case 13: func = new FXParam13(); break;
                    case 14: func = new FXParam14(); break;
                    case 19: func = new FXParam19(); break;
                    case 20: func = new FXParam20(); break;
                    case 21: func = new FXParam21(); break;
                    case 22: func = new FXParam22(); break;
                    case 27: func = new FXParam27(); break;
                    case 28: func = new FXParam28(); break;
                    case 29: func = new FXParam29(); break;
                    case 37: func = new InstantiateEffect(); break;
                    case 38: func = new CallAction(); break;
                    case 41: func = new FXParam41(); break;
                    case 44: func = new FXParam44(); break;
                    case 45: func = new FXParam45(); break;
                    case 46: func = new FXParam46(); break;
                    case 47: func = new FXParam47(); break;
                    case 59: func = new FXParam59(); break;
                    case 60: func = new FXParam60(); break;
                    case 66: func = new FXParam66(); break;
                    case 68: func = new PlaySound(); break;
                    case 69: func = new PlaySoundB(); break;
                    case 70: func = new FXParam70(); break;
                    case 71: func = new FXParam71(); break;
                    case 79: func = new FXParam79(); break;
                    case 81: func = new FXParam81(); break;
                    case 85: func = new FXParam85(); break;
                    case 87: func = new FXParam87(); break;
                    case 89: func = new FXParam89(); break;
                    case 91: func = new FXParam91(); break;
                    case 95: func = new FXParam95(); break;
                    case 111: func = new FXParam111(); break;
                    case 112: func = new FXParam112(); break;
                    case 113: func = new FXParam113(); break;
                    case 114: func = new FXParam114(); break;
                    case 115: func = new FXParam115(); break;
                    case 120: func = new FXParam120(); break;
                    case 121: func = new FXParam121(); break;
                    case 122: func = new FXParam122(); break;
                    case 123: func = new FXParam123(); break;
                    case 124: func = new FXParam124(); break;
                    case 126: func = new FXParam126(); break;
                    case 127: func = new FXParam127(); break;
                    case 128: func = new FXParam128(); break;
                    case 129: func = new FXParam129(); break;
                    case 130: func = new FXParam130(); break;
                    case 131: func = new FXParam131(); break;
                    case 132: func = new FXParam132(); break;
                    case 133: func = new CreateEffect(); break;
                    case 134: func = new CreateEffectLite(); break;
                    case 136: func = new FXParam136(); break;
                    case 137: func = new FXParam137(); break;
                    case 138: func = new FXParam138(); break;
                    case 139: func = new FXParam139(); break;
                    case 140: func = new FXParam140(); break;
                    default:
                        throw new NotImplementedException();
                }

                return func;
            }

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                ReadInner(br, env);
            }

            public class FXParamRef : FXParam
            {
                public string ReferenceXID;

                public FXParamRef(FXParam refVal)
                {
                    ReferenceXID = refVal?.XID;
                }

                public FXParamRef()
                {

                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    throw new InvalidOperationException("Cannot actually deserialize a FXParamRef.");
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    throw new InvalidOperationException("Cannot actually serialize a FXParamRef.");
                }
            }

            public class InstantiateEffect : FXParam
            {
                public int EffectID;
                public FXParamList ParamList;
                public int Unk;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    ParamList = fxr.ReferenceFXParamList(ParamList);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    ParamList = fxr.DereferenceFXParamList(ParamList);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(37);

                    EffectID = br.ReadFXR1Varint();
                    int astOffset = br.ReadFXR1Varint();
                    Unk = br.ReadInt32();

                    ParamList = env.GetEffect(br, astOffset);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(37);

                    bw.WriteFXR1Varint(EffectID);
                    env.RegisterPointer(ParamList);
                    bw.WriteInt32(Unk);
                }
            }

            public class CallAction : FXParam
            {
                public int ActionType;
                public FXParamList ParamList;
                public int Unk;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    ParamList = fxr.ReferenceFXParamList(ParamList);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    ParamList = fxr.DereferenceFXParamList(ParamList);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(38);

                    ActionType = br.ReadFXR1Varint();
                    int astOffset = br.ReadFXR1Varint();
                    Unk = br.ReadInt32();

                    ParamList = env.GetEffect(br, astOffset);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(38);

                    bw.WriteFXR1Varint(ActionType);
                    env.RegisterPointer(ParamList);
                    bw.WriteInt32(Unk);
                }
            }

            public class CreateEffect : FXParam
            {
                public int EffectID;
                public int Unk;
                public FXParamList ParamList1;
                public FXParamList ParamList2;
                public List<FlowNode> Nodes;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    for (int i = 0; i < Nodes.Count; i++)
                        Nodes[i] = fxr.ReferenceFlowNode(Nodes[i]);
                    ParamList1 = fxr.ReferenceFXParamList(ParamList1);
                    ParamList2 = fxr.ReferenceFXParamList(ParamList2);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    for (int i = 0; i < Nodes.Count; i++)
                        Nodes[i] = fxr.DereferenceFlowNode(Nodes[i]);
                    ParamList1 = fxr.DereferenceFXParamList(ParamList1);
                    ParamList2 = fxr.DereferenceFXParamList(ParamList2);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(133);

                    EffectID = br.ReadFXR1Varint();
                    for (int i = 0; i < 7; i++)
                        br.AssertFXR1Varint(0);
                    Unk = br.ReadFXR1Varint();
                    //throw new NotImplementedException();

                    ParamList1 = env.GetEffect(br, br.Position);
                    br.Position += FXParamList.GetSize(br.VarintLong);

                    ParamList2 = env.GetEffect(br, br.Position);
                    br.Position += FXParamList.GetSize(br.VarintLong);

                    int offsetToNodeList = br.ReadFXR1Varint();
                    int nodeCount = br.ReadFXR1Varint();
                    Nodes = new List<FlowNode>();
                    br.StepIn(offsetToNodeList);
                    for (int i = 0; i < nodeCount; i++)
                    {
                        Nodes.Add(env.GetFlowNode(br, br.Position));
                        br.Position += FlowNode.GetSize(br.VarintLong);
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(133);

                    bw.WriteFXR1Varint(EffectID);
                    for (int i = 0; i < 7; i++)
                        bw.WriteFXR1Varint(0);
                    bw.WriteFXR1Varint(Unk);
                    ParamList1.Write(bw, env);
                    ParamList2.Write(bw, env);
                    env.RegisterPointer(Nodes);
                    bw.WriteFXR1Varint(Nodes.Count);
                }
            }

            public class CreateEffectLite : FXParam
            {
                public int EffectID;
                public int Unk;
                public List<FXParam> Params;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    for (int i = 0; i < Params.Count; i++)
                        Params[i] = fxr.ReferenceFXParam(Params[i]);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    for (int i = 0; i < Params.Count; i++)
                        Params[i] = fxr.DereferenceFXParam(Params[i]);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(134);

                    EffectID = br.ReadFXR1Varint();
                    Unk = br.ReadFXR1Varint();
                    int offsetToParamOffsetList = br.ReadFXR1Varint();
                    int funcCount = br.ReadFXR1Varint();
                    Params = new List<FXParam>(funcCount);
                    br.StepIn(offsetToParamOffsetList);
                    for (int i = 0; i < funcCount; i++)
                    {
                        int nextParamOffset = br.ReadInt32();
                        var func = env.GetFXParam(br, nextParamOffset);
                        Params.Add(func);
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(134);

                    bw.WriteFXR1Varint(EffectID);
                    bw.WriteFXR1Varint(Unk);
                    env.RegisterPointer(Params);
                    bw.WriteFXR1Varint(Params.Count);
                }
            }

            public class FXParam1 : FXParam
            {
                [XmlAttribute]
                public int Unk;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(1);

                    Unk = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(1);

                    bw.WriteFXR1Varint(Unk);
                }
            }

            public class FXParam2 : FXParam
            {
                public List<int> IntList;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(2);

                    int listOffset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    IntList = new List<int>(listCount);

                    br.StepIn(listOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        IntList.Add(br.ReadInt32());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(2);

                    env.RegisterPointer(IntList);
                    bw.WriteFXR1Varint(IntList.Count);
                }
            }

            public class FXParam3 : FXParam
            {
                public List<int> IntList;
                public List<float> FloatList;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(3);

                    int floatListOffset = br.ReadFXR1Varint();
                    int intListOffset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList = new List<float>(listCount);
                    br.StepIn(floatListOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    IntList = new List<int>(listCount);
                    br.StepIn(intListOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        IntList.Add(br.ReadInt32());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(3);

                    throw new NotImplementedException();
                }
            }

            public class FXParam5 : FXParam
            {
                public List<int> IntList;
                public List<float> FloatList;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(5);

                    int floatListOffset = br.ReadFXR1Varint();
                    int intListOffset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList = new List<float>(listCount);
                    br.StepIn(floatListOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    IntList = new List<int>(listCount);
                    br.StepIn(intListOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        IntList.Add(br.ReadInt32());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(5);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList);
                    env.RegisterPointer(IntList);
                    bw.WriteFXR1Varint(FloatList.Count);
                }
            }

            public class FXParam6 : FXParam
            {
                public List<int> IntList;
                public List<float> FloatList;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(6);

                    int floatListOffset = br.ReadFXR1Varint();
                    int intListOffset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList = new List<float>(listCount);
                    br.StepIn(floatListOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    IntList = new List<int>(listCount);
                    br.StepIn(intListOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        IntList.Add(br.ReadInt32());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(6);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList);
                    env.RegisterPointer(IntList);
                    bw.WriteFXR1Varint(FloatList.Count);
                }
            }

            public class FXParam7 : FXParam
            {
                [XmlAttribute]
                public float Unk;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(7);

                    Unk = br.ReadFXR1Single();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(7);

                    bw.WriteFXR1Single(Unk);
                }
            }

            public class FXParam9 : FXParam
            {
                public List<int> IntList;
                public List<float> FloatList;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(9);

                    int floatListOffset = br.ReadFXR1Varint();
                    int intListOffset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList = new List<float>(listCount);
                    br.StepIn(floatListOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    IntList = new List<int>(listCount);
                    br.StepIn(intListOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        IntList.Add(br.ReadInt32());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(9);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList);
                    env.RegisterPointer(IntList);
                    bw.WriteFXR1Varint(FloatList.Count);
                }
            }

            public class FXParam11 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(11);

                    int floatList1Offset = br.ReadFXR1Varint();
                    int floatList2Offset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>(listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    FloatList2 = new List<float>(listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(11);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam12 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(12);

                    int floatList1Offset = br.ReadFXR1Varint();
                    int floatList2Offset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>(listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    FloatList2 = new List<float>(listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(12);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam13 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(13);

                    int floatList1Offset = br.ReadFXR1Varint();
                    int floatList2Offset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>(listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 3;

                    FloatList2 = new List<float>(listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(13);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam14 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(14);

                    int floatList1Offset = br.ReadFXR1Varint();
                    int floatList2Offset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>(listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 3;

                    FloatList2 = new List<float>(listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(14);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam19 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(19);

                    int floatList1Offset = br.ReadFXR1Varint();
                    int floatList2Offset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>(listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 4;

                    FloatList2 = new List<float>(listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(19);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam20 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(20);

                    int floatList1Offset = br.ReadFXR1Varint();
                    int floatList2Offset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>(listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 4;

                    FloatList2 = new List<float>(listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(20);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam21 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(21);

                    int floatList1Offset = br.ReadFXR1Varint();
                    int floatList2Offset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>(listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 12;

                    FloatList2 = new List<float>(listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(21);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam22 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(22);

                    int floatList1Offset = br.ReadFXR1Varint();
                    int floatList2Offset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>(listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 12;

                    FloatList2 = new List<float>(listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(22);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam27 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(27);

                    int floatList1Offset = br.ReadFXR1Varint();
                    int floatList2Offset = br.ReadFXR1Varint();
                    int listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>(listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 4;

                    FloatList2 = new List<float>(listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(27);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam28 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(28);

                    long floatList1Offset = br.ReadFXR1Varint();
                    long floatList2Offset = br.ReadFXR1Varint();
                    long listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>((int)listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 4;

                    FloatList2 = new List<float>((int)listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(28);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam29 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(29);

                    long floatList1Offset = br.ReadFXR1Varint();
                    long floatList2Offset = br.ReadFXR1Varint();
                    long listCount = br.ReadFXR1Varint();

                    FloatList1 = new List<float>((int)listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 12;

                    FloatList2 = new List<float>((int)listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(29);

                    //TODO: MULTILIST
                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                }
            }

            public class FXParam41 : FXParam
            {
                [XmlAttribute]
                public int Unk;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(41);

                    Unk = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(41);

                    bw.WriteFXR1Varint(Unk);
                }
            }

            public class FXParam44 : FXParam
            {
                [XmlAttribute]
                public short Unk1;
                [XmlAttribute]
                public short Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(44);

                    Unk1 = br.ReadInt16();
                    Unk2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(44);

                    bw.WriteInt16(Unk1);
                    bw.WriteInt16(Unk2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class FXParam45 : FXParam
            {
                public short Unk1;
                public short Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(45);

                    Unk1 = br.ReadInt16();
                    Unk2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(45);

                    bw.WriteInt16(Unk1);
                    bw.WriteInt16(Unk2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class FXParam46 : FXParam
            {
                [XmlAttribute]
                public short Unk1;
                [XmlAttribute]
                public short Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(46);

                    Unk1 = br.ReadInt16();
                    Unk2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(46);

                    bw.WriteInt16(Unk1);
                    bw.WriteInt16(Unk2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class FXParam47 : FXParam
            {
                [XmlAttribute]
                public short Unk1;
                [XmlAttribute]
                public short Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(47);

                    Unk1 = br.ReadInt16();
                    Unk2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(47);

                    bw.WriteInt16(Unk1);
                    bw.WriteInt16(Unk2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class FXParam59 : FXParam
            {
                [XmlAttribute]
                public int Unk;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(59);

                    Unk = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(59);

                    bw.WriteFXR1Varint(Unk);
                }
            }

            public class FXParam60 : FXParam
            {
                [XmlAttribute]
                public short Unk1;
                [XmlAttribute]
                public short Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(60);

                    Unk1 = br.ReadInt16();
                    Unk2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(60);

                    bw.WriteInt16(Unk1);
                    bw.WriteInt16(Unk2);

                    bw.WriteFXR1Garbage(); //????
                }
            }


            public class FXParam66 : FXParam
            {
                [XmlAttribute]
                public int Unk;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(66);

                    Unk = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(66);

                    bw.WriteFXR1Varint(Unk);
                }
            }

            public class PlaySound : FXParam
            {
                [XmlAttribute]
                public int SoundID;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(68);

                    SoundID = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(68);

                    bw.WriteFXR1Varint(SoundID);
                }
            }

            public class PlaySoundB : FXParam
            {
                [XmlAttribute]
                public int SoundID;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(69);

                    SoundID = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(69);

                    bw.WriteFXR1Varint(SoundID);
                }
            }

            public class FXParam70 : FXParam
            {
                [XmlAttribute]
                public float Unk;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(70);

                    Unk = br.ReadFXR1Single();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(70);

                    bw.WriteFXR1Single(Unk);
                }
            }


            public class FXParam71 : FXParam
            {
                [XmlAttribute]
                public short Unk1;
                [XmlAttribute]
                public short Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(71);

                    Unk1 = br.ReadInt16();
                    Unk2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(71);

                    bw.WriteInt16(Unk1);
                    bw.WriteInt16(Unk2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class FXParam79 : FXParam
            {
                [XmlAttribute]
                public int Unk1;
                [XmlAttribute]
                public int Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(79);

                    Unk1 = br.ReadFXR1Varint();
                    Unk2 = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(79);

                    bw.WriteFXR1Varint(Unk1);
                    bw.WriteFXR1Varint(Unk2);
                }
            }

            public class FXParam81 : FXParam
            {
                [XmlAttribute]
                public float Unk1;
                [XmlAttribute]
                public float Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(81);

                    Unk1 = br.ReadFXR1Single();
                    Unk2 = br.ReadFXR1Single();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(81);

                    bw.WriteFXR1Single(Unk1);
                    bw.WriteFXR1Single(Unk2);
                }
            }

            public class FXParam85 : FXParam
            {
                [XmlAttribute]
                public float Unk1;
                [XmlAttribute]
                public float Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(85);

                    Unk1 = br.ReadFXR1Single();
                    Unk2 = br.ReadFXR1Single();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(85);

                    bw.WriteFXR1Single(Unk1);
                    bw.WriteFXR1Single(Unk2);
                }
            }

            public class FXParam87 : FXParam
            {
                [XmlAttribute]
                public short Unk1;
                [XmlAttribute]
                public short Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(87);

                    Unk1 = br.ReadInt16();
                    Unk2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(87);

                    bw.WriteInt16(Unk1);
                    bw.WriteInt16(Unk2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class FXParam89 : FXParam
            {
                public List<int> IntList;
                public List<float> FloatList;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(89);

                    long floatListOffset = br.ReadFXR1Varint();
                    long intListOffset = br.ReadFXR1Varint();
                    long listCount = br.ReadFXR1Varint();
                    br.AssertFXR1Varint(1);
                    br.AssertFXR1Varint(0);

                    FloatList = new List<float>((int)listCount);
                    br.StepIn(floatListOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    IntList = new List<int>((int)listCount);
                    br.StepIn(intListOffset);
                    for (int i = 0; i < listCount; i++)
                    {
                        IntList.Add(br.ReadInt32());
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(89);

                    env.RegisterPointer(FloatList);
                    env.RegisterPointer(IntList);
                    bw.WriteFXR1Varint(FloatList.Count);
                    bw.WriteFXR1Varint(1);
                    bw.WriteFXR1Varint(0);
                }
            }

            public class FXParam91 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                public FXParam Param;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(91);

                    long floatList1Offset = br.ReadFXR1Varint();
                    long floatList2Offset = br.ReadFXR1Varint();
                    long listCount = br.ReadFXR1Varint();
                    br.AssertFXR1Varint(1);
                    long functionOffset = br.ReadFXR1Varint();

                    FloatList1 = new List<float>((int)listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 4;

                    FloatList2 = new List<float>((int)listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    Param = env.GetFXParam(br, functionOffset);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(91);

                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                    bw.WriteFXR1Varint(1);
                    env.RegisterPointer(Param);
                }
            }

            public class FXParam95 : FXParam
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                public FXParam Param;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    Param = fxr.ReferenceFXParam(Param);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    Param = fxr.DereferenceFXParam(Param);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(95);

                    long floatList1Offset = br.ReadFXR1Varint();
                    long floatList2Offset = br.ReadFXR1Varint();
                    long listCount = br.ReadFXR1Varint();
                    br.AssertFXR1Varint(1);
                    long functionOffset = br.ReadFXR1Varint();

                    FloatList1 = new List<float>((int)listCount);
                    br.StepIn(floatList1Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList1.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    listCount *= 4;

                    FloatList2 = new List<float>((int)listCount);
                    br.StepIn(floatList2Offset);
                    for (int i = 0; i < listCount; i++)
                    {
                        FloatList2.Add(br.ReadSingle());
                    }
                    br.StepOut();

                    Param = env.GetFXParam(br, functionOffset);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(95);

                    env.RegisterPointer(FloatList1);
                    env.RegisterPointer(FloatList2);
                    bw.WriteFXR1Varint(FloatList1.Count);
                    bw.WriteFXR1Varint(1);
                    env.RegisterPointer(Param);
                }
            }

            public class FXParam111 : FXParam
            {
                [XmlAttribute]
                public int Unk;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(111);

                    Unk = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(111);

                    bw.WriteFXR1Varint(Unk);
                }
            }

            public class FXParam112 : FXParam
            {
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(112);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(112);
                }
            }

            public class FXParam113 : FXParam
            {
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(113);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(113);
                }
            }

            public class FXParam114 : FXParam
            {
                [XmlAttribute]
                public short Unk1;
                [XmlAttribute]
                public short Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(114);

                    Unk1 = br.ReadInt16();
                    Unk2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(114);

                    bw.WriteInt16(Unk1);
                    bw.WriteInt16(Unk2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class FXParam115 : FXParam
            {
                [XmlAttribute]
                public short Unk1;
                [XmlAttribute]
                public short Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(115);

                    Unk1 = br.ReadInt16();
                    Unk2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(115);

                    bw.WriteInt16(Unk1);
                    bw.WriteInt16(Unk2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class FXParam120 : FXParam
            {
                public FXParam Param1;
                public FXParam Param2;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    Param1 = fxr.ReferenceFXParam(Param1);
                    Param2 = fxr.ReferenceFXParam(Param2);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    Param1 = fxr.DereferenceFXParam(Param1);
                    Param2 = fxr.DereferenceFXParam(Param2);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(120);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Param1 = env.GetFXParam(br, funcOffset1);
                    Param2 = env.GetFXParam(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(120);

                    env.RegisterPointer(Param1);
                    env.RegisterPointer(Param2);
                }
            }

            public class FXParam121 : FXParam
            {
                public FXParam Param1;
                public FXParam Param2;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    Param1 = fxr.ReferenceFXParam(Param1);
                    Param2 = fxr.ReferenceFXParam(Param2);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    Param1 = fxr.DereferenceFXParam(Param1);
                    Param2 = fxr.DereferenceFXParam(Param2);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(121);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Param1 = env.GetFXParam(br, funcOffset1);
                    Param2 = env.GetFXParam(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(121);

                    env.RegisterPointer(Param1);
                    env.RegisterPointer(Param2);
                }
            }

            public class FXParam122 : FXParam
            {
                public FXParam Param1;
                public FXParam Param2;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    Param1 = fxr.ReferenceFXParam(Param1);
                    Param2 = fxr.ReferenceFXParam(Param2);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    Param1 = fxr.DereferenceFXParam(Param1);
                    Param2 = fxr.DereferenceFXParam(Param2);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(122);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Param1 = env.GetFXParam(br, funcOffset1);
                    Param2 = env.GetFXParam(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(122);

                    env.RegisterPointer(Param1);
                    env.RegisterPointer(Param2);
                }
            }

            public class FXParam123 : FXParam
            {
                public FXParam Param1;
                public FXParam Param2;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    Param1 = fxr.ReferenceFXParam(Param1);
                    Param2 = fxr.ReferenceFXParam(Param2);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    Param1 = fxr.DereferenceFXParam(Param1);
                    Param2 = fxr.DereferenceFXParam(Param2);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(123);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Param1 = env.GetFXParam(br, funcOffset1);
                    Param2 = env.GetFXParam(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(123);

                    env.RegisterPointer(Param1);
                    env.RegisterPointer(Param2);
                }
            }

            public class FXParam124 : FXParam
            {
                public FXParam Param1;
                public FXParam Param2;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    Param1 = fxr.ReferenceFXParam(Param1);
                    Param2 = fxr.ReferenceFXParam(Param2);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    Param1 = fxr.DereferenceFXParam(Param1);
                    Param2 = fxr.DereferenceFXParam(Param2);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(124);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Param1 = env.GetFXParam(br, funcOffset1);
                    Param2 = env.GetFXParam(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(124);

                    env.RegisterPointer(Param1);
                    env.RegisterPointer(Param2);
                }
            }
            public class FXParam126 : FXParam
            {
                public FXParam Param1;
                public FXParam Param2;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    Param1 = fxr.ReferenceFXParam(Param1);
                    Param2 = fxr.ReferenceFXParam(Param2);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    Param1 = fxr.DereferenceFXParam(Param1);
                    Param2 = fxr.DereferenceFXParam(Param2);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(126);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Param1 = env.GetFXParam(br, funcOffset1);
                    Param2 = env.GetFXParam(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(126);

                    env.RegisterPointer(Param1);
                    env.RegisterPointer(Param2);
                }
            }

            public class FXParam127 : FXParam
            {
                public FXParam Param1;
                public FXParam Param2;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    Param1 = fxr.ReferenceFXParam(Param1);
                    Param2 = fxr.ReferenceFXParam(Param2);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    Param1 = fxr.DereferenceFXParam(Param1);
                    Param2 = fxr.DereferenceFXParam(Param2);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(127);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Param1 = env.GetFXParam(br, funcOffset1);
                    Param2 = env.GetFXParam(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(127);

                    env.RegisterPointer(Param1);
                    env.RegisterPointer(Param2);
                }
            }

            public class FXParam128 : FXParam
            {
                public FXParam Param;

                internal override void InnerToXIDs(FXR1 fxr)
                {
                    Param = fxr.ReferenceFXParam(Param);
                }

                internal override void InnerFromXIDs(FXR1 fxr)
                {
                    Param = fxr.DereferenceFXParam(Param);
                }

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(128);

                    int funcOffset = br.ReadFXR1Varint();

                    Param = env.GetFXParam(br, funcOffset);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(128);

                    env.RegisterPointer(Param);
                }
            }

            public class FXParam129 : FXParam
            {
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(129);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(129);
                }
            }

            public class FXParam130 : FXParam
            {
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(130);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(130);
                }
            }

            public class FXParam131 : FXParam
            {
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(131);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(131);
                }
            }

            public class FXParam132 : FXParam
            {
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(132);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(132);
                }
            }

            public class FXParam136 : FXParam
            {
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(136);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(136);
                }
            }

            public class FXParam137 : FXParam
            {
                [XmlAttribute]
                public int Unk1;
                [XmlAttribute]
                public int Unk2;
                [XmlAttribute]
                public int Unk3;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(137);

                    Unk1 = br.ReadFXR1Varint();
                    Unk2 = br.ReadFXR1Varint();
                    Unk3 = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(137);

                    bw.WriteFXR1Varint(Unk1);
                    bw.WriteFXR1Varint(Unk2);
                    bw.WriteFXR1Varint(Unk3);
                }
            }

            public class FXParam138 : FXParam
            {
                [XmlAttribute]
                public int Unk1;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(138);

                    Unk1 = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(138);

                    bw.WriteFXR1Varint(Unk1);
                }
            }

            public class FXParam139 : FXParam
            {
                [XmlAttribute]
                public int Unk1;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(139);

                    Unk1 = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(139);

                    bw.WriteFXR1Varint(Unk1);
                }
            }

            public class FXParam140 : FXParam
            {
                [XmlAttribute]
                public int Unk1;
                [XmlAttribute]
                public int Unk2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(140);

                    Unk1 = br.ReadFXR1Varint();
                    Unk2 = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(140);

                    bw.WriteFXR1Varint(Unk1);
                    bw.WriteFXR1Varint(Unk2);
                }
            }


        }
    }
}
