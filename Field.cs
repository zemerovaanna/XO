using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;

namespace WpfAppXODemo
{
    public enum XOElement { None, X, O, Null }
    public class Field
    {
        private const int rowCount = 3, colCount = 3;
        static Color black = Color.FromRgb(0, 0, 0);
        static Color blue = Color.FromRgb(0, 0, 255);
        static Color red = Color.FromRgb(255, 0, 0);
        private XOElement[,] xofield;
        private Color[,] colors;
        private XOElement winner = XOElement.Null;
        private bool whoWalks;
        private bool gameOver;

        public XOElement Winner
        {
            get
            {
                return winner;
            }
        }

        public bool WhoWalks
        {
            get
            {
                return whoWalks;
            }
        }

        public bool GameOver
        {
            get
            {
                return gameOver;
            }
        }

        public char[,] XOField
        {
            get
            {
                char[,] result = new char[rowCount, colCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (xofield[i, j] == XOElement.X)
                        {
                            result[i, j] = 'X';
                        }
                        else if (xofield[i, j] == XOElement.O)
                        {
                            result[i, j] = 'O';
                        }
                        else if (xofield[i, j] == XOElement.None)
                        {
                            result[i, j] = '-';
                        }
                    }
                }
                return result;
            }
        }

        public Color[,] XOColor
        {
            get
            {
                return colors;
            }
        }

        public void NewGame()
        {
            xofield = new XOElement[rowCount, colCount]
            {
                { XOElement.None, XOElement.None, XOElement.None},
                { XOElement.None, XOElement.None, XOElement.None},
                { XOElement.None, XOElement.None, XOElement.None}
            };

            colors = new Color[rowCount, colCount]
            {
                { black, black, black},
                { black, black, black},
                { black, black, black}
            };

            winner = XOElement.Null;
            whoWalks = false;
            gameOver = false;
        }

        public void Walks(byte indexA, byte indexB)
        {
            if (xofield[indexA, indexB] == XOElement.None && gameOver == false && whoWalks == false)
            {
                xofield[indexA, indexB] = XOElement.X;
                whoWalks = true;
                CheckingWinner();
            }
            else if (xofield[indexA, indexB] == XOElement.None && gameOver == false && whoWalks == true)
            {
                xofield[indexA, indexB] = XOElement.O;
                whoWalks = false;
                CheckingWinner();
            }
        }

        private void CheckingWinner()
        {
            if (gameOver == false)
            {
                MethodVertical();
                MethodHorizontal();
                MethodForward();
                MethodBackSlash();

                byte count = 0;

                for (int i = 0; i < xofield.GetLength(0); i++)
                {
                    for (int j = 0; j < xofield.GetLength(1); j++)
                    {
                        if(xofield[i, j] != XOElement.None)
                        {
                            count++;
                        }
                    }
                }

                if (count == 9 && gameOver == false)
                {
                    winner = XOElement.None;
                    gameOver = true;
                }
            }
        }

        private void MethodVertical()
        {
            if (gameOver == false)
            {
                for (byte i = 0; i < xofield.GetLength(0); i++)
                {
                    byte countX = 0;
                    byte countO = 0;

                    for (byte j = 0; j < xofield.GetLength(1); j++)
                    {
                        if (xofield[i, j] == XOElement.X)
                        {
                            countX++;
                        }
                        else if (xofield[i, j] == XOElement.O)
                        {
                            countO++;
                        }
                    }

                    if (countO == 3 || countX == 3)
                    {
                        for (byte j = 0; j < xofield.GetLength(1); j++)
                        {
                            if (xofield[i, j] == XOElement.X)
                            {
                                winner = XOElement.X;
                                colors[i, j] = red;
                            }
                            else if (xofield[i, j] == XOElement.O)
                            {
                                winner = XOElement.O;
                                colors[i, j] = blue;
                            }
                        }

                        gameOver = true;
                    }
                }
            }
        }

        private void MethodHorizontal()
        {
            if (gameOver == false)
            {
                for (byte i = 0; i < xofield.GetLength(1); i++)
                {

                    byte countX = 0;
                    byte countO = 0;

                    for (byte j = 0; j < xofield.GetLength(0); j++)
                    {
                        if (xofield[j, i] == XOElement.X)
                        {
                            countX++;
                        }
                        else if (xofield[j, i] == XOElement.O)
                        {
                            countO++;
                        }
                    }

                    if (countO == 3 || countX == 3)
                    {
                        for (byte j = 0; j < xofield.GetLength(0); j++)
                        {
                            if (xofield[j, i] == XOElement.X)
                            {
                                winner = XOElement.X;
                                colors[j, i] = red;
                            }
                            else if (xofield[j, i] == XOElement.O)
                            {
                                winner = XOElement.O;
                                colors[j, i] = blue;
                            }
                        }

                        gameOver = true;
                    }
                }
            }
        }

        private void MethodBackSlash()
        {
            if (gameOver == false)
            {
                byte countX = 0;
                byte countO = 0;

                for (byte i = 0; i < xofield.GetLength(0); i++)
                {
                    if (xofield[i, i] == XOElement.X)
                    {
                        countX++;
                    }
                    else if (xofield[i, i] == XOElement.O)
                    {
                        countO++;
                    }
                }

                if (countO == 3 || countX == 3)
                {
                    for (int j = 0; j < xofield.GetLength(0); j++)
                    {
                        if (xofield[j, j] == XOElement.X)
                        {
                            winner = XOElement.X;
                            colors[j, j] = red;
                        }
                        else if (xofield[j, j] == XOElement.O)
                        {
                            winner = XOElement.O;
                            colors[j, j] = blue;
                        }
                    }

                    gameOver = true;
                }
            }
        }

        private void MethodForward()
        {
            if (gameOver == false)
            {
                byte countX = 0;
                byte countO = 0;
                byte e = 0; ;

                for (int i = xofield.GetLength(1) - 1; i >= 0; i--)
                {
                    if (xofield[i, e] == XOElement.X)
                    {
                        countX++;
                    }
                    else if (xofield[i, e] == XOElement.O)
                    {
                        countO++;
                    }
                    e++;
                }

                if (countO == 3 || countX == 3)
                {
                    e = 0;
                    for (int i = xofield.GetLength(1) - 1; i >= 0; i--)
                    {
                        if (xofield[i, e] == XOElement.X)
                        {
                            winner = XOElement.X;
                            colors[i, e] = red;
                        }
                        else if (xofield[i, e] == XOElement.O)
                        {
                            winner = XOElement.O;
                            colors[i, e] = blue;
                        }
                        e++;
                    }

                    gameOver = true;
                }
            }
        }

        public string FindingWin()
        {
            if (winner == XOElement.X)
            {
                return "Winner: X";
            }
            else if (winner == XOElement.O)
            {
                return "Winner: O";
            }
            else if (winner == XOElement.None)
            {
                return "Draw";
            }
            else
            {
                return "";
            }
        }
    }
}
