using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConLib.IO
{
    public class STLAStreamer : BaseStreamer
    {
        private const string solid = "solid";
        private const int headerSize = 5;
        private const string _solidCheck = @"solid\s+(?<Name>[^\r\n]+)?";
        private const string _facetVertexCheck = @"\s*(facet normal|vertex)\s+(?<X>[^\s]+)\s+(?<Y>[^\s]+)\s+(?<Z>[^\s]+)";

        public override List<string> Extensions => new List<string> { "STL", "STLA" };

        public override void Read(string path, BaseParser parser)
        {
            if (!CanStream(path))
                throw new Exception("Invalid STL file extension");

            using(StreamReader reader = new StreamReader(path))
            {
                ValidateSTLType(reader.BaseStream);
                ReadSTLA(reader, parser);
            }
            
        }

        private void ReadSTLA(StreamReader reader, BaseParser parser)
        {
            List<Tuple<double, double, double>> facet;
            Match solidOpenMatch;
            if (!(solidOpenMatch = Regex.Match(reader.ReadLine(), _solidCheck)).Success)
                throw new Exception(string.Format("Invalid STL header, expected \"solid [name]\", but found \"{0}\".", solidOpenMatch.Value));
            Match facetOpenMatch;
            while ((facetOpenMatch = Regex.Match(reader.ReadLine(), _facetVertexCheck)).Success)
            {
                facet = new List<Tuple<double, double, double>>
                {
                    Tuple.Create(double.Parse(facetOpenMatch.Groups["X"].Value), double.Parse(facetOpenMatch.Groups["Y"].Value), double.Parse(facetOpenMatch.Groups["Z"].Value))
                };
                reader.ReadLine();
                for (int i = 0; i < 3; i++)
                {
                    Match vertexMatch;
                    if (!(vertexMatch = Regex.Match(reader.ReadLine(), _facetVertexCheck)).Success)
                        throw new Exception(string.Format("Invalid STL facet vertex, expected \"vertex [x] [y] [z]\", but found \"{0}\".", vertexMatch.Value));
                    facet.Add(Tuple.Create(double.Parse(vertexMatch.Groups["X"].Value), double.Parse(vertexMatch.Groups["Y"].Value), double.Parse(vertexMatch.Groups["Z"].Value)));
                    
                }
                reader.ReadLine();
                reader.ReadLine();
                parser.SetFacet(facet);
            }
        }

        private void ValidateSTLType(Stream stream)
        {
            byte[] data = new byte[headerSize];
            stream.Seek(0, SeekOrigin.Begin);
            /*int headerRead = */ stream.Read(data, 0, headerSize);
            string header = Encoding.ASCII.GetString(data);
            stream.Seek(0, SeekOrigin.Begin);

            if (!solid.Equals(header, StringComparison.InvariantCultureIgnoreCase))
                throw new Exception("Invalid STL file format");
        }

        public override void Write(string path, BaseParser parser)
        {
            if (!CanStream(path))
                throw new Exception("Invalid STL file extension");

            using(StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Format("soild {0}", path));
                for(int i = 0; i < parser.FacetCount; i++)
                {
                    Tuple<double, double, double>[] facet = parser.GetFacet(i);
                    var normal = facet.ElementAt(1);
                    writer.WriteLine(string.Format("\tfacet normal {0} {1} {2}\n\t\touter loop", (float)normal.Item1, (float)normal.Item2, (float)normal.Item3));
                    for (int j = 0; j < 3; j++) {
                        var vertex = facet.ElementAt(j + 1);
                        writer.WriteLine(string.Format("\t\t\tvertex {0} {1} {2}", (float)vertex.Item1, (float)vertex.Item2, (float)vertex.Item3));
                    }
                    writer.WriteLine("\t\tendloop\n\tendfacet");
                }
                writer.WriteLine(string.Format("endsolid {0}\n", path));
            }
        }
    }
}
