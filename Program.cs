using System;
using Mancala;

namespace Mancalagame
{

    class Program
    {
        static int MAX_DEPTH = 4;
        static bool ALPHA_BETA = true;
        public static readonly double MIN_VALUE = -1.0;
        public static readonly double MAX_VALUR = 1.0;

        public static readonly int computer = 0;
        public static readonly int user = 1;

        static void Main(string[] args)
        {
            
            Console.WriteLine("Let's begin the game.");
            ProcessConfiguration();
            Board gameBoard = new Board();
            bool gameOver = false;
            while (!gameOver)
            {
                double highVal = -1.0;
                int bestMove = 0;
                double alfa = -1.0;
                double beta = 1.0;

                for (int col = 1; col <= Board.NR_COLS; col++)
                {
                    if (gameBoard.canMove(computer, col))
                    {
                        Board nextPos = gameBoard.makeMove(gameBoard, computer, col);
                        double thisVal = AlphaBeta.Value(nextPos, MAX_DEPTH - 1, alfa, beta, computer);
                        //Trace.println($" col = {col}   value = {Math.Round(thisVal, 2)}", 11, MAX_DEPTH);
                        if (thisVal > highVal)
                        {
                            bestMove = col;
                            highVal = thisVal;
                        }
                    }
                }
                
                Console.WriteLine($"My move is {(bestMove)})");
                gameBoard = gameBoard.makeMove(gameBoard, computer, bestMove);
                gameBoard.showBoard();
                if (gameBoard.isEmpty(computer))
                {
                    endGame(gameBoard, user);
                    gameOver = true;
                     
                }
                else
                {
                    Console.WriteLine("Your move");
                    int theirMove = UserInput.getInteger("Select column 1 - 6", 1, 6);
                    while (!gameBoard.canMove(user, theirMove)){
                        theirMove = UserInput.getInteger("Select column 1 - 6", 1, 6);
                    }
                    // int theirMove = UserInput.getInteger("Select column 1 - 6", 1, 6);

                    gameBoard = gameBoard.makeMove(gameBoard, user, theirMove);
                    Console.WriteLine("");
                    gameBoard.showBoard();
                    if (gameBoard.isEmpty(user))
                    {
                        endGame(gameBoard, computer);
                        gameOver = true;
                    }
                }
            }
            Console.ReadKey();
        }

        private static void endGame(Board gameBoard, int player)
        {
            gameBoard = gameBoard.finish(gameBoard, player);
            Console.WriteLine("Final board: ");
            gameBoard.showBoard();
        }

            private static void ProcessConfiguration()
        {
            String strDepth = System.Configuration.ConfigurationManager.AppSettings["Depth"];
            var depth = 0;
            if (int.TryParse(strDepth, out depth))
            {
                if (depth > 1 && depth < 10) MAX_DEPTH = depth;
            }
            String strTrace = System.Configuration.ConfigurationManager.AppSettings["Trace"];
            Int16 trcVal = 0;
            if (Int16.TryParse(strTrace, out trcVal))
            {
                Trace.ON = true; Trace.TraceDetailLevel = trcVal;
            }
            else Trace.ON = false;
            string strAB = System.Configuration.ConfigurationManager.AppSettings["AlphaBeta"];
            ALPHA_BETA = strAB.CompareTo("AB") == 0;
        }

    }
}
