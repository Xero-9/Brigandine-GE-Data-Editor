using System;
using System.Collections.Generic;
using BrigandineGEDataEditor.Enums;

namespace BrigandineGEDataEditor.DataTypes
{
    //TODO Create ElementData struct wih methods below and remove extension methods.

    public static class ElementExtensionMethods
    {
        public static Elements GetElements(this ElementEnum element)
        {
            var elementDictionary = new Dictionary<ElementEnum, int>();
            foreach (ElementEnum color in Enum.GetValues(typeof(ElementEnum)))
            {
                if (color != ElementEnum.None)
                {
                    //Each color has 3 levels and each level is represented by a bit flag, each flag is double the previous value.
                    //                              Base Value +   Base Value   * 2 +   Base Value   * 2 * 2 = Value Representing all levels for that element.
                    //@TODO Consider caching colorFlags value in a readonly static variable.
                    var colorFlags = (ElementEnum) ((ushort) color + (ushort) color * 2 + (ushort) color * 2 * 2);
                    var level = 0;

                    while ((element & colorFlags) != 0)
                    {
                        element -= color;
                        level++;
                    }

                    if (level > 0)
                        elementDictionary.Add(color, level);
                }
            }

            return elementDictionary;
        }
    }

    public class Elements
    {
        public Elements(Dictionary<ElementEnum, int> elements)
        {
            this.elements = elements;
        }

        private Dictionary<ElementEnum, int> elements;
        public int White => elements[ElementEnum.White];
        public int Black => elements[ElementEnum.Black];
        public int Red => elements[ElementEnum.Red];
        public int Blue => elements[ElementEnum.Blue];
        public int Green => elements[ElementEnum.Green];

        public static implicit operator Elements(Dictionary<ElementEnum, int> dict) => new Elements(dict);

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