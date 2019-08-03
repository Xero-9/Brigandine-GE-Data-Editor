using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BrigandineGEDataEditor.DataTypes
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x04)]
    public struct TextData
    {
        public uint Address;

        public string GetText(MemoryAccessor memoryAccessor)
        {
            return memoryAccessor.DereferenceString(Address);
        }

        public bool SetText(MemoryAccessor memoryAccessor, ref string value)
        {
            var text = memoryAccessor.DereferenceString(Address);
            if (value.Length > text.Length)
            {
//                throw new Exception(
//                    "$value's length was greater than the current text. The new string must have a length equal" +
//                    $" to or shorter than value. Current: {text} New. {value}");
                
                return false;
            }
            else if(value.Length == text.Length)
            {
                text = value;
                return true;
            }
            else if (value.Length < text.Length)
            {
                var difference = text.Length - value.Length;
                text = value.PadRight(difference);
                return true;
            }

            return false;
        }

        public static implicit operator uint(TextData textData) => textData.Address;
        public static implicit operator TextData(uint address) => new TextData() { Address = address };
    }
}