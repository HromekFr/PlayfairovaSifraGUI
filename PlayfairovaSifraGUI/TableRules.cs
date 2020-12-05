using System;
using System.Collections.Generic;
using System.Text;

namespace PlayfairovaSifraGUI
{
    public class TableRules
    {
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
    }
}
