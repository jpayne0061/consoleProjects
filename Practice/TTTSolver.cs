using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class TTTSolver
    {
        public static string[][] WinningCombos = new string[][] { new string[] { "00", "01", "02"}, new string[] { "10", "11", "12" },
                                                        new string[] { "20", "21", "22" }, new string[] { "00", "10", "20" },
                                                        new string[] { "01", "11", "21" }, new string[] { "02", "12", "22" },
                                                        new string[] { "00", "11", "22" }, new string[] { "02", "11", "20" } };


        public static int CountNumContained(string[] combo, List<string> playerIndexes)
        {
            return combo.Where(index => playerIndexes.Contains(index)).Count();
        }

        public static int[] ContainsValue(string[] combo, int[][] board, int value)
        {
            if (board[Int32.Parse(combo[0][0].ToString())][Int32.Parse(combo[0][1].ToString())] == value)
                return new int[] { Int32.Parse(combo[0][0].ToString()), Int32.Parse(combo[0][1].ToString()) };
            if (board[Int32.Parse(combo[1][0].ToString())][Int32.Parse(combo[1][1].ToString())] == value)
                return new int[] { Int32.Parse(combo[1][0].ToString()), Int32.Parse(combo[1][1].ToString()) };
            if (board[Int32.Parse(combo[2][0].ToString())][Int32.Parse(combo[2][1].ToString())] == value)
                return new int[] { Int32.Parse(combo[2][0].ToString()), Int32.Parse(combo[2][1].ToString()) };

            return new int[] { -1, -1 };
        }

        public static int[] CheckWinningPlay(List<string> playerIndexes, string[] combo, int[][] board)
        {
            int[] indexes = ContainsValue(combo, board, 0);

            if (CountNumContained(combo, playerIndexes) == 2 && (!indexes.Contains(-1)))
                return indexes;
            else
                return new int[] { -1, -1 };
        }

        public static int[] CheckDefensePlay(List<string> playerIndexes, string[] combo, int[][] board, int opponent)
        {
            int[] indexes = ContainsValue(combo, board, 0);

            if (CountNumContained(combo, playerIndexes) == 2 && (!indexes.Contains(-1)))
                return indexes;
            else
                return new int[] { -1, -1 };
        }

        public static int CountZeros(int[][] board, string[] combo)
        {
            int count = 0;
            foreach (string indexes in combo)
            {
                int value = board[Int32.Parse(indexes[0].ToString())][Int32.Parse(indexes[1].ToString())];
                if (value == 0)
                    count += 1;
            }
            return count;

        }


        public static List<string> GetComboWithOpp(List<string> pair, int[][] board, int opponent)
        {
            List<string> combosWithOpponentAndTwoZero = new List<string>();
            List<string> combosWithOpponentAndOneZero = new List<string>();

            foreach (string[] combo in WinningCombos)
            {
                if ((combo.ToList().Contains(pair[0]) || combo.ToList().Contains(pair[1])) && !ContainsValue(combo, board, opponent).Contains(-1) && CountZeros(board, combo) == 2)
                {
                    if (combo.ToList().Contains(pair[0]))
                        combosWithOpponentAndTwoZero.Add(pair[0]);
                    if (combo.ToList().Contains(pair[1]))
                        combosWithOpponentAndTwoZero.Add(pair[1]);
                }

                if ((combo.ToList().Contains(pair[0]) || combo.ToList().Contains(pair[1])) && !ContainsValue(combo, board, opponent).Contains(-1) && CountZeros(board, combo) == 1)
                {
                    if (combo.ToList().Contains(pair[0]))
                        combosWithOpponentAndOneZero.Add(pair[0]);
                    if (combo.ToList().Contains(pair[1]))
                        combosWithOpponentAndOneZero.Add(pair[1]);
                }


            }

            Dictionary<string, int> tracker = new Dictionary<string, int>();

            foreach (string index in combosWithOpponentAndTwoZero)
            {
                if (tracker.ContainsKey(index))
                    tracker[index] += 1;
                else
                    tracker.Add(index, 1);
            }

            List<string> returnList = new List<string>();

            foreach (KeyValuePair<string, int> kvp in tracker)
            {
                if (kvp.Value > 1)
                    returnList.Add(kvp.Key);
            }

            return returnList;

        }

        public static int[] CheckForSurround(int[][] board, int opponent)
        {
            int playerNumber = opponent == 1 ? 2 : 1;

            if (((board[0][0] == opponent && board[2][2] == opponent) || (board[0][2] == opponent && board[2][0] == opponent)) && board[1][1] == playerNumber)
                return PickAnEdge(board, opponent);
            else
                return new int[] { -1, -1 };

        }

        public static int[] PickAnEdge(int[][] board, int opponent)
        {
            int[][] edges = new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 1, 2 }, new int[] { 2, 1 } };

            foreach (int[] edge in edges)
            {
                if (board[edge[0]][edge[1]] == 0)
                    return edge;
            }
            return new int[] { -1, -1 };
        }


        public static int[] CheckTwoOpen(List<string> playerIndexes, string[] combo, int[][] board, int opponent, int player)
        {

            int[] checkSurround = CheckForSurround(board, opponent);

            if (!checkSurround.Contains(-1))
                return checkSurround;


            if (!(ContainsValue(combo, board, player).Contains(-1)) && ContainsValue(combo, board, opponent).Contains(-1))
            {
                int[] playerIndex = ContainsValue(combo, board, player);

                List<string> checkForOppSpots = new List<string>();

                foreach (string s in combo)
                {
                    if (s != playerIndex[0].ToString() + playerIndex[1].ToString())
                        checkForOppSpots.Add(s);
                }

                List<string> opponentTwoSpots = GetComboWithOpp(checkForOppSpots, board, opponent);

                if (opponentTwoSpots.Count() != 0)
                    return new int[] { Int32.Parse(opponentTwoSpots[0][0].ToString()), Int32.Parse(opponentTwoSpots[0][1].ToString()), 3 };

                foreach (string strIndex in combo)
                {
                    if (strIndex != playerIndex[0].ToString() + playerIndex[1].ToString())
                        return new int[] { Int32.Parse(strIndex[0].ToString()), Int32.Parse(strIndex[1].ToString()) };
                }
                return new int[] { -1, -1 };

            }
            else
                return new int[] { -1, -1 };

        }

        public static int[] CheckOpenCombo(string[] combo, int[][] board)
        {
            if (board[1][1] == 0)
                return new int[] { 1, 1 };

            if (board[Int32.Parse(combo[0][0].ToString())][Int32.Parse(combo[0][1].ToString())] == 0 && board[Int32.Parse(combo[1][0].ToString())][Int32.Parse(combo[1][1].ToString())] == 0 && board[Int32.Parse(combo[2][0].ToString())][Int32.Parse(combo[2][1].ToString())] == 0)
                return new int[] { Int32.Parse(combo[0][0].ToString()), Int32.Parse(combo[0][1].ToString()) };

            return new int[] { -1, -1 };
        }

        public static int[] CheckForAny(string[] combo, int[][] board)
        {
            if (board[Int32.Parse(combo[0][0].ToString())][Int32.Parse(combo[0][1].ToString())] == 0)
                return new int[] { Int32.Parse(combo[0][0].ToString()), Int32.Parse(combo[0][1].ToString()) };
            if (board[Int32.Parse(combo[1][0].ToString())][Int32.Parse(combo[1][1].ToString())] == 0)
                return new int[] { Int32.Parse(combo[1][0].ToString()), Int32.Parse(combo[1][1].ToString()) };
            if (board[Int32.Parse(combo[2][0].ToString())][Int32.Parse(combo[2][1].ToString())] == 0)
                return new int[] { Int32.Parse(combo[2][0].ToString()), Int32.Parse(combo[2][1].ToString()) };

            return new int[] { -1, -1 };
        }

        public static List<string> ReturnPlayerIndexes(int[][] board, int player)
        {
            List<string> playerIndexes = new List<string>();

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (board[i][j] == player)
                        playerIndexes.Add(i.ToString() + j.ToString());
                }
            }

            return playerIndexes;
        }



        public static int[] TurnMethod(int[][] board, int player)
        {
            int opponent = player == 1 ? 2 : 1;
            List<string> playerIndexes = ReturnPlayerIndexes(board, player);
            List<string> opponentIndexes = ReturnPlayerIndexes(board, opponent);

            foreach (string[] combo in WinningCombos)
            {
                int[] checkWin = CheckWinningPlay(playerIndexes, combo, board);
                if (!checkWin.Contains(-1))
                    return checkWin;

            }
            foreach (string[] combo in WinningCombos)
            {
                int[] checkDefense = CheckDefensePlay(opponentIndexes, combo, board, opponent);
                if (!checkDefense.Contains(-1))
                    return checkDefense;
            }

            List<int[]> possiblePlays = new List<int[]>();
            foreach (string[] combo in WinningCombos)
            {
                int[] checkTwoOpen = CheckTwoOpen(playerIndexes, combo, board, opponent, player);
                if (!checkTwoOpen.Contains(-1))
                    possiblePlays.Add(checkTwoOpen);

            }
            if (possiblePlays.Count() > 0)
            {
                foreach (int[] play in possiblePlays)
                {
                    if (play.Count() > 2)
                        return new int[] { play[0], play[1] };
                }
                return possiblePlays[0];
            }

            foreach (string[] combo in WinningCombos)
            {

                int[] checkOpen = CheckOpenCombo(combo, board);
                if (!checkOpen.Contains(-1))
                    return checkOpen;
            }

            foreach (string[] combo in WinningCombos)
            {
                int[] checkForAny = CheckForAny(combo, board);
                if (!checkForAny.Contains(-1))
                    return checkForAny;
            }

            return new int[] { -1, -1 };
        }

    }
}
