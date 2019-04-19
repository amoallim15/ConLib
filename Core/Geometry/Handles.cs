using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.Geometry
{
    public abstract class BaseHandle
    {
        public int Id { get; set; }
        public BaseHandle(int id = -1)
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
        public static bool operator ==(BaseHandle lhs, BaseHandle rhs)
        {
            return lhs.Id == rhs.Id;
        }
        public static bool operator !=(BaseHandle lhs, BaseHandle rhs)
        {
            return lhs.Id != rhs.Id;
        }

        public override bool Equals(object obj)
        {
            return Id == ((BaseHandle)obj).Id;
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

    public class VertexHandle : BaseHandle
    {
        public VertexHandle(int id): base(id) { }
    }
    public class HalfEdgeHandle : BaseHandle
    {
        public HalfEdgeHandle(int id) : base(id) { }
    }
    public class FaceHandle : BaseHandle
    {
        public FaceHandle(int id) : base(id) { }
    }
}
