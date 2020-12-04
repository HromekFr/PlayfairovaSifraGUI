using System;
using System.Linq;
using System.Text;

namespace PlayfairovaSifraGUI
{
    public class Functions
    {

        public static string RemoveDiacritism(string Text)
        {
            string stringFormD = Text.Normalize(NormalizationForm.FormD);
            StringBuilder retVal = new StringBuilder();
            for (int index = 0; index < stringFormD.Length; index++)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stringFormD[index]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    retVal.Append(stringFormD[index]);
            }
            return retVal.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveSpecialChars(string str)
        {
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", ";", "_", "(", ")", ":", "|", "[", "]", "?" };
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }

        public static string Double(string str)
        {
            for (int i = 2; i <= str.Length; i += 2)
            {
                str = str.Insert(i, " ");
                i++;
            }

            return str;
        }

        public static string InsertX(string str)
        {
            for (int i = 0; i <= str.Length; i++)
            {
                if (i < str.Length - 1)
                {
                    if (str[i].Equals(str[i + 1]))
                    {
                        str = str.Insert(i + 1, "X");
                    }
                }
            }

            if (str.Length % 2 != 0)
            {
                if (str[str.Length -1] == 'X')
                {
                    str += 'Q';
                }
                else
                {
                    str += 'X';
                }
                
            }
            return str;
        }
        public static string RemoveWhiteSpace(string str)
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

        public static string DiagonalRule(char firstChar, char secondChar, char[,] arrayTable)
        {
            string output = "";

            IndexesOf2DArray myIndexesOf2DArray1 = new IndexesOf2DArray(arrayTable, firstChar);
            IndexesOf2DArray myIndexesOf2DArray2 = new IndexesOf2DArray(arrayTable, secondChar);

            if (myIndexesOf2DArray1.getRowIndex() != myIndexesOf2DArray2.getRowIndex() && myIndexesOf2DArray1.getColumnIndex() != myIndexesOf2DArray2.getColumnIndex())
            {
                char firstCharEncrypted =
                arrayTable[myIndexesOf2DArray1.getRowIndex(), myIndexesOf2DArray2.getColumnIndex()];

                char secondCharEncrypted =
                    arrayTable[myIndexesOf2DArray2.getRowIndex(), myIndexesOf2DArray1.getColumnIndex()];

                output = $"{firstCharEncrypted}{secondCharEncrypted}";
            }

            return output;
        }

        public static string RowRule(char firstChar, char secondChar, char[,] arrayTable, char decryptOrEncrypt)
        {
            string output = "";

            IndexesOf2DArray myIndexesOf2DArray1 = new IndexesOf2DArray(arrayTable, firstChar);
            IndexesOf2DArray myIndexesOf2DArray2 = new IndexesOf2DArray(arrayTable, secondChar);

            if (decryptOrEncrypt == 'E')
            {
                char firstCharEncrypted =
                arrayTable[myIndexesOf2DArray1.getRowIndex(), (myIndexesOf2DArray1.getColumnIndex() + 1) % arrayTable.GetLength(1)];

                char secondCharEncrypted =
                    arrayTable[myIndexesOf2DArray2.getRowIndex(), (myIndexesOf2DArray2.getColumnIndex() + 1) % arrayTable.GetLength(1)];
                output = $"{firstCharEncrypted}{secondCharEncrypted}";
            }
            else if (decryptOrEncrypt == 'D')
            {
                char firstCharEncrypted =
                arrayTable[myIndexesOf2DArray1.getRowIndex(), (myIndexesOf2DArray1.getColumnIndex() - 1) % arrayTable.GetLength(1)];

                char secondCharEncrypted =
                    arrayTable[myIndexesOf2DArray2.getRowIndex(), (myIndexesOf2DArray2.getColumnIndex() - 1) % arrayTable.GetLength(1)];
                output = $"{firstCharEncrypted}{secondCharEncrypted}";
            }

            return output;
        }

        public static string ColumnRule(char firstChar, char secondChar, char[,] arrayTable, char decryptOrEncrypt)
        {
            string output = "";

            IndexesOf2DArray myIndexesOf2DArray1 = new IndexesOf2DArray(arrayTable, firstChar);
            IndexesOf2DArray myIndexesOf2DArray2 = new IndexesOf2DArray(arrayTable, secondChar);

            if (decryptOrEncrypt == 'E')
            {
                char firstCharEncrypted =
                arrayTable[(myIndexesOf2DArray1.getRowIndex() + 1) % arrayTable.GetLength(1), myIndexesOf2DArray1.getColumnIndex()];

                char secondCharEncrypted =
                    arrayTable[(myIndexesOf2DArray2.getRowIndex() + 1) % arrayTable.GetLength(1), myIndexesOf2DArray2.getColumnIndex()];

                output = $"{firstCharEncrypted}{secondCharEncrypted}";
            }
            else if (decryptOrEncrypt == 'D')
            {
                char firstCharEncrypted;
                char secondCharEncrypted;
                if (myIndexesOf2DArray1.getRowIndex() == 0)
                {
                    firstCharEncrypted =
                arrayTable[4, myIndexesOf2DArray1.getColumnIndex()];
                }
                else
                {
                    firstCharEncrypted =
                arrayTable[(myIndexesOf2DArray1.getRowIndex() - 1) % arrayTable.GetLength(1), myIndexesOf2DArray1.getColumnIndex()];
                }

                if (myIndexesOf2DArray2.getRowIndex() == 0)
                {
                    secondCharEncrypted =
                    arrayTable[4, myIndexesOf2DArray2.getColumnIndex()];
                }
                else
                {
                    secondCharEncrypted =
                    arrayTable[(myIndexesOf2DArray2.getRowIndex() - 1) % arrayTable.GetLength(1), myIndexesOf2DArray2.getColumnIndex()];
                }


                output = $"{firstCharEncrypted}{secondCharEncrypted}";
            }

            return output;
        }

        public static string Fifths(string str)
        {
            for (int i = 5; i <= str.Length; i += 5)
            {
                str = str.Insert(i, " ");
                i++;
            }
            return str;
        }

    }
}
