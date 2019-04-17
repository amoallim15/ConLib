using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConLib.IO
{
    public abstract class BaseStreamer
    {
        public abstract List<string> Extensions { get; }
        public abstract void Read(string path, BaseParser parser);
        public abstract void Write(string path, BaseParser parser);
        public bool CanStream(string path)
        {
            string ext = GetExtension(path);
            if (Extensions.Contains(ext))
                return true;
            return false;
        }
        public bool CheckExtension(string path, string extension)
        {
            string ext = GetExtension(path);
            if (ext.Equals(extension.ToUpper().Trim('.')))
                return true;
            return false;
        }

        public string GetExtension(string path)
        {
            return Path.GetExtension(path).ToUpper().Trim('.');
        }
    }
}
