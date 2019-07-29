using System;
using System.Collections.Generic;

namespace Memory_Map_Builder.Enums
{

    //TODO Consider changing from enum to a class that has static values for
    //TODO each element.
    [Flags]
    public enum Element : ushort
    {
        None  = 0,

        White = 1,
        //White_2 = 2,
        //White_3 = 4,

        Black = 0x8,
        //Black_2 = 16,
        //Black_3 = 32,

        Red   = 0x40,
        //Red_2 = 128,
        //Red_3 = 256,

        Blue  = 0x200,
        //Blue_2 = 1024,
        //Blue_3 = 2048,

        Green = 0x1000,
        //Green_2 = 8192,
        //Green_3 = 16384
    }

    public static class ElementExtensionMethods
    {
        public static Elements GetElements(this Element element)
        {
            var elementDictionary = new Dictionary<Element, int>();
            foreach (Element color in Enum.GetValues(typeof(Element)))
            {
                if (color != Element.None)
                {
                    //Each color has 3 levels and each level is represented by a bit flag, each flag is double the previous value.
                    //                              Base Value +   Base Value   * 2 +   Base Value   * 2 * 2 = Value Representing all levels for that element.
                    //@TODO Possibly cache colorFlags value in a readonly static variable.
                    var colorFlags = (Element) ((ushort) color + (ushort) color * 2 + (ushort) color * 2 * 2);
                    var level      = 0;
                    // Copy element so we can
                    var tmpElement = element;
                    while ((tmpElement & colorFlags) != 0)
                    {
                        tmpElement -= color;
                        level++;
                    }
                    if(level > 0)
                        elementDictionary.Add(color, level);
                }
            }
            return elementDictionary;
        }
    }
    public class Elements
    {
        public Elements(Dictionary<Element, int> elements)
        {
            this.elements = elements;
        }
        private Dictionary<Element, int> elements;
        public int White => elements[Element.White];
        public int Black => elements[Element.Black];
        public int Red => elements[Element.Red];
        public int Blue => elements[Element.Blue];
        public int Green => elements[Element.Green];

        public static implicit operator Elements(Dictionary<Element, int> dict) => new Elements(dict);

        public override string ToString()
        {
            List<string> elementsList = new List<string>();
            foreach (var element in elements)
            {
                if (element.Value > 0)
                    elementsList.Add($"{element.Value} {element.Key}");
            }

            if (elementsList.Count <= 0)
                return "No Elemental Affinity";
            return string.Join(" :: ", elementsList);
        }
    }
}
