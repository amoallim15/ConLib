using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ConLib.IO
{
    public class STLStreamer : BaseStreamer
    {
        private readonly string _solidheaderCheck = @"solid\s+(?<Name>[^\r\n]+)?";
        private readonly string _solidfooterCheck = @"endsolid";
        private readonly string _facetHeaderCheck = @"facet\s+normal\s+(?<Normal>[^\r^\n]+)";
        private readonly string _facetFooterCheck = @"endfacet";
        private readonly string _loopHeaderCheck = @"outer loop";
        private readonly string _loopFooterCheck = @"endloop";
        private readonly string _vertexCheck = @"vertex";

        public enum STLType { STLA, STLB, STL }

        public override bool Read(string path, BaseParser importer)
        {
            bool result;
            STLType? fileType = null;
            if (CheckExtension(path, STLType.STLA.ToString()))
                fileType = STLType.STLA;
            else if (CheckExtension(path, STLType.STLB.ToString()))
                fileType = STLType.STLB;
            else if (CheckExtension(path, STLType.STL.ToString()))
                fileType = CheckSTLType(path);
            switch (fileType)
            {
                case STLType.STLA:
                    using (StreamReader reader = new StreamReader(path))
                        result = ReadSTLA(reader, importer);
                    break;
                case STLType.STLB:
                    using (StreamReader reader = new StreamReader(path))
                        result = ReadSTLB(path, importer);
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        private bool ReadSTLA(StreamReader reader, BaseParser parser)
        {
            Match headerOpenMatch = Regex.Match(reader.ReadLine().Trim(), _solidheaderCheck);
            if (!headerOpenMatch.Success)
                throw new Exception(string.Format("Invalid STL header, expected \"solid [name]\", but found \"{0}\".", headerOpenMatch.Value));

            parser.AddHeader(headerOpenMatch.Groups["Name"].Value.Trim());

            Match facetHeaderMatch = Regex.Match(reader.ReadLine().Trim(), _facetHeaderCheck);
            while (facetHeaderMatch.Success)
            {
                string normal = facetHeaderMatch.Groups["Normal"].Value.Trim();
                //List<int> vector = normal.Split(' ').
                //parser.AssignFacetNormal(normal.Split(' ').)

                //importer.AssignFacetNormal(facetHeaderMatch.Groups["Normal"].Value.Split(' '));
                //facet_line = facet_line.re
            }

            string headerCloseLine;
            if (!Regex.Match((headerCloseLine = reader.ReadLine().Trim()), _solidfooterCheck).Success)
                throw new Exception(string.Format("Invalid STL footer, expected \"endsolid\", but found \"{0}\"", headerCloseLine));

            return false;
        }

        private bool ReadSTLB(string path, BaseParser importer)
        {
            throw new NotImplementedException();
        }

        private STLType CheckSTLType(string path)
        {
            using(StreamReader reader = new StreamReader(path, true))
            {
                const string solid = "solid";
                byte[] buffer = new byte[5];
                string header = null;
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                reader.BaseStream.Read(buffer, 0, buffer.Length);

                header = Encoding.ASCII.GetString(buffer);

                if (solid.Equals(header, StringComparison.InvariantCultureIgnoreCase))
                    return STLType.STLA;
                return STLType.STLB;
            }
        }

        public override bool Read(StreamReader stream, BaseParser importer)
        {
            throw new NotImplementedException();
        }

        public override bool Write(string path, BaseParser exporter)
        {
            throw new NotImplementedException();
        }

        public override bool Write(StreamWriter stream, BaseParser exporter)
        {
            throw new NotImplementedException();
        }
    }
}
