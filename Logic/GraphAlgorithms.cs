using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic;

public static class GraphAlgorithms
{
    public static void ReverseGraphWeight(List<List<(int, int)>> g, List<Edge> edges = null)
    {
        for (int i = 0; i < g.Count; i++)
        {
            for (int j = 0; j < g[i].Count; j++)
            {
                g[i][j] = (g[i][j].Item1, g[i][j].Item2 * (-1));
            }
        }

        if(edges != null)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                edges[i] = new Edge() { From = edges[i].From, To = edges[i].To, Wieght = edges[i] .Wieght * (-1)};
            }
        }
    }

    #region Всё для Краскала
    public static List<List<(int, int)>> Crascal(List<List<(int, int)>> g, List<Edge> edges)
    {
        int n = g.Count;
        
        edges.Sort((A,B) => A.Wieght.CompareTo(B.Wieght));

        // Инициализация
        List<List<(int, int)>> resultGraph = new(n);
        for (int i = 0; i < n; i++)
            resultGraph.Add(new());

        int k = 0;
        for (int i = 0; i < edges.Count; i++)
        {
            if (ContainsEdge(resultGraph, edges[i]))
                continue;

            AddEdge(resultGraph, edges[i]);
            k++;

            if (IsCycled(resultGraph))
            {
                RemoveEdge(resultGraph, edges[i]);
                k--;
            }

            if (k == n - 1)
                break;
        }

        return resultGraph;
    }

    public static bool ContainsEdge(List<List<(int, int)>> g, Edge e)
    {
        return g[e.From].Contains((e.To, e.Wieght));
    }
    public static void AddEdge(List<List<(int, int)>> g, Edge e)
    {
        g[e.From].Add((e.To,e.Wieght));
        g[e.To].Add((e.From,e.Wieght));
    }
    public static void RemoveEdge(List<List<(int, int)>> g, Edge e)
    {
        g[e.From].Remove((e.To, e.Wieght));
        g[e.To].Remove((e.From, e.Wieght));
    }
    #endregion

    #region Цикличность
    /// <summary>
    /// Есть ли в графе цикл
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    public static bool IsCycled(List<List<(int, int)>> g)
    {
        bool cycle = false;

        for (int i = 0; i < g.Count; i++)
        {
            int[] visited = new int[g.Count];

            cycle = cycle || DFS_cycle(g, i, i, visited);
        }

        return cycle;
    }
    public static bool DFS_cycle(List<List<(int, int)>> g, int v, int from, int[] visited)
    {
        visited[v] = 1;

        bool cycle = false;

        for (int i = 0; i < g[v].Count; i++)
        {
            if (g[v][i].Item1 == from)
            {
                continue;
            }
            if (visited[g[v][i].Item1] == 1)
            {
                return true;
            }
            else
            {
                cycle = cycle || DFS_cycle(g, g[v][i].Item1, v,  visited);
            }
        }

        visited[v] = 2;

        return cycle;
    }
    #endregion

    #region BFS / DFS for <int,int>
    public static bool[] BFS(List<List<(int, int)>> g, int s)
    {
        Queue<int> q = new Queue<int>();
        bool[] used = new bool[g.Count];

        q.Enqueue(s);

        while (q.Count > 0)
        {
            var v = q.Dequeue();

            if (!used[v])
            {
                Console.WriteLine(v + 1);

                used[v] = true;


                for (int i = 0; i < g[v].Count; i++)
                {
                    if (!used[g[v][i].Item1])
                        q.Enqueue(g[v][i].Item1);
                }
            }
        }

        return used;
    }
    #endregion

    #region BFS / DFS for int
    public static bool[] DFS(List<List<int>> g, int s, bool[]? used = null)
    {
        if(used == null) 
            used = new bool[g.Count];

        DFSrecursive(g, s, used);

        return used;
    }

    private static void DFSrecursive(List<List<int>> g, int s, bool[] used)
    {
        //Console.WriteLine(s + 1);
        
        used[s] = true;

        for (int i = 0; i < g[s].Count; i++)
        {
            if (!used[g[s][i]])
                DFS(g, g[s][i], used);
        }
    }

    public static bool[] BFS(List<List<int>> g, int s)
    {
        Queue<int> q = new Queue<int>();
        bool[] used = new bool[g.Count];

        q.Enqueue(s);

        while(q.Count > 0) 
        { 
            var v = q.Dequeue();

            used[v] = true;

            for (int i = 0; i < g[v].Count; i++)
            {
                if (!used[g[v][i]])
                    q.Enqueue(g[v][i]);
            }
        }

        return used;
    }

    public static (bool[], int[]) BFSModified(List<List<int>> g, int s)
    {
        Queue<int> q = new Queue<int>();
        bool[] used = new bool[g.Count];
        int[] Y = (new int[g.Count]).Select(s => -1).ToArray();
        Y[s] = 0;

        q.Enqueue(s);

        while (q.Count > 0)
        {
            var v = q.Dequeue();

            used[v] = true;

            for (int i = 0; i < g[v].Count; i++)
            {
                if (!used[g[v][i]])
                {
                    q.Enqueue(g[v][i]);
                    Y[g[v][i]] = v;
                }
            }
        }

        return (used,Y);
    }

    #endregion
}
