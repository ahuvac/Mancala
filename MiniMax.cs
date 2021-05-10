using System;

namespace Mancala
{
    class MiniMax
    {
        public static int NrEntries = 0;
        public static double Value(Board board, int depth, Player player)
        {
            Trace.println($"Enter minimax d = {depth} P = {player}", 5);
            Trace.println($"Enter minimax d = {depth} P = {player}", 11, depth);
            Player opponent = player == Player.MAX ? Player.MIN : Player.MAX;
            ++NrEntries;
            double value = 0.0;
            if (depth == 0)
            {
                value = board.heuristicValue();
            }
            else if (board.isFull())
            {
                value = board.heuristicValue();
            }

            else if (board.isWin(opponent)) { value = player == Player.MAX ? Program.MIN_VALUE : Program.MAX_VALUR; }
            else
            {
                if (player == Player.MAX)
                {
                    double BestValue = Program.MIN_VALUE; for (int col = 0; col < Board.NR_COLS; ++col)
                    { if (board.canMove(col)) { Board nextPos = board.MakeMove(Player.MAX, col); double thisVal = Value(nextPos, depth - 1, opponent); Trace.println($"value = {thisVal} for col = {col}", 11, depth); if (thisVal > BestValue) { BestValue = thisVal; } } }
                    value = BestValue;
                }
                else  // player == Player.MIN
                {
                    double BestValue = 1.0;
                    for (int col = 0; col < Board.NR_COLS; ++col)
                    {
                        if (board.canMove(col))
                        {
                            Board nextPos = board.MakeMove(Player.MIN, col);
                            double thisVal = Value(nextPos, depth - 1, opponent);
                            if (BestValue > thisVal)
                            {
                                BestValue = thisVal;
                            }
                        }
                    }
                    value = BestValue;
                }
            }
            Trace.println("Exit miniMax value = " + value + " depth " + depth, 5);
            Trace.println("Exit miniMax value = " + value + " depth " + depth, 11, depth);
            return value;
        }
    }
}


