using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{


    public class Percolation
    {
        public List<List<int[]>> Grid;

        public Dictionary<int[], int> sz = new Dictionary<int[], int>();

        public long PercTime = 0;


        //public List<List<int[]>> UnionGrid;
        public List<int[]> First = new List<int[]>();
        public List<int[]> Last = new List<int[]>();

        public List<int[]> Sites = new List<int[]>();
        int OpenSites = 0;
        int N;

        public void Display()
        {
            foreach (List<int[]> l in Grid)
            {
                string line = "";
                foreach (int[] a in l)
                {
                    if (a[2] == 1)
                    {
                        line += "#";
                    }
                    else {
                        line += " ";
                    }
                    
                }
                Console.WriteLine(line);
            }

        }

        public Percolation(int n)
        {
            N = n;
            Grid = new List<List<int[]>>();

            for (var i = 0; i < n; i++)
            {
                Grid.Add(new List<int[]>());
                for (var j = 0; j < n; j++)
                {
                    int[] a = new int[3] { i, j, 0};
                    Sites.Add(a);
                    Grid[i].Add(a);
                }
            }
        }

        public bool Percolates()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            foreach (int[] first in First)
            {
                foreach (int[] last in Last)
                {
                    if (Connected(first, last))
                    {
                        watch.Stop();
                        var elapsedM = watch.ElapsedMilliseconds;
                        PercTime += elapsedM;

                        return true;
                    }
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            PercTime += elapsedMs;
            return false;
        }



        public void Open(int row, int column)
        {
            if (row == 0)
                First.Add(Grid[row][column]);
            if (row == N-1)
                Last.Add(Grid[row][column]);

            Grid[row][column][2] = 1;
            OpenSites += 1;
            List<List<int>> neighbs = GetNeighbors(Grid[row][column].ToList());
            AddUnionToOpenNeighbors(neighbs, Grid[row][column].ToList());
            //UnionSelf(row, column);

        }

        //addressed in ctor
        //public void UnionSelf(int row, int column)
        //{
        //    Grid[row][column] = new int[] { row, column };
        //}

        public bool IsOpen(int row, int column)
        {
            return Grid[row][column][2] == 1;
        }

        public bool IsFull(int row, int column)
        {
            return Grid[row][column][2] == 2;
        }

        public int NumOpenSites()
        {
            return OpenSites;
        }

        private int[] Root(int[] site)
        {
            if ( site == Grid[site[0]][site[1]])
            {
                return site;
            }
            else
            {
                Grid[site[0]][site[1]] = Grid[ Grid[site[0]][site[1]][0] ] [ Grid[site[0]][site[1]][1] ];

                return Root(Grid[site[0]][site[1]]);
            }
        }

        public void Union(int[] p, int[] q)
        {
            int[] j = Root(p);
            int[] i = Root(q);

            if (sz.ContainsKey(j))
                sz[j] += 1;
            else
                sz.Add(j, 1);

            if (sz.ContainsKey(i))
                sz[i] += 1;
            else
                sz.Add(i, 1);

            if(sz[i] < sz[j])
                Grid[i[0]][i[1]] = j;
            else
                Grid[j[0]][j[1]] = i;



        }
        //public bool Percolates()
        //{

        //}

        //public bool Connectable(List<int> a, List<int> b)
        //{
        //    return ((a[0] == b[0] || a[1] == b[1]) && (Math.Abs(a[0] - b[0]) == 1 || Math.Abs(a[1] - b[1]) == 1));
        //}

        public bool IsOut(List<int> site)
        {
            return (site.Contains(-1) || site.Contains(N));
        }

        public bool Connected(int[] a, int[] b)
        {
            return Root(Grid[a[0]][a[1]]) == Root(Grid[b[0]][b[1]]);  
        }

        public List<List<int>> GetNeighbors(List<int> site)
        {
            List<List<int>> neighbors = new List<List<int>>();
            neighbors.Add(new List<int> { site[0] - 1, site[1] });
            neighbors.Add(new List<int> { site[0] + 1, site[1] });
            neighbors.Add(new List<int> { site[0], site[1] + 1 });
            neighbors.Add(new List<int> { site[0], site[1] - 1 });

            return neighbors.Where(s => !IsOut(s)).ToList();
        }

        public void AddUnionToOpenNeighbors(List<List<int>> sites, List<int> homeSite)
        {
            foreach (List<int> site in sites)
            {
                if (Grid[site[0]][site[1]][2] == 1)
                {
                    Union(Grid[site[0]][site[1]], Grid[homeSite[0]][homeSite[1]] );
                }
            }
        }


    }

}