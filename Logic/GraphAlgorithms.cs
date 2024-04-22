using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic;

public static class GraphAlgorithms
{
    public static bool[] DFS(List<List<int>> g, int s, bool[]? used = null)
    {
        if(used == null) 
            used = new bool[g.Count];

        DFSrecursive(g, s, used);

        return used;
    }

    private static void DFSrecursive(List<List<int>> g, int s, bool[] used)
    {
        Console.WriteLine(s + 1);
        
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
}
