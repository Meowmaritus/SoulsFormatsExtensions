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

            public List<ASTFunction> AstFunctions;
            public ASTAction AstAction;
            public ASTPool3 Pool3;

            public static int GetSize(bool isLong)
                => (isLong ? 24 : 12) + (isLong ? 16 : 12);

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
                AstFunctions = new List<ASTFunction>(commandPool1TableCount);
                for (int i = 0; i < commandPool1TableCount; i++)
                {
                    //int next = br.ReadInt32();
                    //ast.Pool1List.Add(env.GetASTPool1(br, next));
                    AstFunctions.Add(env.GetASTFunction(br, br.Position));
                    br.Position += ASTFunction.GetSize(br.VarintLong);
                }
                br.StepOut();

                AstAction = env.GetASTAction(br, commandPool2Offset);
                Pool3 = env.GetASTPool3(br, commandPool3Offset);
            }
        }
    }
}
