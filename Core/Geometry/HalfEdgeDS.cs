using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.Geometry
{
    public class HalfEdgeDS<THalfEdgeHandle, TVertexHandle, TFaceHandle, TPoint>
    {
        public List<Vertex<THalfEdgeHandle, TPoint>> VertexList { get; set; }
        public List<HalfEdge<THalfEdgeHandle, TVertexHandle, TFaceHandle>> HalfEdgeList { get; set; }
        public List<Face<THalfEdgeHandle>> FaceList { get; set; }
    }
}
