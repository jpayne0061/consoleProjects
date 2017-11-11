using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class circleGraph
    {
        public static int[] GetY(int x, int radius, int h, int k)
        {

            double yStem = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow((x - h), 2));

            double yPos = yStem + k;
            double yNeg = yStem * -1 + k;

            return new int[] { (int)Math.Round(yPos), (int)Math.Round(yNeg) }; 

        }


        public static void Display(List<List<int>> grid)
        {
            foreach (List<int> l in grid)
            {
                string line = "";
                foreach (int a in l)
                {
                    if (a == 1)
                    {
                        line += "*";
                    }
                    else
                    {
                        line += "  ";
                    }

                }
                Console.WriteLine(line);
            }

        }

        public static List<List<int>> MakeGrid(int n)
        {

            List<List<int>> grid = new List<List<int>>();

            for (var i = 0; i < n; i++)
            {
                List<int> l = new List<int>();
                for (var j = 0; j < n; j++)
                {
                    l.Add(0);
                }
                grid.Add(l);
            }

            return grid;
        }

        public static List<List<int>> PlugGrid(List<List<int>> grid, int radius, int h, int k)
        {

            for (var x = (h - radius); x < (h +radius); x++)
            {
                int[] ys = GetY(x, radius, h, k);

                grid[ys[0]][x] = 1;

                grid[ys[1]][x] = 1;

            }

            return grid;
        }



    }
}
