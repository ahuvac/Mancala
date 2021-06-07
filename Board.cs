using System;
using System.Collections;

namespace Mancalagame
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

        public bool canMove( int row, int col)
        {
            if (row == 0)
            {
                col = 7 - col;
                //flip it becuase the array goes backwards
                return board[row, col - 1] > 0;

            }
            else
            {
                return board[row, col - 1] > 0;
            }
        }


        // display the current status of the board
        public void showBoard()
        {
            Console.WriteLine(" -------------------------------------------------");
            Console.Write("| " + " ");
            Console.Write("    ");
            for (int col = NR_COLS - 1; col >= 0; col--)
            {
                Console.Write($"|  {board[0, col]}  ");
            }
            Console.WriteLine("|      |");
            Console.WriteLine("|   " + urPot + "  |-----------------------------------|  " + myPot + "   |");
            Console.Write("|      |");
            for (int col = 0; col < NR_COLS; col++)
            {
                Console.Write($"  {board[1, col]}  |");
            }
            Console.WriteLine("      |");
            Console.WriteLine(" -------------------------------------------------");
            Console.WriteLine("         1      2     3     4     5     6");
        }

        //moves the pile from designated pit and distibutes the pieces into the following pits
        public Board makeMove(Board board, int row, int column)
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

        public Boolean gameOver(Board board)
        {
            if (board.getPossibleMoves(board, 0).Count > 0 || board.getPossibleMoves(board, 1).Count > 0) return false;
            else return true;
        }

        //get list of possible boards
        public ArrayList getPossibleMoves(Board gameBoard, int player)
        {
            ArrayList moves = new ArrayList();
            for (int col = 1; col <= NR_COLS; col++)
            {
                if (gameBoard.board[player, col - 1] > 0)
                {
                    Board tempBoard = new Board(gameBoard);
                    tempBoard = tempBoard.makeMove(tempBoard, player, col);
                    moves.Add(tempBoard);
                }
            }
            return moves;
        }

        // returns whether one row is empty
        public bool isEmpty(int row)
        {
            bool retVal = true;

            for (int col = 0; col < NR_COLS; col++)
            {
                if (board[row, col] > 0)
                {
                    retVal = false;
                    goto _end_search;
                }
            }
            _end_search:
            return retVal;
        }

     
      

        public double heuristicValue()
        {
            return myPot - urPot / 48;
        }

        public Board finish(Board gameBoard, int row)
        {
            int total = 0;
            Board board = new Board(gameBoard);
            for (int col = 0; col < NR_COLS; col++)
            {
                total += board.board[row, col];             
                board.board[row, col] = 0;
            }
            if (row == 0) board.myPot += total;
            else board.urPot += total;
            return board;
        }
    }
}
    