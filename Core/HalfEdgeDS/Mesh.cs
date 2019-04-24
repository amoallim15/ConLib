using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.HalfEdgeDS
{
    public class Mesh<Point>: ICloneable
    {
        public Mesh() { }
        
        public VertexIdx AddVertex() { return null; }
        public VertexIdx AddVertex(Point p) { return null; }
        public void RemoveVertex(VertexIdx v) { }

        public HalfEdgeIdx AddEdge() { return null; }
        public HalfEdgeIdx AddEdge(VertexIdx v0, VertexIdx v1) { return null; }
        public void RemoveEdge(EdgeIdx e) { }

        public FaceIdx AddFace() { return null; }
        public FaceIdx AddFace(VertexIdx v0, VertexIdx v1, VertexIdx v2) { return null; }
        public FaceIdx AddFace(params VertexIdx[] vertices) { return null; }
        public void RemoveFace(FaceIdx f) { }

        public uint VertexCount() { return 0; }
        public uint RemovedVertexCount() { return 0; }
        public uint HalfEdgeCount() { return 0; }
        public uint RemovedHalfEdgeCount() { return 0; }
        public uint EdgeCount() { return 0; }
        public uint RemovedEdgeCount() { return 0; }
        public uint FaceCount() { return 0; }
        public uint RemovedFaceCount() { return 0; }

        public bool IsEmpty() { return false; }
        public void Clear() { }
        public bool Join(Mesh<Point> other) { return false; }
        public object Clone() { /* TODO: perform deep copy*/ return null; }

        public bool IsRemoved(VertexIdx v) { return false; }
        public bool IsRemoved(HalfEdgeIdx he) { return false; }
        public bool IsRemoved(EdgeIdx e) { return false; }
        public bool IsRemoved(FaceIdx f) { return false; }

        public bool HasValidIndex(VertexIdx v) { return false; }
        public bool HasValidIndex(HalfEdgeIdx he) { return false; }
        public bool HasValidIndex(EdgeIdx e) { return false; }
        public bool HasValidIndex(FaceIdx f) { return false; }

        public bool HasGarbage() { return false; }
        public void CollectGarbage() { }

        public bool IsValid(bool verbose = true) { return false; }
        public bool IsValid(VertexIdx v) { return false; }
        public bool IsValid(HalfEdgeIdx he) { return false; }
        public bool IsValid(EdgeIdx e) { return false; }
        public bool IsValid(FaceIdx f) { return false; }

        public bool IsBorder(VertexIdx v, bool checkIncidentHalfEdges = true) { return false; }
        public bool IsBorder(HalfEdgeIdx he) { return false; }
        public bool IsBorder(EdgeIdx e) { return false; }

        public bool IsIsolated(VertexIdx v) { return false; }

        public static VertexIdx NullVertex() { return new VertexIdx(); }
        public static HalfEdgeIdx NullHalfEdge() { return new HalfEdgeIdx(); }
        public static EdgeIdx NullEdge() { return new EdgeIdx(); }
        public static FaceIdx NullFace() { return new FaceIdx(); }
    }
}
