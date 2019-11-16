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
        [XmlInclude(typeof(EffectRef))]
        public class Effect : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenEffects;

            [XmlAttribute]
            public byte UnkFlag1;
            [XmlAttribute]
            public byte UnkFlag2;
            [XmlAttribute]
            public byte UnkFlag3;

            [XmlElement(IsNullable = true)]
            public List<FunctionPointer> Functions;

            [XmlElement(IsNullable = true)]
            public Behavior Behavior;

            [XmlElement(IsNullable = true)]
            public Template Template;

            public virtual bool ShouldSerializeUnkFlag1() => true;
            public virtual bool ShouldSerializeUnkFlag2() => true;
            public virtual bool ShouldSerializeUnkFlag3() => true;
            public virtual bool ShouldSerializeFunctions() => true;
            public virtual bool ShouldSerializeBehavior() => true;
            public virtual bool ShouldSerializeTemplate() => true;

            internal override void ToXIDs(FXR1 fxr)
            {
                Behavior = fxr.ReferenceBehavior(Behavior);
                Template = fxr.ReferenceTemplate(Template);
                for (int i = 0; i < Functions.Count; i++)
                    Functions[i] = fxr.ReferenceFunctionPointer(Functions[i]);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                Behavior = fxr.DereferenceBehavior(Behavior);
                Template = fxr.DereferenceTemplate(Template);
                for (int i = 0; i < Functions.Count; i++)
                    Functions[i] = fxr.DereferenceFunctionPointer(Functions[i]);
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
                Functions = new List<FunctionPointer>(commandPool1TableCount);
                for (int i = 0; i < commandPool1TableCount; i++)
                {
                    //int next = br.ReadInt32();
                    //ast.Pool1List.Add(env.GetEffectPool1(br, next));
                    Functions.Add(env.GetEffectFunction(br, br.Position));
                    br.Position += FunctionPointer.GetSize(br.VarintLong);
                }
                br.StepOut();

                Behavior = env.GetBehavior(br, commandPool2Offset);
                Template = env.GetTemplate(br, commandPool3Offset);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                if (Behavior != null)
                    Behavior.ParentEffect = this;

                env.RegisterPointer(Functions);
                bw.WriteInt32(Functions.Count);
                bw.WriteInt32(Functions.Count); //Not a typo
                bw.WriteByte(UnkFlag1);
                bw.WriteByte(UnkFlag2);
                bw.WriteByte(UnkFlag3);
                bw.WriteByte(0);
                bw.WriteFXR1Garbage();
                env.RegisterPointer(Behavior);
                env.RegisterPointer(Template);
            }
        }

        public class EffectRef : Effect
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeUnkFlag1() => false;
            public override bool ShouldSerializeUnkFlag2() => false;
            public override bool ShouldSerializeUnkFlag3() => false;
            public override bool ShouldSerializeFunctions() => false;
            public override bool ShouldSerializeBehavior() => false;
            public override bool ShouldSerializeTemplate() => false;

            public EffectRef(Effect refVal)
            {
                ReferenceXID = refVal?.XID;
            }
            public EffectRef() 
            {

            }
        }
    }
}
