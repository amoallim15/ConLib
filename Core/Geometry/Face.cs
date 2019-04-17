using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.Geometry
{
    public class Face
    {
        private HalfEdge Border { get; set; }

        public Face(HalfEdge border)
        {
            Border = border;
        }
    }
}
