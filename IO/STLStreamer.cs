using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static ConLib.IO.Options;

namespace ConLib.IO
{
    public class STLStreamer : BaseStreamer
    {
        public override List<string> Extensions => new List<string> { "STL", "STLA", "STLB" };

        public override void Read(string path, BaseParser parser, HashSet<Flag> flags)
        {
            if (!CanStream(path))
                throw new Exception("Invalid file extension");

            string STLType = ReadSTLType(path);
            switch (STLType)
            {
                case "STLA":
                    using (StreamReader reader = new StreamReader(path))
                        ReadSTLA(reader, parser);
                    break;
                case "STLB":
                    using(BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                        ReadSTLB(reader, parser);
                    break;
            }
        }

        private void ReadSTLA(StreamReader reader, BaseParser parser)
        {
            const string _solidCheck = @"solid\s+(?<Name>[^\r\n]+)?";
            const string _facetVertexCheck = @"\s*(facet normal|vertex)\s+(?<X>[^\s]+)\s+(?<Y>[^\s]+)\s+(?<Z>[^\s]+)";
            Match solidOpenMatch;
            if (!(solidOpenMatch = Regex.Match(reader.ReadLine(), _solidCheck)).Success)
                throw new Exception(string.Format("Invalid STL header, expected \"solid [name]\", but found \"{0}\".", solidOpenMatch.Value));
            Match facetOpenMatch;
            while ((facetOpenMatch = Regex.Match(reader.ReadLine(), _facetVertexCheck)).Success)
            {
                double normalX = double.Parse(facetOpenMatch.Groups["X"].Value);
                double normalY = double.Parse(facetOpenMatch.Groups["Y"].Value);
                double normalZ = double.Parse(facetOpenMatch.Groups["Z"].Value);
                parser.AddFacetNormal(normalX, normalY, normalZ);
                reader.ReadLine();
                for (int i = 0; i < 3; i++)
                {
                    Match vertexMatch;
                    if (!(vertexMatch = Regex.Match(reader.ReadLine(), _facetVertexCheck)).Success)
                        throw new Exception(string.Format("Invalid STL facet vertex, expected \"vertex [x] [y] [z]\", but found \"{0}\".", vertexMatch.Value));
                    double vertexX = double.Parse(vertexMatch.Groups["X"].Value);
                    double vertexY = double.Parse(vertexMatch.Groups["Y"].Value);
                    double vertexZ = double.Parse(vertexMatch.Groups["Z"].Value);
                    parser.AddFacetVertex(vertexX, vertexY, vertexZ);
                }
                reader.ReadLine();
                reader.ReadLine();
            }
        }

        private void ReadSTLB(BinaryReader reader, BaseParser parser)
        {
            reader.ReadBytes(84);
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                const int floatSize = sizeof(float);
                const int ushortSize = sizeof(ushort);
                const int coordsSize = floatSize * 3;
                byte[] normal = new byte[coordsSize];
                int normalBytesRead = reader.Read(normal, 0, normal.Length);
                if (normalBytesRead == 0)
                    break;
                else if (normalBytesRead != normal.Length)
                    throw new Exception(string.Format("Invalid STL facet normal, expected \"{0}\" bytes, but found \"{1}\".", coordsSize, normalBytesRead));

                float normalX = BitConverter.ToSingle(normal, 0);
                float normalY = BitConverter.ToSingle(normal, floatSize);
                float normalZ = BitConverter.ToSingle(normal, floatSize * 2);
                parser.AddFacetNormal(normalX, normalY, normalZ);

                for (int i = 0; i < 3; i++)
                {
                    byte[] vertex = new byte[coordsSize];
                    int vertexBytesRead = reader.Read(vertex, 0, vertex.Length);
                    if (vertexBytesRead == 0 || vertexBytesRead != normal.Length)
                        throw new Exception(string.Format("Invalid STL vertex, expected \"{0}\" bytes, but found \"{1}\".", coordsSize, vertexBytesRead));
                    float vertexX = BitConverter.ToSingle(vertex, 0);
                    float vertexY = BitConverter.ToSingle(vertex, floatSize);
                    float vertexZ = BitConverter.ToSingle(vertex, floatSize * 2);
                    parser.AddFacetVertex(vertexX, vertexY, vertexZ);
                }
                reader.ReadBytes(ushortSize);
            }
        }
        
        private string ReadSTLType(string path)
        {
            string type;
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                const string solid = "solid";
                int asBinarySize = sizeof(uint);
                int asTextSize = solid.Length;

                byte[] asBinary = new byte[asBinarySize];
                byte[] asText = new byte[asTextSize];

                reader.BaseStream.Seek(80, SeekOrigin.Begin);
                int readBinary = reader.Read(asBinary, 0, asBinarySize);
                int size = (int)BitConverter.ToUInt32(asBinary, 0);

                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                int readText = reader.Read(asText, 0, asTextSize);
                string header = Encoding.ASCII.GetString(asText);

                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                if (reader.BaseStream.Length == size * 50 + 84)
                    type = "STLB";
                else if(solid.Equals(header, StringComparison.InvariantCultureIgnoreCase))
                    type = "STLA";
                else
                    throw new Exception("Invalid STL file format");
            }
            return type;
        }

        public override void Write(string path, BaseParser exporter, HashSet<Flag> flags)
        {
            if (!CanStream(path))
                throw new Exception("Invalid file extension");
            if (flags == null)
                flags = new HashSet<Flag>();
            string ext = GetExtension(path);

            if (ext == "STLB" || flags.Contains(Flag.Binary))
            {

            }
            else
            {

            }

        }
    }
}
