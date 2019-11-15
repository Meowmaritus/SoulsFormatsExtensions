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
        [XmlInclude(typeof(ASTRef))]
        public class AST : XIDable
        {
            public override bool ShouldSerializeXID() => FXR1.FlattenASTs;

            [XmlAttribute]
            public byte UnkFlag1;
            [XmlAttribute]
            public byte UnkFlag2;
            [XmlAttribute]
            public byte UnkFlag3;

            [XmlElement(IsNullable = true)]
            public List<FunctionPointer> AstFunctions;

            [XmlElement(IsNullable = true)]
            public ASTPool2 AstPool2;

            [XmlElement(IsNullable = true)]
            public ASTPool3 AstPool3;

            public virtual bool ShouldSerializeUnkFlag1() => true;
            public virtual bool ShouldSerializeUnkFlag2() => true;
            public virtual bool ShouldSerializeUnkFlag3() => true;
            public virtual bool ShouldSerializeAstFunctions() => true;
            public virtual bool ShouldSerializeAstPool2() => true;
            public virtual bool ShouldSerializeAstPool3() => true;

            internal override void ToXIDs(FXR1 fxr)
            {
                AstPool2 = fxr.ReferenceASTPool2(AstPool2);
                AstPool3 = fxr.ReferenceASTPool3(AstPool3);
                for (int i = 0; i < AstFunctions.Count; i++)
                    AstFunctions[i] = fxr.ReferenceFunctionPointer(AstFunctions[i]);
            }

            internal override void FromXIDs(FXR1 fxr)
            {
                AstPool2 = fxr.DereferenceASTPool2(AstPool2);
                AstPool3 = fxr.DereferenceASTPool3(AstPool3);
                for (int i = 0; i < AstFunctions.Count; i++)
                    AstFunctions[i] = fxr.DereferenceFunctionPointer(AstFunctions[i]);
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
                AstFunctions = new List<FunctionPointer>(commandPool1TableCount);
                for (int i = 0; i < commandPool1TableCount; i++)
                {
                    //int next = br.ReadInt32();
                    //ast.Pool1List.Add(env.GetASTPool1(br, next));
                    AstFunctions.Add(env.GetASTFunction(br, br.Position));
                    br.Position += FunctionPointer.GetSize(br.VarintLong);
                }
                br.StepOut();

                AstPool2 = env.GetASTPool2(br, commandPool2Offset);
                AstPool3 = env.GetASTPool3(br, commandPool3Offset);
            }

            public void Write(BinaryWriterEx bw, FxrEnvironment env)
            {
                if (AstPool2 != null)
                    AstPool2.ParentAst = this;

                env.RegisterPointer(AstFunctions);
                bw.WriteInt32(AstFunctions.Count);
                bw.WriteInt32(AstFunctions.Count); //Not a typo
                bw.WriteByte(UnkFlag1);
                bw.WriteByte(UnkFlag2);
                bw.WriteByte(UnkFlag3);
                bw.WriteByte(0);
                bw.WriteFXR1Garbage();
                env.RegisterPointer(AstPool2);
                env.RegisterPointer(AstPool3);
            }
        }

        public class ASTRef : AST
        {
            [XmlAttribute]
            public string ReferenceXID;

            public override bool ShouldSerializeUnkFlag1() => false;
            public override bool ShouldSerializeUnkFlag2() => false;
            public override bool ShouldSerializeUnkFlag3() => false;
            public override bool ShouldSerializeAstFunctions() => false;
            public override bool ShouldSerializeAstPool2() => false;
            public override bool ShouldSerializeAstPool3() => false;

            public ASTRef(AST refVal)
            {
                ReferenceXID = refVal?.XID;
            }
            public ASTRef() 
            {

            }
        }
    }
}
