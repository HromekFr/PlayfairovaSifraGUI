using System;
using System.Linq;
using System.Text;

namespace PlayfairovaSifraGUI
{
    public class Functions
    {
        private static string savedSpaces = "";
        private static string savedSpecialChars = "";

       private static string RemoveDiacritism(string Text)
        {
            string stringFormD = Text.Normalize(NormalizationForm.FormD);
            StringBuilder retVal = new StringBuilder();
            for (int index = 0; index < stringFormD.Length; index++)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stringFormD[index]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    char nextChar = stringFormD[index];
                    retVal.Append(nextChar);
                }
                    
            }
            return retVal.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string RemoveSpecialChars(string str)
        {
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", ";", "_", "(", ")", ":", "|", "[", "]", "?", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }

        public static string MakeDoubles(string str)
        {
            for (int i = 2; i <= str.Length; i += 2)
            {
                str = str.Insert(i, " ");
                i++;
            }

            return str;
        }

        private static string FixDoubleChars(string str)
        {
            for (int i = 0; i <= str.Length; i++)
            {
                if (i < str.Length - 1)
                {
                    if (str[i].Equals(str[i + 1]))
                    {
                        if (str[i] == 'X' && str[i + 1] == 'X')
                        {
                            savedSpaces = savedSpaces.Insert(i + 1, "P");
                            str = str.Insert(i + 1, "Q");
                        }
                        else if (str[i] == 'Q' && str[i + 1] == 'Q')
                        {
                            savedSpaces = savedSpaces.Insert(i + 1, "P");
                            str = str.Insert(i + 1, "W");
                        }
                        else
                        {
                            savedSpaces = savedSpaces.Insert(i + 1, "P");
                            str = str.Insert(i + 1, "X");
                        }

                    }
                }
            }

            return str;
        }

        private static string InsertX(string str)
        {
            if (str.Length % 2 != 0)
            {
                if (str[str.Length - 1] == 'X')
                {
                    str += 'Q';
                }
                else
                {
                    str += 'X';
                }
                savedSpaces = savedSpaces.Insert(savedSpaces.Length, "P");
                return str;

            }
            else
            {
                return str;
            }
        }
        private static string RemoveWhiteSpace(string str)
        {
            string output = str.Replace(" ", "");
            return output;
        }

        public static string AddContentToTable(string str)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
            string output = alphabet.Insert(0, str);
            string outputDistinct = new String(output.Distinct().ToArray());

            return outputDistinct;
        }



        public static string MakeFifths(string str)
        {
            for (int i = 5; i <= str.Length; i += 5)
            {
                str = str.Insert(i, " ");
                i++;
            }
            return str;
        }

        private static void SaveSpaces(string text)
        {
            savedSpaces = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsWhiteSpace(text[i]))
                {
                    savedSpaces += "M";
                }
                else
                {
                    savedSpaces += "P";
                }
            }
        }

        private static string ReturnSpaces(string text)
        {
            int correctionIndex = 0;
            string output = "";
            for (int i = 0; i < text.Length + correctionIndex; i++)
            {
                if (savedSpaces[i] == 'P')
                {
                    output += text[i - correctionIndex];
                }
                else if(savedSpaces[i] == 'M')
                {
                    output += ' ';
                    correctionIndex++;
                }
            }
            savedSpaces = "";
            return output;
        }

        public static string InsertSpecialSequences(string text, char encryptOrDecrypt)
        {
            string output = "";
            int correctionIndex;

            if (encryptOrDecrypt == 'E')
            {
                correctionIndex = 0;
                for (int i = 0; i < savedSpaces.Length; i++)
                {
                    if (savedSpaces[i] == 'M')
                    {
                        output += "GHF";
                        correctionIndex++;

                    }
                    else if (savedSpaces[i] == 'P')
                    {
                        output += text[i - correctionIndex];
                    }

                }
                savedSpaces = "";
                return output;
            }
            else if (encryptOrDecrypt == 'D')
            {
                correctionIndex = 0;
                for (int i = 0; i < savedSpecialChars.Length; i++)
                {
                    if (savedSpecialChars[i] == 'M')
                    {
                        output += "GHF";
                        correctionIndex++;
                    }
                    else if (savedSpecialChars[i] == 'P')
                    {
                        output += text[i - correctionIndex];
                    }
                }
                savedSpecialChars = "";
                return output;
            }

            return output;
        }

        public static string ReplaceSpecialSequences(string text)
        {
            string output = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (i <= text.Length - 2)
                {
                    bool isSpace = text[i] == 'G' && text[i + 1] == 'H' && text[i + 2] == 'F';

                    if (isSpace)
                    {
                        output += " ";
                        i += 2;
                    }
                    else
                    {
                        output += text[i];
                    }
                }
                else
                {
                    output += text[i];
                }
            }

            return output;
        }

        private static string SaveSpecialCharsPosition(string text)
        {
            string output = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (i <= text.Length - 2)
                {
                    bool isSpace = text[i] == 'G' && text[i + 1] == 'H' && text[i + 2] == 'F';

                    if (isSpace)
                    {
                        savedSpecialChars += "M";
                        i += 2;
                    }
                    else
                    {
                        savedSpecialChars += "P";
                        output += text[i];
                    }
                }
                else
                {
                    savedSpecialChars += "P";
                    output += text[i];
                }
            }

            return output;
        }

        public static string RawToCorrectText(string text)
        {
            string output = "";
            Functions.SaveSpaces(text);
            string input = RemoveWhiteSpace(text);
            for (int i = 0; i < input.Length; i += 2)
            {
                char firstChar = input[i];
                char secondChar = input[i + 1];

                if (firstChar != 'X' && secondChar != 'X')
                {
                    output += $"{firstChar}{secondChar}";
                }
                else if (firstChar != 'X' && secondChar == 'X')
                {
                    output += firstChar;
                }
                else if (firstChar == 'X' && secondChar == 'Q')
                {
                    output += firstChar;
                }
                else if (firstChar == 'X' && secondChar != 'X')
                {
                    output += secondChar;
                }
                

            }
            return Functions.ReturnSpaces(output);
        }

        public static string CorrectInput(string text, char inputType)
        {
            if (inputType == 'E')
            {
                string replaceW = text.ToUpper().Replace("W", "V");
                string removeSpecialChars = RemoveSpecialChars(replaceW);
                string removeDiacritism = RemoveDiacritism(removeSpecialChars);
                Functions.SaveSpaces(removeDiacritism);
                string removeWhiteSpace = RemoveWhiteSpace(removeDiacritism);
                string fixDoubleChars = FixDoubleChars(removeWhiteSpace);
                string insertX = InsertX(fixDoubleChars);
                return insertX;
            }
            else if (inputType == 'D')
            {
                string removeWhiteSpace = RemoveWhiteSpace(text.ToUpper()); ;
                string saveSpecialChars = SaveSpecialCharsPosition(removeWhiteSpace);
                return saveSpecialChars;
            }
            else if (inputType == 'C')
            {
                string removeSpecialChars = RemoveSpecialChars(text.ToUpper());
                string removeDiacritism = RemoveDiacritism(removeSpecialChars);
                string removeWhiteSpace = RemoveWhiteSpace(removeDiacritism);
                string outputDistinct = AddContentToTable(removeWhiteSpace);
                return outputDistinct;
            }
            else if (inputType == 'K')
            {
                string outputDistinct = new String(text.ToUpper().Distinct().ToArray());
                string removeSpecialChars = RemoveSpecialChars(text.ToUpper());
                string removeDiacritism = RemoveDiacritism(removeSpecialChars);
                string removeWhiteSpace = RemoveWhiteSpace(removeDiacritism);
                return removeWhiteSpace;
            }
            return text;
        }
    }

}
