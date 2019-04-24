using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.HalfEdgeDS
{
    public class PropertyContainer: ICloneable<PropertyContainer>
    {
        public List<PropertyList> PropertyLists { get; private set; }

        public PropertyContainer() { }

        public PropertyContainer Clone()
        {
            PropertyContainer result = new PropertyContainer();
            foreach(PropertyList list in PropertyLists)
            {
                result.PropertyLists.Add(list.Clone());
            }
            return result;
        }

        public void CopyFrom(PropertyContainer other)
        {
            foreach(PropertyList otherList in other.PropertyLists)
            {
                bool alreadyExist = false;
                foreach(PropertyList thisList in PropertyLists)
                {
                    if (otherList.IsSame(thisList))
                    {
                        alreadyExist = true;
                        break;
                    }
                }
                if (alreadyExist)
                    continue;
                PropertyLists.Add(otherList.EmptyClone());
            }
        }

        public List<string> PropertyListsNames()
        {
            return (from list in PropertyLists select list.Name).ToList();
        }
    }
}
