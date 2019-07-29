using System;
using System.Buffers;

namespace Memory_Map_Builder
{
    public class DataTypeMemoryManager<T> : MemoryManager<T> where T : struct
    {
        public override Span<T> GetSpan()
        {
            return default;
        }

        public override MemoryHandle Pin(int elementIndex = 0)
        {
            return default;
        }

        public override void Unpin() { }
        protected override void Dispose(bool disposing) { }
    }
}