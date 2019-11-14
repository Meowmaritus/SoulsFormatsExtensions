using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsFormatsExtensions
{
    public partial class FXR1
    {
        public class AST
        {
            public byte UnkFlag1;
            public byte UnkFlag2;
            public byte UnkFlag3;

            public List<FunctionPointer> AstFunctions;
            public ASTPool2 AstPool2;
            public ASTPool3 AstPool3;

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
    }
}
