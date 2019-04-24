using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.HalfEdgeDS
{
    public class Vertex
    {
        public HalfEdgeIdx HalfEdge { get; set; }
    }

    public class HalfEdge
    {
        public FaceIdx Face { get; set; }
        public VertexIdx Target { get; set; }
        public HalfEdgeIdx Next { get; set; }
        public HalfEdgeIdx Prev { get; set; }
        public HalfEdgeIdx Opposite { get; set; }
    }
    public class Face
    {
        public HalfEdgeIdx HalfEdge { get; set; }
    }
}
