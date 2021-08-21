using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RandomGenerator_WebApp
{
    class InOutUtils
    {
        public static List<string> ReadFile(string[] AllLines)
        {
            List<string> AllWords = new List<string>();
            foreach (string line in AllLines)
            {
                string[] AllParts = line.Split(' ');
                foreach (string part in AllParts)
                {
                    AllWords.Add(part);
                }
            }
            return AllWords;
        }

        public static string[] FormLinesToWrite(bool enumeration, List<string> AllLines)
        {
            string[] WrittenLines = new string[AllLines.Count];
            for (int i = 0; i < AllLines.Count; i++)
            {
                string line = "";
                switch (enumeration)
                {
                    case true:
                        line = String.Format("{0}. {1}", i + 1, AllLines[i]);
                        break;
                    case false:
                        line = AllLines[i];
                        break;
                }
                WrittenLines[i] = line;
            }
            return WrittenLines;
        }

    }
}

