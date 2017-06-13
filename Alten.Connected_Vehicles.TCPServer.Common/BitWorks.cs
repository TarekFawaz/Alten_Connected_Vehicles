using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPServer.Common
{
    /// <summary>
    /// Common Uitlities for common bit operations, upon reciving devices packets 
    /// </summary>
    public class BitWorks
    {
        /// <summary>
        /// Seprarte the array of bytes based on a seprator and return the last part of it
        /// </summary>
        /// <param name="source">the source Bytes</param>
        /// <param name="separator">seprator represented as array of bytes for flexibility</param>
        /// <returns></returns>
        byte[] SeparateAndGetLast(byte[] source, byte[] separator)
        {
            for (var i = 0; i < source.Length; ++i)
            {
                if (Equals(source, separator, i)) // Equals method used to compare Source bytes as start index to the separartor
                {
                    var index = i + separator.Length; //  Calculate the Actual Index of the last Part
                    var part = new byte[source.Length - index]; // Intialize The Part Array Size 
                    Array.Copy(source, index, part, 0, part.Length); // Copy the last Part 
                    return part;
                }
            }
            throw new Exception("Separator Not found"); // Throw and expception if separator not found within the source
            // TODO: Implement Specified Exception 
        }

        /// <summary>
        /// Sperarte the source array of bytes into list of arrays without including the separartor
        /// </summary>
        /// <param name="source">Array of bytes representing the source</param>
        /// <param name="separator">Array of bytes representing separartor</param>
        /// <returns></returns>
        public static List<byte[]> Separate(byte[] source, byte[] separator)
        {
            var Parts = new List<byte[]>();
            var Index = 0;
            byte[] Part;
            for (var I = 0; I < source.Length; ++I)
            {
                if (Equals(source, separator, I)) 
                {
                    int startindex = I - Index; // Calculae start index for 
                    if (startindex > 0)
                    {
                        Part = new byte[startindex];
                        Array.Copy(source, Index, Part, 0, Part.Length);
                        Parts.Add(Part);
                        Index = I;
                        I += separator.Length - 1;
                    }
                }
            }
            Part = new byte[source.Length - Index]; // the Last Part
            Array.Copy(source, Index, Part, 0, Part.Length);
            Parts.Add(Part);
            return Parts;
        }

        /// <summary>
        /// Holde comparing technique 
        /// </summary>
        /// <param name="source">the source byte Array</param>
        /// <param name="separator">Separator bytes</param>
        /// <param name="index">the Start index at the Source</param>
        /// <returns></returns>
        private static bool Equals(byte[] source, byte[] separator, int index)
        {
            //TODO: Impelemnting XORing instead of using loop and If
            for (int i = 0; i < separator.Length; ++i)
                if (index + i >= source.Length || source[index + i] != separator[i]) // Simple Compar technique :)
                    return false;
            return true;
        }

        /// <summary>
        /// Convert Hex strinng into byte array
        /// </summary>
        /// <param name="hex">hexadecimal data to be converted</param>
        /// <returns></returns>
        public static byte[] HexToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }


    }
}
