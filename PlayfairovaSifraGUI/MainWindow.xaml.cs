using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlayfairovaSifraGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char[,] arrayTable;
        private bool spaceSecondChar = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private char[,] InitializeTable()
        {
            string contentOfTable = Functions.AddContentToTable(Functions.RemoveWhiteSpace(klic.Text.ToUpper()));
            arrayTable = new char[5, 5];

            for (int i = 0; i < arrayTable.GetLength(0); i++)
            {
                for (int j = 0; j < arrayTable.GetLength(1); j++)
                {
                    arrayTable[i, j] = Convert.ToChar(contentOfTable.Substring(arrayTable.GetLength(0) * i + j, 1));
                }
            }

            return arrayTable;
        }

        private void VytvorTabulku_Click(object sender, RoutedEventArgs e)
        {
            arrayTable = InitializeTable();

            var t = new DataTable();
            int columns = arrayTable.GetLength(0);
            int rows = arrayTable.GetLength(1);

            for (var c = 0; c < columns; c++)
            {
                t.Columns.Add(new DataColumn());
            }

            for (var r = 0; r < rows; r++)
            {
                var newRow = t.NewRow();
                for (var c = 0; c < columns; c++)
                {
                    newRow[c] = arrayTable[r, c];
                }
                t.Rows.Add(newRow);
            }

            sifrovaciTabulka.ItemsSource = t.DefaultView;

        }

        private void Zasifruj_Click(object sender, RoutedEventArgs e)
        {
            string vstupniText = otevrenyText.Text;
            Functions.SaveSpaces(vstupniText);
            string replaceW = vstupniText.ToUpper().Replace("W", "V");
            string removeSpecialChars = Functions.RemoveSpecialChars(replaceW);
            string removeDiacritism = Functions.RemoveDiacritism(removeSpecialChars);
            string removeWhiteSpace = Functions.RemoveWhiteSpace(removeDiacritism);
            string insertX = Functions.InsertX(removeWhiteSpace);
            string returnSpaces = Functions.ReturnSpaces(insertX);
            string textDvojice = Functions.Double(insertX);
            dvojiceText.Text = textDvojice;

            string textSifrovani = Functions.InsertX(Functions.RemoveWhiteSpace(Functions.RemoveDiacritism(Functions.RemoveSpecialChars(vstupniText.ToUpper().Replace("W", "V")))));
            //Do for cyklu posilat text s mezerami a koretne pridanym X, aby se mohlo dat pridat spravne + GHF
            string output = "";

            for (int i = 0; i < textSifrovani.Length; i += 2)
            {
                if (spaceSecondChar)
                {
                    int correctSpacePosition = i - 3;
                    output = output.Insert(correctSpacePosition, "GHF");
                    spaceSecondChar = false;
                }

                char firstChar = textSifrovani[i];
                char secondChar = textSifrovani[i + 1];

                if (Char.IsWhiteSpace(firstChar))
                {
                    output += "GHF";
                    firstChar = secondChar;
                    secondChar = textSifrovani[i + 2];
                    i += 2;
                }
                else if (Char.IsWhiteSpace(secondChar))
                {
                    secondChar = textSifrovani[i + 2];
                    spaceSecondChar = true;
                    i += 2;
                }

                IndexesOf2DArray myIndexesOf2DArray1 = new IndexesOf2DArray(arrayTable, firstChar);
                IndexesOf2DArray myIndexesOf2DArray2 = new IndexesOf2DArray(arrayTable, secondChar);
                bool rowRule = myIndexesOf2DArray1.getRowIndex() == myIndexesOf2DArray2.getRowIndex();
                bool columnRule = myIndexesOf2DArray1.getColumnIndex() == myIndexesOf2DArray2.getColumnIndex();
                bool diagonalRule = myIndexesOf2DArray1.getRowIndex() != myIndexesOf2DArray2.getRowIndex() && myIndexesOf2DArray1.getColumnIndex() != myIndexesOf2DArray2.getColumnIndex();
                if (diagonalRule)
                {
                    output += Functions.DiagonalRule(firstChar, secondChar, arrayTable);
                }
                else if (rowRule)
                {
                    output += Functions.RowRule(firstChar, secondChar, arrayTable, 'E');
                }
                else if (columnRule)
                {
                    output += Functions.ColumnRule(firstChar, secondChar, arrayTable, 'E');
                }
            }

            zasifText.Text = Functions.Fifths(output);

        }

        private void Desifruj_Click(object sender, RoutedEventArgs e)
        {
            var textDesif = Functions.RemoveWhiteSpace(zasifText.Text);

            string output = "";

            for (int i = 0; i < textDesif.Length; i += 2)
            {
                bool isGHF = textDesif[i] == 'G' && textDesif[i + 1] == 'H' && textDesif[i + 2] == 'F';
                if (isGHF)
                {
                    output += ' ';
                    i++;

                    continue;
                }

                char firstChar = textDesif[i];
                char secondChar = textDesif[i + 1];

                IndexesOf2DArray myIndexesOf2DArray1 = new IndexesOf2DArray(arrayTable, firstChar);
                IndexesOf2DArray myIndexesOf2DArray2 = new IndexesOf2DArray(arrayTable, secondChar);
                bool rowRule = myIndexesOf2DArray1.getRowIndex() == myIndexesOf2DArray2.getRowIndex();
                bool columnRule = myIndexesOf2DArray1.getColumnIndex() == myIndexesOf2DArray2.getColumnIndex();
                bool diagonalRule = myIndexesOf2DArray1.getRowIndex() != myIndexesOf2DArray2.getRowIndex() && myIndexesOf2DArray1.getColumnIndex() != myIndexesOf2DArray2.getColumnIndex();

                if (diagonalRule)
                {
                    output += Functions.DiagonalRule(firstChar, secondChar, arrayTable);
                }
                else if (rowRule)
                {
                    output += Functions.RowRule(firstChar, secondChar, arrayTable, 'D');
                }
                else if (columnRule)
                {
                    output += Functions.ColumnRule(firstChar, secondChar, arrayTable, 'D');
                }
            }

            surDesifText.Text = output;
            desifText.Text = output.Replace("X", "");
        }
    }
}
