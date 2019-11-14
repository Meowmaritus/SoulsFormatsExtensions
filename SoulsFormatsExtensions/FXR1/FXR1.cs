﻿using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsFormatsExtensions
{
    public partial class FXR1 : SoulsFile<FXR1>
    {
        public bool BigEndian { get; set; } = false;
        public bool Wide { get; set; } = false;
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }

        public Function RootFunction { get; set; }

        public List<FlowNode> FlowNodes { get; set; }
        public List<FlowEdge> FlowEdges { get; set; }
        public List<FlowAction> FlowActions { get; set; }

        protected override void Read(BinaryReaderEx br)
        {
            br.AssertASCII("FXR\0");
            int endianCheck = br.AssertInt32(0x10000, 0x100);
            br.BigEndian = BigEndian = (endianCheck == 0x100);

            uint longCheck = br.GetUInt32(0xC);
            br.VarintLong = Wide = (longCheck == 0 || longCheck == 0xCDCDCDCD);

            long mainDataOffset = br.ReadFXR1Varint();
            int metadataTableOffset = br.ReadInt32();
            int pointerTableCount = br.ReadInt32();
            int functionTableCount = br.ReadInt32();
            Unknown1 = br.ReadInt32();
            Unknown2 = br.ReadInt32();

            var env = new FxrEnvironment();

            br.Pad(16);

            RootFunction = env.GetFunction(br, br.Position);

            env.CalculateAllIndices();

            FlowNodes = env.masterFlowNodeList;
            FlowEdges = env.masterFlowEdgeList;
            FlowActions = env.masterFlowActionList;
        }

        protected override void Write(BinaryWriterEx bw)
        {
            bw.WriteASCII("FXR\0");
            bw.BigEndian = BigEndian;
            bw.WriteUInt32(0x10000);
            bw.VarintLong = Wide;
            var env = new FxrEnvironment();
            env.bw = bw;
            env.fxr = this;

            env.RegisterPointer(RootFunction);

            bw.ReserveFXR1Varint("OffsetToTable");

            bw.ReserveInt32("TablePointerCount");
            bw.ReserveInt32("TableFunctionCount");

            bw.WriteInt32(Unknown1);
            bw.WriteInt32(Unknown2);

            bw.Pad(16);
            
            // Write RootFunction and everything else :fatcat:
            env.FinishRecursiveWrite();

            bw.FillFXR1Varint("OffsetToTable", (int)bw.Position);
            env.WritePointerTable("TablePointerCount");
            env.WriteFunctionTable("TableFunctionCount");

        }

    }
}
