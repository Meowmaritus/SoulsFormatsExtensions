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
        public abstract class Function
        {
            //public readonly long ID;
            //internal Function(long id)
            //{
            //    ID = id;
            //}
            internal abstract void ReadInner(BinaryReaderEx br, FxrEnvironment env);
            internal abstract void WriteInner(BinaryWriterEx bw, FxrEnvironment env);

            public static Function Read(BinaryReaderEx br, FxrEnvironment env)
            {
                long functionID = br.GetFXR1Varint(br.Position);
                Function func = null;
                switch (functionID)
                {
                    case 1: func = new Function1(); break;
                    case 2: func = new Function2(); break;
                    case 3: func = new Function3(); break;
                    case 5: func = new Function5(); break;
                    case 6: func = new Function6(); break;
                    case 7: func = new Function7(); break;
                    case 9: func = new Function9(); break;
                    case 11: func = new Function11(); break;
                    case 12: func = new Function12(); break;
                    case 13: func = new Function13(); break;
                    case 14: func = new Function14(); break;
                    case 19: func = new Function19(); break;
                    case 20: func = new Function20(); break;
                    case 21: func = new Function21(); break;
                    case 22: func = new Function22(); break;
                    case 27: func = new Function27(); break;
                    case 28: func = new Function28(); break;
                    case 29: func = new Function29(); break;
                    case 37: func = new Function37(); break;
                    case 38: func = new Function38(); break;
                    case 41: func = new Function41(); break;
                    case 44: func = new Function44(); break;
                    case 45: func = new Function45(); break;
                    case 46: func = new Function46(); break;
                    case 47: func = new Function47(); break;
                    case 59: func = new Function59(); break;
                    case 60: func = new Function60(); break;
                    case 66: func = new Function66(); break;
                    case 68: func = new Function68(); break;
                    case 69: func = new Function69(); break;
                    case 70: func = new Function70(); break;
                    case 71: func = new Function71(); break;
                    case 79: func = new Function79(); break;
                    case 81: func = new Function81(); break;
                    case 85: func = new Function85(); break;
                    case 87: func = new Function87(); break;
                    case 89: func = new Function89(); break;
                    case 91: func = new Function91(); break;
                    case 111: func = new Function111(); break;
                    case 112: func = new Function112(); break;
                    case 113: func = new Function113(); break;
                    case 114: func = new Function114(); break;
                    case 115: func = new Function115(); break;
                    case 120: func = new Function120(); break;
                    case 121: func = new Function121(); break;
                    case 122: func = new Function122(); break;
                    case 123: func = new Function123(); break;
                    case 124: func = new Function124(); break;
                    case 126: func = new Function126(); break;
                    case 127: func = new Function127(); break;
                    case 128: func = new Function128(); break;
                    case 129: func = new Function129(); break;
                    case 130: func = new Function130(); break;
                    case 131: func = new Function131(); break;
                    case 132: func = new Function132(); break;
                    case 133: func = new Function133(); break;
                    case 134: func = new Function134(); break;
                    case 136: func = new Function136(); break;
                    case 137: func = new Function137(); break;
                    case 138: func = new Function138(); break;
                    case 139: func = new Function139(); break;
                    case 140: func = new Function140(); break;
                    default: throw new NotImplementedException();
                }

                func.ReadInner(br, env);
                return func;
            }

            public class Function1 : Function
            {
                public int Unknown;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(1);

                    Unknown = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(1);

                    bw.WriteFXR1Varint(Unknown);
                }
            }

            public class Function2 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function3 : Function
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

            public class Function5 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function6 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function7 : Function
            {
                public float Unknown;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(7);

                    Unknown = br.ReadFXR1Single();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(7);

                    bw.WriteFXR1Single(Unknown);
                }
            }

            public class Function9 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function11 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function12 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function13 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function14 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function19 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function20 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function21 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function22 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function27 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function28 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function29 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function37 : Function
            {
                public int TemplateFXRID;
                public AST Ast;
                public int Unknown;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(37);

                    TemplateFXRID = br.ReadFXR1Varint();
                    int astOffset = br.ReadFXR1Varint();
                    Unknown = br.ReadInt32();

                    Ast = env.GetAST(br, astOffset);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(37);

                    throw new NotImplementedException();
                }
            }

            public class Function38 : Function
            {
                public int SubType;
                public AST Ast;
                public int Unknown;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(38);

                    SubType = br.ReadFXR1Varint();
                    int astOffset = br.ReadFXR1Varint();
                    Unknown = br.ReadInt32();

                    Ast = env.GetAST(br, astOffset);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(38);

                    throw new NotImplementedException();
                }
            }

            public class Function41 : Function
            {
                public int Unknown;
                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(41);

                    Unknown = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(41);

                    bw.WriteFXR1Varint(Unknown);
                }
            }

            public class Function44 : Function
            {
                public short Unknown1;
                public short Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(44);

                    Unknown1 = br.ReadInt16();
                    Unknown2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(44);

                    bw.WriteInt16(Unknown1);
                    bw.WriteInt16(Unknown2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class Function45 : Function
            {
                public short Unknown1;
                public short Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(45);

                    Unknown1 = br.ReadInt16();
                    Unknown2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(45);

                    bw.WriteInt16(Unknown1);
                    bw.WriteInt16(Unknown2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class Function46 : Function
            {
                public short Unknown1;
                public short Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(46);

                    Unknown1 = br.ReadInt16();
                    Unknown2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(46);

                    bw.WriteInt16(Unknown1);
                    bw.WriteInt16(Unknown2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class Function47 : Function
            {
                public short Unknown1;
                public short Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(47);

                    Unknown1 = br.ReadInt16();
                    Unknown2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(47);

                    bw.WriteInt16(Unknown1);
                    bw.WriteInt16(Unknown2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class Function59 : Function
            {
                public int Unknown;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(59);

                    Unknown = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(59);

                    bw.WriteFXR1Varint(Unknown);
                }
            }

            public class Function60 : Function
            {
                public short Unknown1;
                public short Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(60);

                    Unknown1 = br.ReadInt16();
                    Unknown2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(60);

                    bw.WriteInt16(Unknown1);
                    bw.WriteInt16(Unknown2);

                    bw.WriteFXR1Garbage(); //????
                }
            }


            public class Function66 : Function
            {
                public int Unknown;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(66);

                    Unknown = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(66);

                    bw.WriteFXR1Varint(Unknown);
                }
            }

            public class Function68 : Function
            {
                public int Unknown;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(68);

                    Unknown = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(68);

                    bw.WriteFXR1Varint(Unknown);
                }
            }

            public class Function69 : Function
            {
                public int Unknown;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(69);

                    Unknown = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(69);

                    bw.WriteFXR1Varint(Unknown);
                }
            }

            public class Function70 : Function
            {
                public float Unknown;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(70);

                    Unknown = br.ReadFXR1Single();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(70);

                    bw.WriteFXR1Single(Unknown);
                }
            }


            public class Function71 : Function
            {
                public short Unknown1;
                public short Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(71);

                    Unknown1 = br.ReadInt16();
                    Unknown2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(71);

                    bw.WriteInt16(Unknown1);
                    bw.WriteInt16(Unknown2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class Function79 : Function
            {
                public int Unknown1;
                public int Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(79);

                    Unknown1 = br.ReadFXR1Varint();
                    Unknown2 = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(79);

                    bw.WriteFXR1Varint(Unknown1);
                    bw.WriteFXR1Varint(Unknown2);
                }
            }

            public class Function81 : Function
            {
                public float Unknown1;
                public float Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(81);

                    Unknown1 = br.ReadFXR1Single();
                    Unknown2 = br.ReadFXR1Single();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(81);

                    bw.WriteFXR1Single(Unknown1);
                    bw.WriteFXR1Single(Unknown2);
                }
            }

            public class Function85 : Function
            {
                public float Unknown1;
                public float Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(85);

                    Unknown1 = br.ReadFXR1Single();
                    Unknown2 = br.ReadFXR1Single();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(85);

                    bw.WriteFXR1Single(Unknown1);
                    bw.WriteFXR1Single(Unknown2);
                }
            }

            public class Function87 : Function
            {
                public short Unknown1;
                public short Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(87);

                    Unknown1 = br.ReadInt16();
                    Unknown2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(87);

                    bw.WriteInt16(Unknown1);
                    bw.WriteInt16(Unknown2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class Function89 : Function
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

                    throw new NotImplementedException();
                }
            }

            public class Function91 : Function
            {
                public List<float> FloatList1;
                public List<float> FloatList2;
                public Function Func;
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

                    Func = env.GetFunction(br, functionOffset);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(91);

                    throw new NotImplementedException();
                }
            }

            public class Function111 : Function
            {
                public int Unknown;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(111);

                    Unknown = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(111);

                    bw.WriteFXR1Varint(Unknown);
                }
            }

            public class Function112 : Function
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

            public class Function113 : Function
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

            public class Function114 : Function
            {
                public short Unknown1;
                public short Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(114);

                    Unknown1 = br.ReadInt16();
                    Unknown2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(114);

                    bw.WriteInt16(Unknown1);
                    bw.WriteInt16(Unknown2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class Function115 : Function
            {
                public short Unknown1;
                public short Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(115);

                    Unknown1 = br.ReadInt16();
                    Unknown2 = br.ReadInt16();

                    br.AssertFXR1Garbage(); //????
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(115);

                    bw.WriteInt16(Unknown1);
                    bw.WriteInt16(Unknown2);

                    bw.WriteFXR1Garbage(); //????
                }
            }

            public class Function120 : Function
            {
                public Function Func1;
                public Function Func2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(120);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Func1 = env.GetFunction(br, funcOffset1);
                    Func2 = env.GetFunction(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(120);

                    throw new NotImplementedException();
                }
            }

            public class Function121 : Function
            {
                public Function Func1;
                public Function Func2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(121);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Func1 = env.GetFunction(br, funcOffset1);
                    Func2 = env.GetFunction(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(121);

                    throw new NotImplementedException();
                }
            }

            public class Function122 : Function
            {
                public Function Func1;
                public Function Func2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(122);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Func1 = env.GetFunction(br, funcOffset1);
                    Func2 = env.GetFunction(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(122);

                    throw new NotImplementedException();
                }
            }

            public class Function123 : Function
            {
                public Function Func1;
                public Function Func2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(123);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Func1 = env.GetFunction(br, funcOffset1);
                    Func2 = env.GetFunction(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(123);

                    throw new NotImplementedException();
                }
            }

            public class Function124 : Function
            {
                public Function Func1;
                public Function Func2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(124);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Func1 = env.GetFunction(br, funcOffset1);
                    Func2 = env.GetFunction(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(124);

                    throw new NotImplementedException();
                }
            }
            public class Function126 : Function
            {
                public Function Func1;
                public Function Func2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(126);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Func1 = env.GetFunction(br, funcOffset1);
                    Func2 = env.GetFunction(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(126);

                    throw new NotImplementedException();
                }
            }

            public class Function127 : Function
            {
                public Function Func1;
                public Function Func2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(127);

                    int funcOffset1 = br.ReadFXR1Varint();
                    int funcOffset2 = br.ReadFXR1Varint();

                    Func1 = env.GetFunction(br, funcOffset1);
                    Func2 = env.GetFunction(br, funcOffset2);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(127);

                    throw new NotImplementedException();
                }
            }

            public class Function128 : Function
            {
                public Function Func;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(128);

                    int funcOffset = br.ReadFXR1Varint();

                    Func = env.GetFunction(br, funcOffset);
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(128);

                    throw new NotImplementedException();
                }
            }

            public class Function129 : Function
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

            public class Function130 : Function
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

            public class Function131 : Function
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

            public class Function132 : Function
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

            public class Function133 : Function
            {
                public int FXRID;
                public int Unknown;
                public AST Ast1;
                public AST Ast2;
                public List<FlowNode> Nodes;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(133);

                    FXRID = br.ReadFXR1Varint();
                    for (int i = 0; i < 7; i++)
                        br.AssertFXR1Varint(0);
                    Unknown = br.ReadFXR1Varint();
                    //throw new NotImplementedException();

                    Ast1 = env.GetAST(br, br.Position);
                    br.Position += AST.GetSize(br.VarintLong);

                    Ast2 = env.GetAST(br, br.Position);
                    br.Position += AST.GetSize(br.VarintLong);

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
                }
            }

            public class Function134 : Function
            {
                public int FXRID;
                public int Unknown;
                public List<Function> Funcs;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(134);

                    FXRID = br.ReadFXR1Varint();
                    Unknown = br.ReadFXR1Varint();
                    int offsetToFuncOffsetList = br.ReadFXR1Varint();
                    int funcCount = br.ReadFXR1Varint();
                    Funcs = new List<Function>(funcCount);
                    br.StepIn(offsetToFuncOffsetList);
                    for (int i = 0; i < funcCount; i++)
                    {
                        int nextFuncOffset = br.ReadInt32();
                        var func = env.GetFunction(br, nextFuncOffset);
                        Funcs.Add(func);
                    }
                    br.StepOut();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(134);

                    throw new NotImplementedException();
                }
            }

            public class Function136 : Function
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

            public class Function137 : Function
            {
                public int Unknown1;
                public int Unknown2;
                public int Unknown3;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(137);

                    Unknown1 = br.ReadFXR1Varint();
                    Unknown2 = br.ReadFXR1Varint();
                    Unknown3 = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(137);

                    bw.WriteFXR1Varint(Unknown1);
                    bw.WriteFXR1Varint(Unknown2);
                    bw.WriteFXR1Varint(Unknown3);
                }
            }

            public class Function138 : Function
            {
                public int Unknown1;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(138);

                    Unknown1 = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(138);

                    bw.WriteFXR1Varint(Unknown1);
                }
            }

            public class Function139 : Function
            {
                public int Unknown1;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(139);

                    Unknown1 = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(139);

                    bw.WriteFXR1Varint(Unknown1);
                }
            }

            public class Function140 : Function
            {
                public int Unknown1;
                public int Unknown2;

                internal override void ReadInner(BinaryReaderEx br, FxrEnvironment env)
                {
                    br.AssertFXR1Varint(140);

                    Unknown1 = br.ReadFXR1Varint();
                    Unknown2 = br.ReadFXR1Varint();
                }

                internal override void WriteInner(BinaryWriterEx bw, FxrEnvironment env)
                {
                    bw.WriteFXR1Varint(140);

                    bw.WriteFXR1Varint(Unknown1);
                    bw.WriteFXR1Varint(Unknown2);
                }
            }


        }
    }
}
