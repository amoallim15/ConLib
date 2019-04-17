using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.Geometry
{
    public class Mesh
    {
        public HashSet<HalfEdge> HalfEdges { get; private set; }

        public Mesh()
        {

        }
    }
}
