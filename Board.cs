using System;
using System.Collections;

namespace Mancala
{
    class Board
    {
        public static readonly int NR_ROWS = 2;
        public static readonly int NR_COLS = 6;
        private int myPot;
        private int urPot;

        public enum Cell{HAS, EMPTY};

        private int[,] board = new int[NR_ROWS, NR_COLS];

        //no args constructor
        public Board()
        {
            for (int row = NR_ROWS - 1; row >= 0; --row)
            {
                for (int col = 0; col < NR_COLS; ++col)
                {
                    board[row, col] = 4;
                }
            }
            myPot = 0;
            urPot = 0;
        }


        public Board(Board other)
        {
            for (int row = NR_ROWS - 1; row >= 0; --row)
            {
                for (int col = 0; col < NR_COLS; ++col)
                {
                    this.board[row, col] = other.board[row, col];
                }
            }
                this.myPot = other.myPot;
                this.urPot = other.urPot;
        }


        // display the current status of the board
        public void showBoard()
        {
            Console.WriteLine(" ---------------------------------");
            Console.Write("| " + " ");
            Console.Write("  ");

            for (int col = NR_COLS - 1; col >= 0; col--)
            {
                Console.Write($"| {board[0, col]} ");
            }
            Console.WriteLine("|    |");
            Console.WriteLine("| " + urPot + "  |-----------------------|  " + myPot + " |");
            Console.Write("|    |");
            for (int col = 0; col < NR_COLS; col++)
            {
                Console.Write($" {board[1, col]} |");
            }
            Console.WriteLine("    |");

            Console.WriteLine(" ---------------------------------");
            Console.WriteLine("       1   2   3   4   5   6");
        }

        //moves the pile from designated pit and distibutes the pieces into the following pits
        public Board makeMove( Board board, int row, int column)
        {
            Board newBoard = new Board(board);
            if (row == 0)
            {
                column = 7 - column;
            }

            int index = column - 1;
            int r = row;

            while (newBoard.board[row, column - 1] > 0)
            {
                newBoard.board[row, column - 1]--; //decrease the original pile

                if (index == 5)
                {
                    index = -1;
                    if (row == 0)
                    {
                        newBoard.urPot++;
                        r = 1;
                    }
                    else
                    {
                        newBoard.myPot++;
                        r = 0;     
                    }
                }
                else
                {
                    newBoard.board[r, index + 1]++;
                    index++;
                }
            }           
            return newBoard;
        }


        //send list of possible boards
        public ArrayList getPossibleMoves(Board board)
        {
            ArrayList moves = new ArrayList();
            for (int col = 0; col > NR_COLS; col++)
            {
                if (board.board[0, col] > 0)
                {
                    Board tempBoard = new Board(board);
                    tempBoard.makeMove(tempBoard, 0, col);
                    moves.Add(tempBoard);
                }
            }
            return moves;
        }


        public bool isWin(int row, int column)
        {
            if (board[row, column] == 7 - column)
            {
                return true;
            }
            else return false;
        }

        public double findMove()
        {
            for (int col = 0; col < NR_COLS; col++)
            {
                if (isWin(0, col)) return col;

                else if (isWin(1, col)) return col;
            }
            return 0;
        }

        public double heuristicValue()
        {
            return myPot - urPot / 48;

        }
    }
}
    