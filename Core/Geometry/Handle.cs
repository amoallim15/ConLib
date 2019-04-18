using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.Geometry
{
    public class Handle
    {
        public int Id { get; set; }
        public Handle(int id = -1)
        {
            Id = id;
        }
        public bool IsValid()
        {
            return Id > 0;
        }
        public void Reset()
        {
            Id = -1;
        }
        public static bool operator ==(Handle lhs, Handle rhs)
        {
            return lhs.Id == rhs.Id;
        }
        public static bool operator !=(Handle lhs, Handle rhs)
        {
            return lhs.Id != rhs.Id;
        }

        public override bool Equals(object obj)
        {
            return Id == ((Handle)obj).Id;
        }
        public override int GetHashCode()
        {
            return Id;
        }
        protected void Increment()
        {
            Id++;
        }
        protected void Decrement()
        {
            Id--;
        }
        protected void Increment(int amount)
        {
            Id += amount;
        }
        protected void Decrement(int amount)
        {
            Id -= amount;
        }
    }
}
