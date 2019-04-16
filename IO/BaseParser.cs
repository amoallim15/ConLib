using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.IO
{
    public class BaseParser
    {
        public void AddFacetNormal(double normalX, double normalY, double normalZ)
        {
            Console.WriteLine("normal " + normalX.ToString() + " " + normalY.ToString() + " " + normalZ.ToString());
        }

        public void AddFacetVertex(double vertexX, double vertexY, double vertexZ)
        {
            Console.WriteLine("vertex " + vertexX.ToString() + " " + vertexY.ToString() + " " + vertexZ.ToString());
        }
    }
}
