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
        private bool tableCreated = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private char[,] InitializeTable()
        {
            string contentOfTable = Functions.AddContentToTable(Functions.RemoveWhiteSpace(Functions.RemoveSpecialChars(Functions.RemoveDiacritism(klic.Text.ToUpper()))));
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
            if (klic.Text.Length < 8)
            {
                MessageBox.Show("Šifrovací klíč musí mít aspoň 8 znaků");
            }
            else
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

                tableCreated = true;
                sifrovaciTabulka.ItemsSource = t.DefaultView;
            }
        }

        private void Zasifruj_Click(object sender, RoutedEventArgs e)
        {
            if (!tableCreated)
            {
                MessageBox.Show("Před začátkem šifrování je nutno prvně vytvořit šifrovací tabulku");
            }
            else if(otevrenyText.Text == "")
            {
                MessageBox.Show("Pro šifrování je nutno vyplnit pole Vstup pro text k šifrování");
            }
            else
            {
                string vstupniText = otevrenyText.Text;
                string replaceW = vstupniText.ToUpper().Replace("W", "V");
                string removeSpecialChars = Functions.RemoveSpecialChars(replaceW);
                string removeDiacritism = Functions.RemoveDiacritism(removeSpecialChars);
                Functions.SaveSpaces(removeDiacritism);
                string removeWhiteSpace = Functions.RemoveWhiteSpace(removeDiacritism);
                string fixDoubleChars = Functions.FixDoubleChars(removeWhiteSpace);
                string insertX = Functions.InsertX(fixDoubleChars);
                string textDvojice = Functions.MakeDoubles(insertX);
                dvojiceText.Text = textDvojice;

                string textSifrovani = insertX;
                string output = "";

                for (int i = 0; i < textSifrovani.Length; i += 2)
                {
                    char firstChar = textSifrovani[i];
                    char secondChar = textSifrovani[i + 1];

                    IndexesOf2DArray myIndexesOf2DArray1 = new IndexesOf2DArray(arrayTable, firstChar);
                    IndexesOf2DArray myIndexesOf2DArray2 = new IndexesOf2DArray(arrayTable, secondChar);
                    bool rowRule = myIndexesOf2DArray1.getRowIndex() == myIndexesOf2DArray2.getRowIndex();
                    bool columnRule = myIndexesOf2DArray1.getColumnIndex() == myIndexesOf2DArray2.getColumnIndex();
                    bool diagonalRule = myIndexesOf2DArray1.getRowIndex() != myIndexesOf2DArray2.getRowIndex() && myIndexesOf2DArray1.getColumnIndex    () != myIndexesOf2DArray2.getColumnIndex();
                    if (diagonalRule)
                    {
                        output += TableRules.DiagonalRule(firstChar, secondChar, arrayTable);
                    }               
                    else if (rowRule)
                    {
                        output += TableRules.RowRule(firstChar, secondChar, arrayTable, 'E');
                    }
                    else if (columnRule)
                    {
                        output += TableRules.ColumnRule(firstChar, secondChar, arrayTable, 'E');
                    }
                }
                string textWithSpecialSeqeunces = Functions.InsertSpecialSequences(output, 'E');
                zasifText.Text = Functions.MakeFifths(textWithSpecialSeqeunces);
            }

        }

        private void Desifruj_Click(object sender, RoutedEventArgs e)
        {
            if (zasifText.Text == "")
            {
                MessageBox.Show("Musíte nejdříve provést šifrování nebo vyplnit pole Zašifrovaný text");
            }
            else if(!tableCreated)
            {
                MessageBox.Show("Před začátkem dešifrování je nutno prvně vytvořit šifrovací tabulku");
            }
            else
            {
                var textDesif = Functions.SaveSpecialCharsPosition(Functions.RemoveWhiteSpace(zasifText.Text));
                string output = "";

                for (int i = 0; i < textDesif.Length; i += 2)
                {
                    char firstChar = textDesif[i];
                    char secondChar = textDesif[i + 1];

                    IndexesOf2DArray myIndexesOf2DArray1 = new IndexesOf2DArray(arrayTable, firstChar);
                    IndexesOf2DArray myIndexesOf2DArray2 = new IndexesOf2DArray(arrayTable, secondChar);
                    bool rowRule = myIndexesOf2DArray1.getRowIndex() == myIndexesOf2DArray2.getRowIndex();
                    bool columnRule = myIndexesOf2DArray1.getColumnIndex() == myIndexesOf2DArray2.getColumnIndex();
                    bool diagonalRule = myIndexesOf2DArray1.getRowIndex() != myIndexesOf2DArray2.getRowIndex() && myIndexesOf2DArray1.getColumnIndex() != myIndexesOf2DArray2.getColumnIndex();

                    if (diagonalRule)
                    {
                        output += TableRules.DiagonalRule(firstChar, secondChar, arrayTable);
                    }
                    else if (rowRule)
                    {
                        output += TableRules.RowRule(firstChar, secondChar, arrayTable, 'D');
                    }
                    else if (columnRule)
                    {
                        output += TableRules.ColumnRule(firstChar, secondChar, arrayTable, 'D');
                    }
                }
                var surovyDesifText = Functions.InsertSpecialSequences(output, 'D');
                surDesifText.Text = surovyDesifText;
                desifText.Text = Functions.RawToCorrectText(Functions.ReplaceSpecialSequences(surovyDesifText));
            }
            
        }
    }
}
