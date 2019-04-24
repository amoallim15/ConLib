using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.HalfEdgeDS
{
    public abstract class BaseProperties
    {
        public string Name { get; private set; }

        public BaseProperties(string name)
        {
            Name = name;
        }
        public bool IsSame(BaseProperties other)
        {
            return Name.Equals(other.Name);
        }
        public abstract void Reserve(uint n);
        public abstract void Resize(uint n);
        public abstract void ShrinkToFit();
        public abstract void PushBack();
        public abstract void Reset(uint idx);
        public abstract bool Transfer(BaseProperties other);
        public abstract bool Transfer(BaseProperties other, uint from, uint to);
        public abstract void Swap(uint i0, uint i1);
        public abstract BaseProperties Clone();
    }
}
