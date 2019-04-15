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
        public enum Extensions { STL, CON };
        public BaseStreamer() { }
        
        public bool CanStream(string path)
        {
            string ext = Path.GetExtension(path).ToUpper();
            if (Enum.IsDefined(typeof(Extensions), ext))
                return true;
            return false;
        }

        public bool CheckExtension(string path, string extension)
        {
            string ext = Path.GetExtension(path).ToUpper();
            if (ext.Equals(extension.ToUpper()))
                return true;
            return false;
        }

        public abstract bool Read(string path, BaseParser parser);

        public abstract bool Read(StreamReader stream, BaseParser parser);

        public abstract bool Write(string path, BaseParser parser);

        public abstract bool Write(StreamWriter stream, BaseParser parser);
    }
}
