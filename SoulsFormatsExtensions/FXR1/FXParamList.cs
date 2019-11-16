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
        [XmlInclude(typeof(FXParamListRef))]
        public class FXParamList : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenFXParamLists;

            [XmlAttribute]
            public byte UnkFlag1;
            [XmlAttribute]
            public byte UnkFlag2;
            [XmlAttribute]
            public byte UnkFlag3;

            [XmlElement(IsNullable = true)]
            public List<FXParam> FXParams;

            [XmlElement(IsNullable = true)]
            public FXBehavior Behavior;

            [XmlElement(IsNullable = true)]
            public Template Template;

            public virtual bool ShouldSerializeUnkFlag1() => true;
            public virtual bool ShouldSerializeUnkFlag2() => true;
            public virtual bool ShouldSerializeUnkFlag3() => true;
            public virtual bool ShouldSerializeFXParams() => true;
            public virtual bool ShouldSerializeBehavior() => true;
            public virtual bool ShouldSerializeTemplate() => true;

            internal override void ToXIDs(FXR1 fxr)
            {
                Behavior = fxr.ReferenceFXBehavior(Behavior);
                Template = fxr.ReferenceTemplate(Template);
                for (int i = 0; i < FXParams.Count; i++)
                    FXParams[i] = fxr.ReferenceFXParam(FXParams[i]);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                Behavior = fxr.DereferenceFXBehavior(Behavior);
                Template = fxr.DereferenceTemplate(Template);
                for (int i = 0; i < FXParams.Count; i++)
                    FXParams[i] = fxr.DereferenceFXParam(FXParams[i]);
            }

            public static int GetSize(bool isLong)
                => isLong ? 40 : 24;

            public void Read(BinaryReaderEx br, FxrEnvironment env)
            {
                int commandPool1TableOffset = br.ReadFXR1Varint();
                int commandPool1TableCount = br.ReadInt32();
                br.AssertInt32(commandPool1TableCount);
                UnkFlag1 = br.ReadByte();
                UnkFlag2 = br.ReadByte();
                UnkFlag3 = br.ReadByte();
                br.AssertByte(0);

                br.AssertFXR1Garbage(); // ???

                int commandPool2Offset = br.ReadFXR1Varint();
                int commandPool3Offset = br.ReadFXR1Varint();

                br.StepIn(commandPool1TableOffset);
                FXParams = new List<FXParam>(commandPool1TableCount);
                for (int i = 0; i < commandPool1TableCount; i++)
                {
                    //int next = br.ReadInt32();
                    //ast.Pool1List.Add(env.GetEffectPool1(br, next));
                    var paramPointer = env.GetEffectFXParam(br, br.Position);
                    FXParams.Add(paramPointer.Param);
                    br.Position += FXParamPointer.GetSize(br.VarintLong);
                }
                br.StepOut();

                Behavior = env.GetBehavior(br, commandPool2Offset);
                Template = env.GetTemplate(br, commandPool3Offset);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                var paramPointers = new List<FXParamPointer>();

                for (int i = 0; i < FXParams.Count; i++)
                {
                    paramPointers.Add(new FXParamPointer() { Param = FXParams[i] });
                }

                if (Behavior != null)
                    Behavior.ContainingParamList = this;

                env.RegisterPointer(paramPointers);
                bw.WriteInt32(paramPointers.Count);
                bw.WriteInt32(paramPointers.Count); //Not a typo
                bw.WriteByte(UnkFlag1);
                bw.WriteByte(UnkFlag2);
                bw.WriteByte(UnkFlag3);
                bw.WriteByte(0);
                bw.WriteFXR1Garbage();
                env.RegisterPointer(Behavior);
                env.RegisterPointer(Template);
            }
        }

        public class FXParamListRef : FXParamList
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeUnkFlag1() => false;
            public override bool ShouldSerializeUnkFlag2() => false;
            public override bool ShouldSerializeUnkFlag3() => false;
            public override bool ShouldSerializeFXParams() => false;
            public override bool ShouldSerializeBehavior() => false;
            public override bool ShouldSerializeTemplate() => false;

            public FXParamListRef(FXParamList refVal)
            {
                ReferenceXID = refVal?.XID;
            }
            public FXParamListRef() 
            {

            }
        }
    }
}
