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
        [XmlInclude(typeof(FXContainerRef))]
        public class FXContainer : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenFXContainers;

            [XmlAttribute]
            public byte UnkFlag1;
            [XmlAttribute]
            public byte UnkFlag2;
            [XmlAttribute]
            public byte UnkFlag3;

            public List<FXNode> FXNodes;
            public FXBehavior Behavior;
            public FXTemplate Template;

            public virtual bool ShouldSerializeUnkFlag1() => true;
            public virtual bool ShouldSerializeUnkFlag2() => true;
            public virtual bool ShouldSerializeUnkFlag3() => true;
            public virtual bool ShouldSerializeFXNodes() => true;
            public virtual bool ShouldSerializeBehavior() => true;
            public virtual bool ShouldSerializeTemplate() => true;

            internal override void ToXIDs(FXR1 fxr)
            {
                Behavior = fxr.ReferenceFXBehavior(Behavior);
                Template = fxr.ReferenceTemplate(Template);
                for (int i = 0; i < FXNodes.Count; i++)
                    FXNodes[i] = fxr.ReferenceFXNode(FXNodes[i]);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                Behavior = fxr.DereferenceFXBehavior(Behavior);
                Template = fxr.DereferenceTemplate(Template);
                for (int i = 0; i < FXNodes.Count; i++)
                    FXNodes[i] = fxr.DereferenceFXNode(FXNodes[i]);
            }

            public static int GetSize(bool isLong)
                => isLong ? 40 : 24;

            internal void Read(BinaryReaderEx br, FxrEnvironment env)
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
                FXNodes = new List<FXNode>(commandPool1TableCount);
                for (int i = 0; i < commandPool1TableCount; i++)
                {
                    //int next = br.ReadInt32();
                    //ast.Pool1List.Add(env.GetEffectPool1(br, next));
                    var paramPointer = env.GetFXNodePointer(br, br.Position);
                    FXNodes.Add(paramPointer.Node);
                    br.Position += FXNodePointer.GetSize(br.VarintLong);
                }
                br.StepOut();

                Behavior = env.GetFXBehavior(br, commandPool2Offset);
                Template = env.GetFXTemplate(br, commandPool3Offset);
            }

            internal void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                env.RegisterOffset(bw.Position, this);

                var nodePointers = new List<FXNodePointer>();

                for (int i = 0; i < FXNodes.Count; i++)
                {
                    nodePointers.Add(new FXNodePointer() { Node = FXNodes[i] });
                }

                if (Behavior != null)
                    Behavior.ContainingContainer = this;

                env.RegisterPointer(nodePointers);
                bw.WriteInt32(nodePointers.Count);
                bw.WriteInt32(nodePointers.Count);
                bw.WriteByte(UnkFlag1);
                bw.WriteByte(UnkFlag2);
                bw.WriteByte(UnkFlag3);
                bw.WriteByte(0);
                bw.WriteFXR1Garbage();
                env.RegisterPointer(Behavior);
                env.RegisterPointer(Template);
            }
        }

        public class FXContainerRef : FXContainer
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeUnkFlag1() => false;
            public override bool ShouldSerializeUnkFlag2() => false;
            public override bool ShouldSerializeUnkFlag3() => false;
            public override bool ShouldSerializeFXNodes() => false;
            public override bool ShouldSerializeBehavior() => false;
            public override bool ShouldSerializeTemplate() => false;

            public FXContainerRef(FXContainer refVal)
            {
                ReferenceXID = refVal?.XID;
            }
            public FXContainerRef() 
            {

            }
        }
    }
}
