using System;

namespace Mancala
{

    class Program
    {
        static int MAX_DEPTH = 4;
        static bool ALPHA_BETA = false;
        public static readonly double MIN_VALUE = -1.0;
        public static readonly double MAX_VALUR = 1.0;



        static void Main(string[] args)
        {
            Board board = new Board();

            Console.WriteLine("Human moving pile 6");

            board = board.makeMove(board, 1, 6);
            board.showBoard();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Computer moving pile 4");

            board = board.makeMove(board, 0, 4);
            board.showBoard();
            
            //Console.WriteLine("Lets start playing Mancala. Here is our board:");
            //board.showBoard();
            //playGame(board);



        }

        private static void playGame(Board board)
        { 
            //Console.WriteLine("Your turn.+\n+Which pile do you want to pick up?");
            //int choice = 0;
            //board = board.MakeMove(board, Player.0, choice);
            //board.showBoard();
            //Console.WriteLine("My turn.");
            //board = board.MakeMove(board, Player.0, 2);
            //board.showBoard();


        }
    }
}
