using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;

namespace RandomGenerator_WebApp
{
    class TaskUtils : System.Web.UI.Page
    {
        public static int RandomNumber(Random random, int minValue, int maxValue)
        {
            int number = random.Next(maxValue);
            return number;
        }

        public static string FormatNameAndSurname(Random random, string name, string surname, int caseNo, char punctuation)
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
                string copy = surname;
                //for (int i = surname.Length - 2; i >= 0; i--)
                //{
                //    if (eVowels.IndexOf(surname[i]) == -1)
                //    {
                //        int removeIndex = i + 1;
                //        surname = surname.Remove(removeIndex);
                //        int endNumber = RandomNumber(random, 0, CE - 1);
                //        surname += surnameEnds[endNumber];
                //        break;
                //    }
                //}
                if (surname.Length == 0) throw new Exception("tuscia");
                int i = surname.Length - 2;
                while (i >= 0 && eVowels.IndexOf(surname[i]) >= 0)
                {
                    i--;
                }
                int removeIndex = i + 1;
                surname = surname.Remove(removeIndex);
                int endNumber = RandomNumber(random, 0, CE - 1);
                surname += surnameEnds[endNumber];
            }
            string line = "";
            switch (caseNo)
            {
                case 0:
                    line = String.Format("{0}{1}{2}", name, punctuation, surname);
                    break;
                case 1:
                    line = String.Format("{0}{1}{2}", surname, punctuation, name);
                    break;
            }

            return line;
        }

        public static void AddToLine(Random random, DropDownList a, List<string> names, List<string> surnames, string customString, char punctuation, int caseNo, ref string line)
        {
            switch (caseNo)
            {
                case 0:
                    if (a.SelectedValue == "Name")
                    {
                        string name = names[RandomNumber(random, 0, names.Count)];
                        line += (name + punctuation);
                        return;
                    }

                    if (a.SelectedValue == "Surname")
                    {
                        string surname = surnames[RandomNumber(random, 0, surnames.Count)];
                        line += (surname + punctuation);
                        return;
                    }

                    if (a.SelectedValue == "Digit")
                    {
                        int digit = RandomNumber(random, 0, 99999);
                        line += (digit.ToString() + punctuation);
                        return;
                    }

                    if (a.SelectedValue == "Custom String")
                    {
                        line += (customString + punctuation);
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
                        line += (FormatNameAndSurname(random, name, surname, 0, punctuation) + punctuation);
                        return;
                    }
                    if (a.SelectedValue == "Name")
                    {
                        string name = names[RandomNumber(random, 0, names.Count)];
                        string surname = surnames[RandomNumber(random, 0, surnames.Count)];
                        line += (FormatNameAndSurname(random, name, surname, 1, punctuation) + punctuation);
                        return;
                    }
                    break;
            }

        }

        public static string FormLine(Random random, DropDownList a, DropDownList b, DropDownList c, List<string> names, List<string> surnames, string customString, char punctuation)
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
                    AddToLine(random, dropDownLists[i + 1], names, surnames, customString, punctuation, 1, ref line);
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
                    AddToLine(random, dropDownLists[i + 1], names, surnames, customString, punctuation, 1, ref line);
                    if (i == 0)
                    {
                        i = 2;
                    }
                    
                    else
                    {
                        break;
                    }
                }
                
                if (dropDownLists[i].SelectedValue == "Custom String")
                {
                    AddToLine(random, dropDownLists[i], names, surnames, customString, punctuation, 0, ref line);
                }

                else
                {
                    AddToLine(random, dropDownLists[i], names, surnames, customString, punctuation, 0, ref line);
                }
            }
            line.TrimEnd();
            return line;
        }

        public static List<string> CollectLines(Random random, DropDownList a, DropDownList b, DropDownList c, List<string> names, List<string> surnames, 
            char punctuation, int n, DropDownList[] dropDownLists, bool button2, bool tableVisibility, TextBox[] textBoxes, TextBox TextBox2)
        {
            List<string> AllLines = new List<string>();
            for (int i = 0; i < n; i++)
            {
                string customString = "";
                if (button2 && tableVisibility)
                {
                    int count = 0;
                    foreach (DropDownList dropDownList in dropDownLists)
                    {
                        if (WebUtils.CheckSelectedItem(dropDownList, "Letter"))
                        {
                            string letters = TaskUtils.FormRandomLetterLine(random);

                            customString += letters[random.Next(0, letters.Length)];
                        }

                        if (WebUtils.CheckSelectedItem(dropDownList, "Digit"))
                        {
                            customString += random.Next(0, 9).ToString();
                        }

                        if (WebUtils.CheckSelectedItem(dropDownList, "Constant"))
                        {
                            customString += textBoxes[count].Text;
                        }
                        count++;
                    }


                    if (TextBox2.Text != "")
                    {
                        punctuation = TextBox2.Text[0];
                    }
                }
                AllLines.Add(FormLine(random, a, b, c, names, surnames, customString, punctuation));
            }
            return AllLines;
        }

        public static string FormRandomLetterLine(Random random)
        {
            string letters = "qwertyuiopasdfghjklzxcvbnm" + ("qwertyuiopasdfghjklzxcvbnm").ToUpper();
            string randomLettersLine = "";

            int count = 0;
            while (count != letters.Length)
            {
                randomLettersLine += letters[random.Next(0, letters.Length)];
                count++;
            }

            return randomLettersLine;
        }



    }
}
