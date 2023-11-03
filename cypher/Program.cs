using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace cypher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string cypher = "kCmIgFi6GUJNgkNI1Q41fbfyLoCFTCvIqkZilOKIAXAzP1U1uy1BE4UfPBfpKmmLObjYnQNRBaPtKiVWzc5A4v0w3xle8F0hAGJZ7g4in0wndJxMOv03dc1M82at2T6935roTqyWDgtGD/hwwRF30HqFM5Vcw1JtINbsgWRm404/quEDkZ7x1B275bX3/Fo1";
            string key = "TheGiant";
            int size = cypher.Length / key.Length;
            char[,] cypherArray = new char[size, key.Length];

            for(int i = 0, k = 0; i < key.Length; i++)
            {
                for(int j = 0; j < size; ++j)
                {
                    cypherArray[j, i] = cypher[k];
                    k++;
                }
            }

            char[,] keyArray = new char[size + 1, key.Length];
            for (int i = 0; i < key.Length; i++)
                keyArray[0, i] = key[i];

            for (int i = 1; i < size; i++)
            {
                for (int j = 0; j < key.Length; ++j)
                {
                    keyArray[i, j] = cypherArray[i - 1, j];
                }
            }

            Console.WriteLine();
            PrintCypher(keyArray);

            
            Console.WriteLine();

            string[] cypherStrings = GetArray(keyArray);


            for (int i = 0; i < cypherStrings.Length; i++)
            {
                for (int j = 0; j < cypherStrings.Length - 1; j++)
                {
                    if (needToReOrder(cypherStrings[j], cypherStrings[j + 1]))
                    {
                        string s = cypherStrings[j];
                        cypherStrings[j] = cypherStrings[j + 1];
                        cypherStrings[j + 1] = s;
                    }
                }
            }

            Console.WriteLine();

            string[] finalCypher = new string[cypherStrings[0].Length];

            StringBuilder sb = new StringBuilder();

            for(int i = 1; i < finalCypher.Length; i++)
            {
                for(int j = 0; j < cypherStrings.GetLength(0); j++)
                {
                    sb.Append(cypherStrings[j].ToCharArray()[i]);
                }
                finalCypher[i] = sb.ToString();
                sb.Clear();
            }

            PrintStrings(finalCypher);
            
            Console.ReadKey();
        }

        private static bool needToReOrder(string s1, string s2)
        {
            for (int i = 0; i < (s1.Length > s2.Length ? s2.Length : s1.Length); i++)
            {
                if (s1.ToCharArray()[i] < s2.ToCharArray()[i]) return false;
                if (s1.ToCharArray()[i] > s2.ToCharArray()[i]) return true;
            }
            return false;
        }

        private static void PrintCypher(char[,] cypher)
        {
            for (int i = 0; i < cypher.GetLength(0); i++)
            {
                for(int j = 0; j < cypher.GetLength(1); j++)
                {
                    Console.Write(cypher[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static string[] GetArray(char[,] cypher)
        {
            string[] temp = new string[cypher.GetLength(1)];

            for(int i = 0; i < cypher.GetLength(1); i++)
            {
                temp[i] = GetString(cypher, i);
            }
            return temp;
        }

        private static string GetString(char[,] cypher, int row)
        {
            char[] characters = new char[cypher.GetLength(0)];

            for(int i = 0; i < cypher.GetLength(0); i++)
            {
                characters[i] = cypher[i, row];
            }
            return String.Concat(characters);
        }

        private static void PrintStrings(string[] strings)
        {
            for(int i = 0; i < strings.Length;  i++)
            {
                Console.WriteLine(strings[i]);
            }
        }
    }
}
