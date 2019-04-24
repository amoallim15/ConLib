using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.HalfEdgeDS
{
    public interface ICloneable<T>
    {
        T Clone();
    }

    public interface IProperty: ICloneable<IProperty>
    {
    }
}
