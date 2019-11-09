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

            public List<ASTPool1> Pool1List;
            public ASTPool2 Pool2;
            public ASTPool3 Pool3;

            public static int GetSize(bool isLong)
                => (isLong ? 24 : 12) + (isLong ? 16 : 12);

            public static AST Read(BinaryReaderEx br, FxrEnvironment env)
            {
                var ast = new AST();

                int commandPool1TableOffset = br.ReadFXR1Varint();
                int commandPool1TableCount = br.ReadInt32();
                br.AssertInt32(commandPool1TableCount);
                ast.UnkFlag1 = br.ReadByte();
                ast.UnkFlag2 = br.ReadByte();
                ast.UnkFlag3 = br.ReadByte();
                br.AssertByte(0);

                br.AssertFXR1Garbage(); // ???

                int commandPool2Offset = br.ReadFXR1Varint();
                int commandPool3Offset = br.ReadFXR1Varint();

                br.StepIn(commandPool1TableOffset);
                ast.Pool1List = new List<ASTPool1>(commandPool1TableCount);
                for (int i = 0; i < commandPool1TableCount; i++)
                {
                    int next = br.ReadInt32();
                    ast.Pool1List.Add(env.GetASTPool1(br, next));
                }
                br.StepOut();

                ast.Pool2 = env.GetASTPool2(br, commandPool2Offset);
                ast.Pool3 = env.GetASTPool3(br, commandPool3Offset);

                return ast;
            }
        }
    }
}
