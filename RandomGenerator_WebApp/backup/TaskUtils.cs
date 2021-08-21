using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

namespace RandomGenerator_WebApp
{
    class TaskUtils : System.Web.UI.Page
    {
        public static int RandomNumber(Random random, int minValue, int maxValue)
        {
            int number = random.Next(maxValue);
            return number;
        }

        public static string FormatNameAndSurname(Random random, string name, string surname, int caseNo)
        {
            const int CE = 5;

            string fLetters = "ėa";
            string eVowels = "aąčęeėįšųuūioy";

            string[] surnameEnds = new string[CE];
            surnameEnds[0] = "aitė";
            surnameEnds[1] = "ytė";
            surnameEnds[2] = "utė";
            surnameEnds[3] = "iūtė";
            surnameEnds[4] = "ūtė";

            if (name.Length - 1 >= 0 && (fLetters.IndexOf(name[name.Length - 1]) >= 0 || (fLetters.ToUpper()).IndexOf(name[name.Length - 1]) >= 0))
            {
                for (int i = surname.Length - 2; i >= 0; i--)
                {
                    if (eVowels.IndexOf(surname[i]) == -1)
                    {
                        int removeIndex = i + 1;
                        surname = surname.Remove(removeIndex);
                        int endNumber = RandomNumber(random, 0, CE - 1);
                        surname += surnameEnds[endNumber];
                        break;
                    }
                }
            }
            string line = "";
            switch (caseNo)
            {
                case 0:
                    line = String.Format("{0} {1}", name, surname);
                    break;
                case 1:
                    line = String.Format("{0} {1}", surname, name);
                    break;
            }

            return line;
        }

        public static void AddToLine(Random random, DropDownList a, List<string> names, List<string> surnames, int caseNo, ref string line)
        {
            switch (caseNo)
            {
                case 0:
                    if (a.SelectedValue == "Name")
                    {
                        string name = names[RandomNumber(random, 0, names.Count)];
                        line += (name + " ");
                        return;
                    }

                    if (a.SelectedValue == "Surname")
                    {
                        string surname = surnames[RandomNumber(random, 0, surnames.Count)];
                        line += (surname + " ");
                        return;
                    }

                    if (a.SelectedValue == "Digit")
                    {
                        int digit = RandomNumber(random, 0, 99999);
                        line += (digit.ToString() + " ");
                        return;
                    }

                    if (a.SelectedValue == "None")
                    {
                        return;
                    }
                    break;

                case 1:
                    if (a.SelectedValue == "Surname")
                    {
                        string name = names[RandomNumber(random, 0, names.Count)];
                        string surname = surnames[RandomNumber(random, 0, surnames.Count)];
                        line += (FormatNameAndSurname(random, name, surname, 0) + " ");
                        return;
                    }
                    if (a.SelectedValue == "Name")
                    {
                        string name = names[RandomNumber(random, 0, names.Count)];
                        string surname = surnames[RandomNumber(random, 0, surnames.Count)];
                        line += (FormatNameAndSurname(random, name, surname, 1) + " ");
                        return;
                    }
                    break;
            }

        }

        public static string FormLine(Random random, DropDownList a, DropDownList b, DropDownList c, List<string> names, List<string> surnames)
        {
            string line = "";
            DropDownList[] dropDownLists = new DropDownList[3];
            dropDownLists[0] = a;
            dropDownLists[1] = b;
            dropDownLists[2] = c;
            for (int i = 0; i < 3; i++)
            { 
                if (i != 2 && dropDownLists[i].SelectedValue == "Name" && dropDownLists[i + 1].SelectedValue == "Surname")
                {
                    AddToLine(random, dropDownLists[i + 1], names, surnames, 1, ref line);
                    if (i == 0)
                    {
                        i = 2;
                    }

                    else
                    {
                        break;
                    }
                }

                if (i != 2 && dropDownLists[i].SelectedValue == "Surname" && dropDownLists[i + 1].SelectedValue == "Name")
                {
                    AddToLine(random, dropDownLists[i + 1], names, surnames, 1, ref line);
                    if (i == 0)
                    {
                        i = 2;
                    }
                    
                    else
                    {
                        break;
                    }
                } 

                else
                {
                    AddToLine(random, dropDownLists[i], names, surnames, 0, ref line);
                }
            }
            line.TrimEnd();
            return line;
        }

        public static List<string> CollectLines(Random random, DropDownList a, DropDownList b, DropDownList c, List<string> names, List<string> surnames, int n)
        {
            List<string> AllLines = new List<string>();
            for (int i = 0; i < n; i++)
            {
                AllLines.Add(FormLine(random, a, b, c, names, surnames));
            }
            return AllLines;
        }

        public static bool CheckCheckbox(CheckBox checkBox)
        {
            if (checkBox.Checked == true)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

    }
}
