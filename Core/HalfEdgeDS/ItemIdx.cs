using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib.Core.HalfEdgeDS
{
    public interface IItemIdx: ICloneable
    {
        uint Idx { get; }
        void Reset();
        bool IsValid();
        void Increment(uint i = 1);
        void Decrement(uint i = 1);
    }

    public abstract class ItemIdx: IItemIdx
    {
        public uint Idx { get; private set; }
        public ItemIdx(uint idx = uint.MaxValue)
        {
            Idx = idx;
        }
        public void Reset()
        {
            Idx = uint.MaxValue;
        }
        public bool IsValid()
        {
            return Idx != uint.MaxValue;
        }
        public void Increment(uint i = 1)
        {
            Idx += i;
        }
        public void Decrement(uint i = 1)
        {
            Idx -= i;
        }

        public abstract object Clone();
    }

    public class VertexIdx : ItemIdx
    {
        public VertexIdx() : base() { }
        public VertexIdx(uint idx) : base(idx) { }
        public override object Clone()
        {
            return new VertexIdx(Idx);
        }
    }

    public class HalfEdgeIdx : ItemIdx
    {
        public HalfEdgeIdx() : base() { }
        public HalfEdgeIdx(uint idx) : base(idx) { }
        public override object Clone()
        {
            return new HalfEdgeIdx(Idx);
        }
    }

    public class FaceIdx : ItemIdx
    {
        public FaceIdx() : base() { }
        public FaceIdx(uint idx) : base(idx) { }
        public override object Clone()
        {
            return new FaceIdx(Idx);
        }
    }

    public class EdgeIdx: IItemIdx
    {
        public HalfEdgeIdx HalfEdgeIdx { get; private set; }
        public uint Idx
        {
            get { return HalfEdgeIdx.Idx / 2; }
        }
        public EdgeIdx()
        {
            HalfEdgeIdx = new HalfEdgeIdx();
        }
        public EdgeIdx(uint idx)
        {
            HalfEdgeIdx = new HalfEdgeIdx(idx * 2);
        }
        public EdgeIdx(HalfEdgeIdx halfEdge)
        {
            HalfEdgeIdx = halfEdge;
        }
        public void Reset()
        {
            HalfEdgeIdx.Reset();
        }
        public bool IsValid()
        {
            return HalfEdgeIdx.IsValid();
        }
        public void Increment(uint i = 1)
        {
            HalfEdgeIdx.Increment(i);
        }
        public void Decrement(uint i = 1)
        {
            HalfEdgeIdx.Decrement(i);
        }
        public object Clone()
        {
            return new EdgeIdx(Idx);
        }
    }
}
