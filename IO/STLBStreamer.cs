using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.IO
{
    public class STLBStreamer : BaseStreamer
    {
        private const int binarySize = sizeof(uint);
        private const int floatSize = sizeof(float);
        private const int xyzSize = floatSize * 3;
        private const int ushortSize = sizeof(ushort);
        private int triangles;
        private const string header = "binary stl file";

        public override List<string> Extensions => new List<string> { "STL", "STLB" };

        public override void Read(string path, BaseParser parser)
        {
            if (!CanStream(path))
                throw new Exception("Invalid STL file extension");

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                ValidateSTLType(reader.BaseStream);
                ReadSTLB(reader, parser);
            }
        }

        private void ReadSTLB(BinaryReader reader, BaseParser parser)
        {
            List<Tuple<double, double, double>> facet;
            reader.ReadBytes(84);
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                byte[] normal = new byte[xyzSize];
                int normalRead = reader.Read(normal, 0, xyzSize);
                if (normalRead == 0)
                    break;
                else if (normalRead != xyzSize)
                    throw new Exception(string.Format("Invalid STL facet normal, expected \"{0}\" bytes, but found \"{1}\".", xyzSize, normalRead));

                facet = new List<Tuple<double, double, double>>
                {
                    Tuple.Create((double)BitConverter.ToSingle(normal, 0), (double)BitConverter.ToSingle(normal, floatSize), (double)BitConverter.ToSingle(normal, floatSize * 2))
                };
                for (int i = 0; i < 3; i++)
                {
                    byte[] vertex = new byte[xyzSize];
                    int vertexRead = reader.Read(vertex, 0, xyzSize);
                    if (vertexRead == 0 || vertexRead != xyzSize)
                        throw new Exception(string.Format("Invalid STL vertex, expected \"{0}\" bytes, but found \"{1}\".", xyzSize, vertexRead));
                    facet.Add(Tuple.Create((double)BitConverter.ToSingle(vertex, 0), (double)BitConverter.ToSingle(vertex, floatSize), (double)BitConverter.ToSingle(vertex, floatSize * 2)));
                }
                reader.ReadBytes(ushortSize);
                parser.SetFacet(facet);
            }
        }

        private void ValidateSTLType(Stream stream)
        {
            byte[] data = new byte[binarySize];
            stream.Seek(80, SeekOrigin.Begin);
            /*int binaryRead = */ stream.Read(data, 0, binarySize);
            triangles = (int)BitConverter.ToUInt32(data, 0);
            stream.Seek(0, SeekOrigin.Begin);

            if (triangles * 50 + 84 != stream.Length)
                throw new Exception("Invalid STL file format");
        }

        public override void Write(string path, BaseParser parser)
        {
            if (!CanStream(path))
                throw new Exception("Invalid STL file extension");

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Open)))
            {
                writer.Write(Encoding.ASCII.GetBytes(header), 0, 80);
                writer.Write((uint)parser.FacetCount);
                for(int i =0; i < parser.FacetCount; i++)
                {
                    Tuple<double, double, double>[] facet = parser.GetFacet(i);
                    for(int j = 0; j < 4; j++)
                    {
                        var element = facet.ElementAt(j);
                        writer.Write((float)element.Item1);
                        writer.Write((float)element.Item2);
                        writer.Write((float)element.Item3);
                    }
                    writer.Write((short)0);
                }
            }
        }
    }
}
