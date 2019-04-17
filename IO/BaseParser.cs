using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.IO
{
    public class BaseParser
    {
        public int FacetCount { get; internal set; }

        public void SetFacet(List<Tuple<double, double, double>> facet)
        {
            facet.ForEach(Console.WriteLine);
        }

        public Tuple<double, double, double>[] GetFacet(int i)
        {
            throw new NotImplementedException();
        }
    }
}
