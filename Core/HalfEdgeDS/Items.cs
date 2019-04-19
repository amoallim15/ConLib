using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.HalfEdgeDS
{
    public struct Vertex
    {
        public HalfEdgeIdx HalfEdge { get; set; }
    }

    public struct HalfEdge
    {
        public FaceIdx Face { get; set; }
        public VertexIdx Vertex { get; set; }
        public HalfEdgeIdx Next { get; set; }
        public HalfEdgeIdx Prev { get; set; }
    }
    public struct Face
    {
        public HalfEdgeIdx HalfEdge { get; set; }
    }
}
