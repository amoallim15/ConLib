using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.Geometry
{
    public class HalfEdge
    {
        public enum Type { Imaginary, Real }
        public HalfEdge Twin {get; private set;}
        public HalfEdge Next { get; private set; }
        public HalfEdge Prev { get; private set; }
        public Vertex Origin { get; private set; }
        public Face Side { get; private set; }


    }
}
