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
        public class FxrEnvironment
        {
            public BinaryWriterEx bw;

            private List<long> FunctionOffets = new List<long>();

            private Dictionary<long, object> ObjectsByOffset = new Dictionary<long, object>();
            private Dictionary<object, long> OffsetsByObject = new Dictionary<object, long>();

            private List<object> ThingsToWrite = new List<object>();

            public void AddThingToWrite(object thingToWrite)
            {
                if (!ThingsToWrite.Contains(thingToWrite))
                    ThingsToWrite.Add(thingToWrite);
            }

            internal List<FlowEdge> masterFlowEdgeList = new List<FlowEdge>();
            internal List<FlowNode> masterFlowNodeList = new List<FlowNode>();
            internal List<FlowAction> masterFlowActionList = new List<FlowAction>();

            public void RegisterOffset(long offset, object thingThere)
            {
                if (!ObjectsByOffset.ContainsKey(offset))
                    ObjectsByOffset.Add(offset, thingThere);

                if (!OffsetsByObject.ContainsKey(thingThere))
                    OffsetsByObject.Add(thingThere, offset);
            }

            public void CalculateAllIndices()
            {
                foreach (var edge in masterFlowEdgeList)
                    edge.CalculateIndices(this);

                foreach (var node in masterFlowNodeList)
                    node.CalculateIndices(this);
            }

            public int GetFlowEdgeIndex(FlowEdge edge)
            {
                return masterFlowEdgeList.IndexOf(edge);
            }

            public int GetFlowNodeIndex(FlowNode node)
            {
                return masterFlowNodeList.IndexOf(node);
            }

            public int GetFlowActionIndex(FlowAction action)
            {
                return masterFlowActionList.IndexOf(action);
            }

            private void RegisterFlowNode(FlowNode node)
            {
                if (!masterFlowNodeList.Contains(node))
                    masterFlowNodeList.Add(node);
            }

            private void RegisterFlowEdge(FlowEdge edge)
            {
                if (!masterFlowEdgeList.Contains(edge))
                    masterFlowEdgeList.Add(edge);
            }

            private void RegisterFlowAction(FlowAction action)
            {
                if (!masterFlowActionList.Contains(action))
                    masterFlowActionList.Add(action);
            }

            public ASTFunction GetASTFunction(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is ASTFunction v)
                        return v;
                    else 
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = ASTFunction.Read(br, this);
                    br.StepOut();

                    ObjectsByOffset.Add(offset, newVal);
                    return newVal;
                }
            }

            public ASTPool2 GetASTPool2(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is ASTPool2 v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = ASTPool2.Read(br, this);
                    br.StepOut();

                    ObjectsByOffset.Add(offset, newVal);
                    return newVal;
                }
            }

            public ASTPool3 GetASTPool3(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is ASTPool3 v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = ASTPool3.Read(br, this);
                    br.StepOut();

                    ObjectsByOffset.Add(offset, newVal);
                    return newVal;
                }
            }

            public Function GetFunction(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is Function v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = Function.Read(br, this);
                    br.StepOut();

                    ObjectsByOffset.Add(offset, newVal);
                    return newVal;
                }
            }

            public AST GetAST(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is AST v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    var newVal = new AST();
                    ObjectsByOffset.Add(offset, newVal);
                    br.StepIn(offset);
                    newVal.Read(br, this);
                    br.StepOut();
                    
                    return newVal;
                }
            }

            public FlowNode GetFlowNode(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is FlowNode v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    var newVal = new FlowNode();
                    RegisterFlowNode(newVal);
                    ObjectsByOffset.Add(offset, newVal);
                    br.StepIn(offset);
                    newVal.Read(br, this);
                    br.StepOut();
                    
                    return newVal;
                }
            }

            public FlowEdge GetFlowEdge(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is FlowEdge v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    var newVal = new FlowEdge();
                    RegisterFlowEdge(newVal);
                    ObjectsByOffset.Add(offset, newVal);
                    br.StepIn(offset);
                    newVal.Read(br, this);
                    br.StepOut();

                    return newVal;
                }
            }

            public FlowAction GetFlowAction(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is FlowAction v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = FlowAction.Read(br, this);
                    RegisterFlowAction(newVal);
                    br.StepOut();

                    ObjectsByOffset.Add(offset, newVal);
                    return newVal;
                }
            }

            private Dictionary<object, List<long>> PointerWriteLocations = new Dictionary<object, List<long>>();

            public void RegisterPointer32(object pointToObject)
            {
                if (!PointerWriteLocations.ContainsKey(pointToObject))
                    PointerWriteLocations.Add(pointToObject, new List<long>());

                if (!PointerWriteLocations[pointToObject].Contains(bw.Position))
                    PointerWriteLocations[pointToObject].Add(bw.Position);

                bw.WriteUInt32(0xEFBEADDE); //DEADBEEF
            }

            public void RegisterPointer(object pointToObject)
            {
                if (!PointerWriteLocations.ContainsKey(pointToObject))
                    PointerWriteLocations.Add(pointToObject, new List<long>());

                if (!PointerWriteLocations[pointToObject].Contains(bw.Position))
                    PointerWriteLocations[pointToObject].Add(bw.Position);

                bw.WriteUInt32(0xEFBEADDE); //DEADBEEF
                bw.WriteUInt32(0xCDCDCDCD); 
            }

            public void FillAllPointers()
            {
                long startOffset = bw.Position;
                foreach (var kvp in PointerWriteLocations)
                {
                    var locationItPointsTo = OffsetsByObject[kvp.Key];
                    foreach (var location in kvp.Value)
                    {
                        bw.Position = location;
                        bw.WriteInt32((int)locationItPointsTo);
                    }
                }
                bw.Position = startOffset;
            }

            public void RegisterFunctionOffsetHere()
            {
                if (!FunctionOffets.Contains(bw.Position))
                    FunctionOffets.Add(bw.Position);
            }

            public void WriteAllFunctions()
            {
                foreach (var func in ThingsToWrite.OfType<Function>())
                {
                    RegisterFunctionOffsetHere();
                    func.WriteInner(bw, this);
                }
            }

            public void WriteFunctionTable(string tableCountFillLabel)
            {
                foreach (var location in FunctionOffets.OrderBy(x => x))
                {
                    bw.WriteFXR1Varint((int)location);
                }
                bw.FillInt32(tableCountFillLabel, FunctionOffets.Count);
            }

            public void WritePointerTable(string tableCountFillLabel)
            {
                
                List<long> allPointerWriteLocations = new List<long>();
                foreach (var kvp in PointerWriteLocations)
                {
                    foreach (var offset in kvp.Value)
                    {
                        if (!allPointerWriteLocations.Contains(offset))
                        {
                            allPointerWriteLocations.Add(offset);
                        }
                    }
                }

                allPointerWriteLocations = allPointerWriteLocations.OrderBy(x => x).ToList();

                foreach (var offset in allPointerWriteLocations)
                {
                    bw.WriteFXR1Varint((int)offset);
                }

                bw.FillInt32(tableCountFillLabel, allPointerWriteLocations.Count);
            }
        }
    }
}
