using System;
using System.Runtime.InteropServices;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x04)]
    public struct Text
    {
        public uint Address;

        public string GetText(MemoryAccessor memoryAccessor) => memoryAccessor.DereferenceString(Address);

        public bool SetText(MemoryAccessor memoryAccessor, ref string value)
        {
            var text = memoryAccessor.DereferenceString(Address);
            if (value.Length > text.Length)
                return false;
            if (value.Length == text.Length)
            {
                text = value;
                return true;
            }
            if (value.Length < text.Length)
            {
                var difference = text.Length - value.Length;
                text = value.PadRight(difference);
                return true;
            }
            //TODO Finish Writing Text Back
            return false;
        }

        public static implicit operator uint(Text textData) => textData.Address;
        public static implicit operator Text(uint address) => new Text() {Address = address};
    }
}