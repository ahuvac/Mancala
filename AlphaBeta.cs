using System;
using System.Collections;
using System.Diagnostics;

namespace Mancala
{
    class AlphaBeta
    {
        public static double Value(Board board, int depth, double alfa, double beta, int player)
        {
            Trace.println("Enter alphabeta d = " + depth + " a = " + alfa + " b = " + beta + " P = " + player, 5);

            double value = 0.0;
            if (depth == 0)
            {
                value = board.heuristicValue();
            }

            else
            {
                int opponent = player == 0 ? 1 : 0;
                if (player == 0)
                {
                    ArrayList boards = new ArrayList();
                    foreach (Board move in boards)
                    {
                        double thisVal = Value(move, depth - 1, alfa, beta, opponent);
                        if (thisVal > alfa)
                        {
                            alfa = thisVal;
                        }
                        if (beta <= alfa)
                        {
                            break;
                        }

                    }
                    value = alfa;
                }
                else
                // player == Player.MIN
                {
                    ArrayList boards = new ArrayList();
                    foreach (Board move in boards)
                    {
                        double thisVal = Value(move, depth - 1, alfa, beta, opponent);
                        if (thisVal < beta)
                        {
                            beta = thisVal;
                        }
                        if (beta <= alfa) { break; }
                    }
                }
                value = beta;
            }

            Trace.println("Exit alfabeta value = " + value + " depth " + depth, 5);
            return value;
        }
    }
}
