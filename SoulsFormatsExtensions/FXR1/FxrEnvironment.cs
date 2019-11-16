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

            public FXR1 fxr;

            public List<Param> Debug_AllReadParams = new List<Param>();

            public List<Function> XmlFunctionList = new List<Function>();

            public void Debug_RegisterReadParam(Param p)
            {
                if (!Debug_AllReadParams.Contains(p))
                    Debug_AllReadParams.Add(p);
            }

            private List<long> PointerOffsets = new List<long>();
            private List<long> FunctionOffets = new List<long>();

            public Dictionary<long, object> ObjectsByOffset = new Dictionary<long, object>();
            public Dictionary<object, long> OffsetsByObject = new Dictionary<object, long>();

            private List<object> ThingsToWrite = new List<object>();

            public void RegisterPointerOffset(long offset)
            {
                if (!PointerOffsets.Contains(offset))
                    PointerOffsets.Add(offset);
            }

            public void AddThingToWrite(object thingToWrite)
            {
                if (!ThingsToWrite.Contains(thingToWrite))
                    ThingsToWrite.Add(thingToWrite);
            }

            public void RegisterOffset(long offset, object thingThere)
            {
                if (!ObjectsByOffset.ContainsKey(offset))
                    ObjectsByOffset.Add(offset, thingThere);

                if (!OffsetsByObject.ContainsKey(thingThere))
                    OffsetsByObject.Add(thingThere, offset);
            }

            public FunctionPointer GetEffectFunction(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is FunctionPointer v)
                        return v;
                    else 
                        throw new InvalidOperationException();
                }
                else
                {
                    var newVal = new FunctionPointer();
                    RegisterOffset(offset, newVal);
                    br.StepIn(offset);
                    newVal.Read(br, this);
                    br.StepOut();
                    
                    return newVal;
                }
            }

            public Behavior GetBehavior(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is Behavior v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = Behavior.Read(br, this);
                    RegisterOffset(offset, newVal);
                    newVal.XID = $"0x{offset:X}";
                    br.StepOut();

                    return newVal;
                }
            }

            public Template GetTemplate(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is Template v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    br.StepIn(offset);
                    var newVal = Template.GetProperType(br, this);
                    RegisterOffset(offset, newVal);
                    newVal.XID = $"0x{offset:X}";
                    newVal.Read(br, this);
                    br.StepOut();
                    
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
                    var newVal = Function.GetProperFunctionType(br, this);
                    RegisterOffset(offset, newVal);
                    newVal.XID = $"0x{offset:X}";
                    XmlFunctionList.Add(newVal);
                    newVal.Read(br, this);
                    br.StepOut();
                    
                    return newVal;
                }
            }

            public Effect GetEffect(BinaryReaderEx br, long offset)
            {
                if (offset == 0)
                    return null;

                if (ObjectsByOffset.ContainsKey(offset))
                {
                    if (ObjectsByOffset[offset] is Effect v)
                        return v;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    var newVal = new Effect();
                    RegisterOffset(offset, newVal);
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
                    RegisterOffset(offset, newVal);
                    newVal.XID = $"0x{offset:X}";
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
                    RegisterOffset(offset, newVal);
                    newVal.XID = $"0x{offset:X}";
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
                    var newVal = new FlowAction();
                    RegisterOffset(offset, newVal);
                    newVal.XID = $"0x{offset:X}";
                    br.StepIn(offset);
                    newVal.Read(br, this);
                    br.StepOut();
                    return newVal;
                }
            }

            private Dictionary<object, List<long>> PointerWriteLocations = new Dictionary<object, List<long>>();

            //public void RegisterPointer32(object pointToObject)
            //{
            //    if (!PointerWriteLocations.ContainsKey(pointToObject))
            //        PointerWriteLocations.Add(pointToObject, new List<long>());

            //    if (!PointerWriteLocations[pointToObject].Contains(bw.Position))
            //        PointerWriteLocations[pointToObject].Add(bw.Position);

            //    bw.WriteUInt32(0xEFBEADDE); //DEADBEEF
            //}

            public void RegisterPointer(object pointToObject, bool useExistingPointerOnly = false)
            {
                if (pointToObject == null)
                {
                    bw.WriteFXR1Varint(0);
                    return;
                }

                if (!PointerOffsets.Contains(bw.Position))
                    PointerOffsets.Add(bw.Position);

                if (OffsetsByObject.ContainsKey(pointToObject))
                {
                    bw.WriteFXR1Varint((int)OffsetsByObject[pointToObject]);
                }
                else
                {
                    if (useExistingPointerOnly)
                        throw new InvalidOperationException("Assertion that pointer already existed failed.");

                    if (!PointerWriteLocations.ContainsKey(pointToObject))
                        PointerWriteLocations.Add(pointToObject, new List<long>());

                    if (!PointerWriteLocations[pointToObject].Contains(bw.Position))
                        PointerWriteLocations[pointToObject].Add(bw.Position);

                    bw.WriteUInt32(0xEEEEEEEE);
                    bw.WriteFXR1Garbage();
                }
            }


            public void FinishRecursiveWrite()
            {
                // Copy the current write shit to a new collection so we can start queueing the
                // shit for the next recursive pass :demonicfatcat:
                var currentWriteFeed = new Dictionary<object, List<long>>();
                foreach (var kvp in PointerWriteLocations)
                {
                    currentWriteFeed.Add(kvp.Key, kvp.Value);
                }
                PointerWriteLocations.Clear();

                foreach (var kvp in currentWriteFeed)
                {
                    // Register this as an offset for recursive write and pointer shit
                    RegisterOffset(bw.Position, kvp.Key);

                    // Write the actual data
                    void DoDataWrite(object data)
                    {
                        switch (data)
                        {
                            case FunctionPointer asEffectFunction: asEffectFunction.Write(bw, this); break;
                            case Behavior asBehavior: asBehavior.Write(bw, this); break;
                            case Template asTemplate: asTemplate.Write(bw, this); break;
                            case Effect asEffect: asEffect.Write(bw, this); break;
                            case FlowAction asFlowAction: asFlowAction.Write(bw, this); break;
                            case FlowEdge asFlowEdge: asFlowEdge.Write(bw, this); break;
                            case FlowNode asFlowNode: asFlowNode.Write(bw, this); break;
                            case Function asFunction: asFunction.Write(bw, this); break;
                            case List<Function> asFunctionList:
                                foreach (var v in asFunctionList)
                                    RegisterPointer(v);
                                break;
                            case List<FlowEdge> asFlowEdgeList:
                                //if (!PointerOffsets.Contains(bw.Position))
                                //    PointerOffsets.Add(bw.Position);

                                foreach (var v in asFlowEdgeList)
                                {
                                    RegisterOffset(bw.Position, v);
                                    v.Write(bw, this);
                                }
                                break;
                            case List<FlowAction> asFlowActionList:
                                foreach (var v in asFlowActionList)
                                {
                                    RegisterOffset(bw.Position, v);
                                    v.Write(bw, this);
                                }
                                break;
                            case List<FlowNode> asFlowNodeList:
                                foreach (var v in asFlowNodeList)
                                {
                                    RegisterOffset(bw.Position, v);
                                    v.Write(bw, this);
                                }
                                break;
                            case List<FunctionPointer> asEffectFunctionList:
                                foreach (var v in asEffectFunctionList)
                                {
                                    RegisterOffset(bw.Position, v);
                                    RegisterPointerOffset(bw.Position);
                                    v.Write(bw, this);
                                }
                                break;
                            case List<float> asFloatList:
                                foreach (var v in asFloatList)
                                {
                                    bw.WriteSingle(v);
                                }
                                break;
                            case List<int> asIntList:
                                foreach (var v in asIntList)
                                {
                                    bw.WriteInt32(v);
                                }
                                break;
                            default: throw new NotImplementedException($"FxrEnvironment recursive write not implemented for '{data.GetType().ToString()}'");
                        }
                    }
                    DoDataWrite(kvp.Key);

                    // Fill in any current references to this shit
                    var locationItPointsTo = OffsetsByObject[kvp.Key];
                    foreach (var location in kvp.Value)
                    {
                        bw.StepIn(location);
                        RegisterPointerOffset(bw.Position);
                        bw.WriteInt32((int)locationItPointsTo);
                        bw.StepOut();
                    }
                }

                if (PointerWriteLocations.Count > 0)
                {
                    FinishRecursiveWrite();
                }
            }

            public void RegisterFunctionOffsetHere()
            {
                if (!FunctionOffets.Contains(bw.Position))
                    FunctionOffets.Add(bw.Position);
            }

            //public void WriteAllFunctions()
            //{
            //    foreach (var func in ThingsToWrite.OfType<Function>())
            //    {
            //        RegisterFunctionOffsetHere();
            //        func.WriteInner(bw, this);
            //    }
            //}

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
                foreach (var offset in PointerOffsets.OrderBy(x => x))
                {
                    bw.WriteFXR1Varint((int)offset);
                }
                bw.FillInt32(tableCountFillLabel, PointerOffsets.Count);
            }

            public void ReadPointerTable(BinaryReaderEx br, int count)
            {
                PointerOffsets = new List<long>();
                for (int i = 0; i < count; i++)
                    PointerOffsets.Add(br.ReadFXR1Varint());
            }
        }
    }
}
