using System;
using System.Collections.Generic;
using System.Text;

namespace PlayfairovaSifraGUI
{
    class IndexesOf2DArray
    {
        private int rowIndex;
        private int columnIndex;
        private char[,] whole2dArray;
        private char searchedChar;

        public int getRowIndex()
        {
            return rowIndex;
        }

        private void setRowIndex(int index)
        {
            rowIndex = index;
        }

        public int getColumnIndex()
        {
            return columnIndex;
        }

        private void setColumnIndex(int index)
        {
            columnIndex = index;
        }

        private char[,] getWhole2dArray()
        {
            return whole2dArray;
        }

        private void setWhole2dArray(char[,] whole2dArray)
        {
            this.whole2dArray = whole2dArray;
        }

        public char getSearchedChar()
        {
            return searchedChar;
        }

        private void setSearchedChar(char searchedChar)
        {
            this.searchedChar = searchedChar;
        }

        public IndexesOf2DArray(char[,] whole2dArray, char searchedChar)
        {
            setWhole2dArray(whole2dArray);
            setSearchedChar(searchedChar);

            for (int i = 0; i < getWhole2dArray().GetLength(0); i++)
            {
                for (int j = 0; j < getWhole2dArray().GetLength(0); j++)
                {
                    if (getWhole2dArray()[i, j].Equals(getSearchedChar()))
                    {
                        setRowIndex(i);
                        setColumnIndex(j);
                        return;
                    }
                }
            }
            setRowIndex(-1);
            setColumnIndex(-1);
        }
    }
}
