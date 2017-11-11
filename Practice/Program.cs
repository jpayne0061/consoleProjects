using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mathHelper;
using Rextester;


namespace Practice
{
    public class UnionFind
    {
        public int[] components;


        public UnionFind(int n)
        {
            components = Enumerable.Range(0, n).ToArray();
        }

        public void Union(int p, int q)
        {
            foreach (int val in components)
            {
                if (val == p)
                {
                    int indexOfVal = Array.IndexOf(components, val);
                    components[indexOfVal] = components[q];
                }
            }

        }

        public bool Connected(int p, int q)
        {
            return components[p] == components[q];
        }


    }

    public class QuickFind
    {
        public int[] components;


        public QuickFind(int n)
        {
            components = Enumerable.Range(0, n).ToArray();
        }

        public void Union(int p, int q)
        {
            p = GetRoot(q);

        }

        public bool Connected(int p, int q)
        {
            return GetRoot(p) == GetRoot(q);
        }

        public int GetRoot(int value)
        {
            if (value == components[value])
                return value;
            else
                return GetRoot(components[value]);
        }


    }


    class Program
    {



        public static bool ValidateThreeByThree(List<int> threeByThree)
        {
            int[] allNine = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            return new HashSet<int>(threeByThree).SetEquals(allNine);

        }


        public static bool ValidateSolution(int[][] board)
        {
            List<int> sub1 = new List<int>();
            List<int> sub2 = new List<int>();
            List<int> sub3 = new List<int>();

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (j >= 0 && j <= 2)
                        sub1.Add(board[i][j]);
                    if (j >= 3 && j <= 5)
                        sub2.Add(board[i][j]);
                    if (j >= 6 && j <= 8)
                        sub3.Add(board[i][j]);
                }
                if ((i + 1) % 3 == 0)
                {
                    if (ValidateThreeByThree(sub1) == false || ValidateThreeByThree(sub2) == false || ValidateThreeByThree(sub3) == false)
                        return false;
                    sub1 = new List<int>();
                    sub2 = new List<int>();
                    sub3 = new List<int>();
                }

            }

            return true;
        }


        public static string MakeLine(int x, int mark)
        {
            string line = "";

            for (var i = x; i > 0; i--)
            {
                line += " ";
            }

            if (mark == 1)
            {
                return line + "x";
            }
            else
            {
                return line;
            }

        }

        public static int factorial(int a)
        {
            int sum = 1;

            if (a == 0)
            {
                return 0;
            }

            for (var i = a; i > 0; i--)
            {
                sum *= i;
            }

            return sum;
        }

        public static Dictionary<string, int> FindPatterns(string blob)
        {
            Dictionary<string, int> patterns = new Dictionary<string, int>();

            for (var i = 2; i <= blob.Length / 2; i++)
            {
                for (var j = 0; j <= (blob.Length - i); j++)
                {
                    string subString = blob.Substring(j, i);

                    if (patterns.ContainsKey(subString))
                    {
                        patterns[subString] += 1;
                    }
                    else
                    {
                        patterns.Add(subString, 1);
                    }

                }


            }
            Dictionary<string, int> patternsCleaned = new Dictionary<string, int>();

            foreach (var kvp in patterns)
            {
                if (kvp.Value > 1)
                    patternsCleaned.Add(kvp.Key, kvp.Value);
            }
            return patternsCleaned;

        }

        public static int solveExpression(string expression)
        {
            string[] splitExp = expression.Split('=');

            string left = splitExp[0];
            string right = splitExp[1];

            if (left.Contains("--"))
            {
                left = left.Replace("--", "+");
            }


            if (left.Contains('*'))
            {
                string[] leftExp = left.Split('*');

                string allNums = left + right;

                for (var i = 0; i <= 9; i++)
                {
                    if (((leftExp[0].Count(x => x == '?') > 1 || leftExp[1].Count(x => x == '?') > 1 || right.Count(x => x == '?') > 1) && i == 0) || allNums.Contains(i.ToString()))
                    {
                        continue;
                    }

                    if (Int32.Parse(leftExp[0].Replace("?", i.ToString())) * Int32.Parse(leftExp[1].Replace("?", i.ToString())) == Int32.Parse(right.Replace("?", i.ToString())))
                    {
                        return i;
                    }
                }
                return -1;

            }
            else if (left.Contains('+'))
            {

                string[] leftExp = left.Split('+');

                string allNums = left + right;

                for (var i = 0; i <= 9; i++)
                {
                    if (((leftExp[0].Count(x => x == '?') > 1 || leftExp[1].Count(x => x == '?') > 1 || right.Count(x => x == '?') > 1) && i == 0) || allNums.Contains(i.ToString()))
                    {
                        continue;
                    }

                    if (Int32.Parse(leftExp[0].Replace("?", i.ToString())) + Int32.Parse(leftExp[1].Replace("?", i.ToString())) == Int32.Parse(right.Replace("?", i.ToString())))
                    {
                        return i;
                    }
                }
                return -1;
            }
            else
            {
                string[] leftExp = left.Split('-');

                string allNums = left + right;

                for (var i = 0; i <= 9; i++)
                {
                    if (((leftExp[0].Count(x => x == '?') > 1 || leftExp[1].Count(x => x == '?') > 1 || right.Count(x => x == '?') > 1) && i == 0) || allNums.Contains(i.ToString()))
                    {
                        continue;
                    }

                    if (Int32.Parse(leftExp[0].Replace("?", i.ToString())) - Int32.Parse(leftExp[1].Replace("?", i.ToString())) == Int32.Parse(right.Replace("?", i.ToString())))
                    {
                        return i;
                    }
                }
                return -1;
            }

        }





        public static void Main()
        {
            int radius = 15;
            int h = 20;
            int k = 20;

            List<List<int>> m = circleGraph.MakeGrid(40);
            List<List<int>> p = circleGraph.PlugGrid(m, radius, h, k);

            circleGraph.Display(p);



            //var watch = System.Diagnostics.Stopwatch.StartNew();
            //// the code that you want to measure comes here
            //Console.WriteLine("Enter n, then t");
            //int n = Int32.Parse(Console.ReadLine());
            //int t = Int32.Parse(Console.ReadLine());


            //Random rnd = new Random();

            //List<double> counts = new List<double>();

            //for (var i = 0; i < t; i++)
            //{

            //    //List<double> std = new List<double>();

            //    int count = 0;
            //    //int n = 200;
            //    Percolation p = new Percolation(n);


            //    while (!p.Percolates())
            //    {
            //        int r = rnd.Next(0, p.Sites.Count());
            //        int[] a = p.Sites[r];

            //        p.Open(a[0], a[1]);
            //        p.Sites.RemoveAt(r);
            //        count += 1;

            //    }

            //    p.Display();
            //    counts.Add((double)count);

            //}

            //double std = PracHelpers.CalculateStdDev(counts.Select(c => c / (n * n)));
            //double mean = counts.Select(c => c / (n * n)).Average();




            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;

            //p.Display();

            //solveExpression("??*??=302?");
            //FindPatterns("abcdefghhhijklmnohhhpqreeestuveeewxyzeee9876eee");
            //TTTSolver t = new TTTSolver();

            //int[][] board = new int[][] { new int[] { 2, 1, 2 }, new int[] { 1, 2, 1 }, new int[] { 0, 0, 0 } };
            //var board2 = new int[][]
            //{
            //        new int[] { 0, 2, 1 },
            //        new int[] { 1, 2, 2 },
            //        new int[] { 2, 2, 0 }
            //};

            //var board3 = new int[][]
            //{
            //        new int[] { 0, 0, 0 },
            //        new int[] { 0, 0, 0 },
            //        new int[] { 0, 0, 0 }
            //};

            //var board4 = new int[][]
            //{
            //        new int[] { 0,0,2 },
            //        new int[] { 2,1,0 },
            //        new int[] { 0,0,0 }
            //};

            //int[] play = TTTSolver.TurnMethod(board4, 1);




        }



        

    }
}
