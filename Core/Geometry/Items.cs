using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.Geometry
{
    public struct Vertex
    {
        public Handle HalfEdge { get; set; }
    }

    public struct Face
    {
        public Handle HalfEdge { get; set; }
    }

    public struct HalfEdge
    {
        public Handle Opposite { get; set; }
        public Handle Next { get; set; }
        public Handle Prev { get; set; }
        public Face Face { get; set; }
        public Vertex Vertex { get; set; }
    }

    public struct HalfEdgeDS
    {
        public List<Vertex> Vertices { get; set; }
        public List<HalfEdge> HalfEdges { get; set; }
        public List<Face> Faces { get; set; }

    }
}
