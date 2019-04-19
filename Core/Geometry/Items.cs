using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.Geometry
{
    public class Vertex<THalfEdgeHandle, TPoint>
    {
        public THalfEdgeHandle HalfEdge { get; set; }
        public TPoint Point { get; private set; }

        public Vertex(TPoint point)
        {
            Point = point;
        }
    }

    public class Face<THalfEdgeHandle>
    {
        public THalfEdgeHandle HalfEdge { get; set; }
    }

    public class HalfEdge<THalfEdgeHandle, TVertexHandle, TFaceHandle>
    {
        public THalfEdgeHandle Opposite { get; set; }
        public THalfEdgeHandle Next { get; set; }
        public THalfEdgeHandle Prev { get; set; }
        public TVertexHandle Vertex { get; set; }
        public TFaceHandle Face { get; set; }

        public bool IsBorder()
        {
            return true;
            //return Face == FaceHandle();
        }
    }
}
