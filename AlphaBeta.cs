using System;
using System.Collections;
using System.Diagnostics;

namespace Mancalagame
{
    class AlphaBeta
    {
        public static double Value(Board board, int depth, double alfa, double beta, int player)
        {
            int computer = 0;
            int user = 1;
            Trace.println("Enter alphabeta d = " + depth + " a = " + alfa + " b = " + beta + " P = " + player, 5);

            double value = 0.0;

            if (depth == 0)
            {
                value = board.heuristicValue();
            }

            else
            {
               // Player opponent = player == Player.MAX ? Player.MIN : Player.MAX
                if (player == computer)
                {
                    ArrayList boards = board.getPossibleMoves(board, computer);
                    foreach (Board move in boards)
                    {
                        double thisVal = Value(move, depth - 1, alfa, beta, user);
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
                // player == user
                {
                    ArrayList boards = board.getPossibleMoves(board, user);

                    foreach (Board move in boards)
                    {
                        double thisVal = Value(move, depth - 1, alfa, beta, computer);
                        if (thisVal < beta)
                        {
                            beta = thisVal;
                        }
                        if (beta <= alfa) {
                            break;
                        }
                    }
                }
                value = beta;
            }

            Trace.println("Exit alfabeta value = " + value + " depth " + depth, 5);
            return value;
        }
    }
}
