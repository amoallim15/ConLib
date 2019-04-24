using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.HalfEdgeDS
{
    public class PropertyList: ICloneable<PropertyList>
    {
        public List<IProperty> Properties { get; private set; }
        public string Name { get; private set; }

        public PropertyList(string name, List<IProperty> storage = null)
        {
            Name = name;
            Properties = storage;

            if (Properties == null)
                Properties = new List<IProperty>();
        }

        public bool IsSame(PropertyList other)
        {
            return Name.Equals(other.Name);
        }

        public PropertyList Clone()
        {
            PropertyList result = new PropertyList(Name);
            foreach (IProperty item in Properties)
                result.Properties.Add(item.Clone());
            return result;
        }

        public PropertyList EmptyClone()
        {
            return new PropertyList(Name);
        }
    }
}
