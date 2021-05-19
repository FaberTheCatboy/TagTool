﻿using System.Collections.Generic;
using TagTool.Cache;
using TagTool.Common;
using TagTool.Tags;
using TagTool.Tags.Definitions;
using PhysicsModelGen2 = TagTool.Tags.Definitions.Gen2.PhysicsModel;

namespace TagTool.Commands.Porting.Gen2
{
    partial class PortTagGen2Command : Command
    {
        public PhysicsModel ConvertPhysicsModel(CachedTag tag, PhysicsModelGen2 gen2PhysicsModel)
        {
            var physicsModel = new PhysicsModel()
            {
                Flags = (PhysicsModel.PhysicsModelFlags)gen2PhysicsModel.Flags,
                Mass = gen2PhysicsModel.Mass,
                LowFrequencyDeactivationScale = gen2PhysicsModel.LowFreqDeactivationScale,
                HighFrequencyDeactivationScale = gen2PhysicsModel.HighFreqDeactivationScale,
                PhantomTypes = new List<PhysicsModel.PhantomType>(),
                NodeEdges = new List<PhysicsModel.NodeEdge>(),
                RigidBodies = new List<PhysicsModel.RigidBody>(),
                Materials = new List<PhysicsModel.Material>(),
                Spheres = new List<PhysicsModel.Sphere>(),
                Pills = new List<PhysicsModel.Pill>(),
                Boxes = new List<PhysicsModel.Box>(),
                Triangles = new List<PhysicsModel.Triangle>(),
                Polyhedra = new List<PhysicsModel.Polyhedron>(),
                PolyhedronFourVectors = new List<PhysicsModel.PolyhedronFourVector>(),
                PolyhedronPlaneEquations = new List<PhysicsModel.PolyhedronPlaneEquation>(),
                Lists = new List<PhysicsModel.List>(),
                ListShapes = new List<PhysicsModel.ListShape>(),
                HingeConstraints = new List<PhysicsModel.HingeConstraint>(),
                RagdollConstraints = new List<PhysicsModel.RagdollConstraint>(),
                Regions = new List<PhysicsModel.Region>(),
                Nodes = new List<PhysicsModel.Node>(),
                LimitedHingeConstraints = new List<PhysicsModel.LimitedHingeConstraint>(),
                Phantoms = new List<PhysicsModel.Phantom>()
            };

            //convert rigid bodies
            foreach (var gen2rig in gen2PhysicsModel.RigidBodies)
            {
                PhysicsModel.RigidBody newRig = new PhysicsModel.RigidBody
                {
                    Node = gen2rig.Node,
                    Region = gen2rig.Region,
                    Permutation = gen2rig.Permutation,
                    SerializedShapes = gen2rig.SerializedShapes,
                    BoundingSphereOffset = gen2rig.BoundingSphereOffset,
                    BoundingSphereRadius = gen2rig.BoundingSphereRadius,
                    Flags = (PhysicsModel.RigidBody.RigidBodyFlags)gen2rig.Flags,
                    MotionType = (PhysicsModel.RigidBody.MotionTypeValue)gen2rig.MotionType,
                    NoPhantomPowerAltRigidBody = gen2rig.NoPhantomPowerAlt,
                    Size = (PhysicsModel.RigidBodySize)gen2rig.Size,
                    InertiaTensorScale = gen2rig.InertiaTensorScale,
                    LinearDampening = gen2rig.LinearDamping,
                    AngularDampening = gen2rig.AngularDamping,
                    CenterOfMassOffset = gen2rig.CenterOffMassOffset,
                    ShapeType = (Havok.BlamShapeType)gen2rig.ShapeType,
                    ShapeIndex = gen2rig.Shape,
                    Mass = gen2rig.Mass,
                    CenterOfMass = gen2rig.CenterOfMass,
                    CenterOfMassRadius = gen2rig.CenterofMassRadius,
                    InertiaTensorX = gen2rig.IntertiaTensorX,
                    InertiaTensorXRadius = gen2rig.IntertiaTensorXRadius,
                    InertiaTensorY = gen2rig.IntertiaTensorY,
                    InertiaTensorYRadius = gen2rig.IntertiaTensorYRadius,
                    InertiaTensorZ = gen2rig.IntertiaTensorZ,
                    InertiaTensorZRadius = gen2rig.IntertiaTensorZRadius,
                    BoundingSpherePad = gen2rig.BoundingSpherePad
                };
                physicsModel.RigidBodies.Add(newRig);
            }

            //convert pills
            foreach (var gen2pill in gen2PhysicsModel.Pills)
            {
                PhysicsModel.Pill newPill = new PhysicsModel.Pill
                {
                    Bottom = gen2pill.Bottom,
                    BottomRadius = gen2pill.BottomRadius,
                    Top = gen2pill.Top,
                    TopRadius = gen2pill.TopRadius
                };
                ConvertHavokShape(newPill, gen2pill);
                physicsModel.Pills.Add(newPill);
            }

            //convert spheres
            //TODO: Shape reference?
            foreach (var gen2sphere in gen2PhysicsModel.Spheres)
            {
                PhysicsModel.Sphere newSphere = new PhysicsModel.Sphere
                {
                    ConvexBase = ConvertHavokShapeBase(gen2sphere.ConvexBase),
                    Translation = gen2sphere.Translation,
                    TranslationRadius = gen2sphere.TranslationRadius
                };
                ConvertHavokShape(newSphere, gen2sphere);
                physicsModel.Spheres.Add(newSphere);
            }

            //convert boxes
            foreach (var gen2box in gen2PhysicsModel.Boxes)
            {
                PhysicsModel.Box newBox = new PhysicsModel.Box
                {
                    HalfExtents = gen2box.HalfExtents,
                    HalfExtentsRadius = gen2box.HalfExtentsRadius,
                    RotationI = gen2box.RotationI,
                    RotationIRadius = gen2box.RotationIRadius,
                    RotationJ = gen2box.RotationJ,
                    RotationJRadius = gen2box.RotationJRadius,
                    RotationK = gen2box.RotationK,
                    RotationKRadius = gen2box.RotationKRadius,
                    Translation = gen2box.Translation,
                    TranslationRadius = gen2box.TranslationRadius,
                    ConvexBase = ConvertHavokShapeBase(gen2box.ShapeBase)
                };
                ConvertHavokShape(newBox, gen2box);
                physicsModel.Boxes.Add(newBox);
            }

            int usedfourvectorcount = 0;
            int polyhedra_index = 0;

            //convert polyhedra
            foreach (var gen2poly in gen2PhysicsModel.Polyhedra)
            {
                PhysicsModel.Polyhedron newPoly = new PhysicsModel.Polyhedron
                {
                    AabbHalfExtents = gen2poly.AabbHalfExtents,
                    AabbHalfExtentsRadius = gen2poly.AabbHalfExtentsRadius,
                    AabbCenter = gen2poly.AabbCenter,
                    AabbCenterRadius = gen2poly.AabbCenterRadius,
                    //FieldPointerSkip = gen2poly.FieldPointerSkip,
                    FourVectorsSize = gen2poly.FourVectorsSize,
                    FourVectorsCapacity = (uint)gen2poly.FourVectorsSize | 0x80000000,
                    NumVertices = gen2poly.NumVertices,
                    m_useSpuBuffer = gen2poly.Unknown,
                    PlaneEquationsSize = gen2poly.PlaneEquationsSize,
                    PlaneEquationsCapacity = (uint)gen2poly.PlaneEquationsSize | 0x80000000,
                    ProxyCollisionGroup = -1 //doesn't exist in H2
                };

                //sets of three or less fourvectors are stored inside the polyhedron block in H2
                if(gen2poly.FourVectorsSize <= 3)
                {
                    for (var i = 0; i < gen2poly.FourVectorsSize; i++)
                    {
                        PhysicsModelGen2.PolyhedronFourVectorsBlock gen2vector = new PhysicsModelGen2.PolyhedronFourVectorsBlock();
                        switch (i)
                        {
                            case 0:
                                gen2vector = gen2poly.FourVectorsA;
                                break;
                            case 1:
                                gen2vector = gen2poly.FourVectorsB;
                                break;
                            case 2:
                                gen2vector = gen2poly.FourVectorsC;
                                break;
                        }
                        physicsModel.PolyhedronFourVectors.Add(ConvertPolyhedronFourVector(gen2vector));
                    }                              
                }
                else
                {
                    for (var i = 0; i < gen2poly.FourVectorsSize; i++)
                    {
                        PhysicsModelGen2.PolyhedronFourVectorsBlock gen2vector = gen2PhysicsModel.PolyhedronFourVectors[usedfourvectorcount++];
                        physicsModel.PolyhedronFourVectors.Add(ConvertPolyhedronFourVector(gen2vector));
                    }
                }
                ConvertHavokShape(newPoly, gen2poly);

                //not sure what this is for, but just matching existing tags
                newPoly.ShapeBase.Offset = 32 + 128 * polyhedra_index++;

                physicsModel.Polyhedra.Add(newPoly);
            }

            //convert polyhedron plane equations
            foreach (var gen2peq in gen2PhysicsModel.PolyhedronPlaneEquations)
            {
                physicsModel.PolyhedronPlaneEquations.Add(new PhysicsModel.PolyhedronPlaneEquation
                {
                    PlaneEquation = gen2peq.PlaneEquation
                });
            }

            //convert materials
            foreach (var gen2material in gen2PhysicsModel.Materials)
            {
                physicsModel.Materials.Add(new PhysicsModel.Material
                {
                    Name = gen2material.Name,
                    MaterialName = gen2material.GlobalMaterialName,
                    PhantomType = gen2material.PhantomType,
                    //this seems to be a default value
                    RuntimeCollisionGroup = byte.MaxValue
                });
            }

            //convert lists
            foreach (var gen2list in gen2PhysicsModel.Lists)
            {
                physicsModel.Lists.Add(new PhysicsModel.List
                {
                    //FieldPointerSkip = gen2list.ShapeBase.FieldPointerSkip,
                    Size = gen2list.ShapeBase.Size,
                    Count = gen2list.ShapeBase.Count,
                    Offset = gen2list.ShapeBase.Offset,
                    ChildShapesSize = gen2list.ChildShapesSize,
                    ChildShapesCapacity = (uint)gen2list.ChildShapesSize | 0x80000000,
                    UserData = 10 //seems to be a default value
                    //TODO: Half Extents and Radius?
                });

                //convert list shapes
                for(var i = 0; i < gen2list.ChildShapesSize; i++)
                {
                    var gen2listshape = gen2list.CollisionFilter[i];
                    physicsModel.ListShapes.Add(new PhysicsModel.ListShape
                    {
                        ShapeType = (Havok.BlamShapeType)gen2listshape.ShapeType,
                        ShapeIndex = gen2listshape.Shape,
                        CollisionFilter = (uint)gen2listshape.CollisionFilter,
                        NumChildShapes = (uint)gen2list.ChildShapesSize
                    });
                    //TODO: Shape Size?
                }
            }

            //convert regions
            foreach (var gen2region in gen2PhysicsModel.Regions)
            {
                var newRegion = new PhysicsModel.Region()
                {
                    Name = gen2region.Name,
                    Permutations = new List<PhysicsModel.Region.Permutation>()
                };
                //region permutations
                foreach (var gen2perm in gen2region.Permutations)
                {
                    var newPerm = new PhysicsModel.Region.Permutation
                    {
                        Name = gen2perm.Name,
                        RigidBodies = new List<PhysicsModel.Region.Permutation.RigidBody>()
                    };
                    //permutation rigidbodies
                    foreach (var gen2rigid in gen2perm.RigidBodies)
                    {
                        newPerm.RigidBodies.Add(new PhysicsModel.Region.Permutation.RigidBody
                        {
                            RigidBodyIndex = gen2rigid.RigidBody
                        });
                    }
                    newRegion.Permutations.Add(newPerm);
                }
                physicsModel.Regions.Add(newRegion);
            }

            //convert nodes
            foreach(var gen2node in gen2PhysicsModel.Nodes)
            {
                physicsModel.Nodes.Add(new PhysicsModel.Node
                {
                    Name = gen2node.Name,
                    Flags = (ushort)gen2node.Flags,
                    Parent = gen2node.Parent,
                    Sibling = gen2node.Sibling,
                    Child = gen2node.Child
                });
            }

            return physicsModel;
        }

        public void ConvertHavokShape(PhysicsModel.Shape newShape, PhysicsModelGen2.HavokShape gen2shape)
        {
            newShape.Name = gen2shape.Name;
            newShape.MaterialIndex = gen2shape.Material;
            newShape.MaterialFlags = (PhysicsModel.MaterialFlags)gen2shape.Flags;
            newShape.RelativeMassScale = gen2shape.RelativeMassScale;
            newShape.Friction = gen2shape.Friction;
            newShape.Restitution = gen2shape.Restitution;
            newShape.Volume = gen2shape.Volume;
            newShape.Mass = gen2shape.Mass;
            newShape.MassDistributionIndex = gen2shape.MassDistributionIndex;
            newShape.PhantomIndex = (sbyte)gen2shape.Phantom;

            newShape.ShapeBase = ConvertHavokShapeBase(gen2shape.ShapeBase);
            return;
        }

        public PhysicsModel.HavokShapeBase ConvertHavokShapeBase(PhysicsModelGen2.HavokShapeBase gen2shapebase)
        {
            PhysicsModel.HavokShapeBase newShapeBase = new PhysicsModel.HavokShapeBase
            {
                //FieldPointerSkip = gen2shapebase.FieldPointerSkip,
                Size = gen2shapebase.Size,
                Count = gen2shapebase.Count,
                //Offset = gen2shapebase.Offset,
                Radius = gen2shapebase.Radius
            };
            return newShapeBase;
        }

        public PhysicsModel.PolyhedronFourVector ConvertPolyhedronFourVector(PhysicsModelGen2.PolyhedronFourVectorsBlock gen2vector)
        {
            return new PhysicsModel.PolyhedronFourVector
            {
                FourVectorsX = gen2vector.FourVectorsX,
                FourVectorsXRadius = gen2vector.FourVectorsXRadius,
                FourVectorsY = gen2vector.FourVectorsY,
                FourVectorsYRadius = gen2vector.FourVectorsYRadius,
                FourVectorsZ = gen2vector.FourVectorsZ,
                FourVectorsZRadius = gen2vector.FourVectorsZRadius
            };
        }
    }
}
