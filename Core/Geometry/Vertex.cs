using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.Geometry
{
    public class Vertex
    {
        private HalfEdge Beam { get; set; }

        public Vertex(HalfEdge beam)
        {
            Beam = beam;
        }
    }
}
