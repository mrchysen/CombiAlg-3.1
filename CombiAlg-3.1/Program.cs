using Logic;

var g = GraphReader.ReadGraph("graph2.txt");

#region Компонент связности 
bool[] used = new bool[g.Count];
int i = 0;
while (used.Any(el => !el)) // О(N*(N+M))
{
    var v = -1;

    for (int j = 0; j < g.Count; j++)
    {
        if (!used[j])
        {
            v = j;
            break;
        }
    }

    i++;

    GraphAlgorithms.DFS(g, v, used);
}
Console.WriteLine("Компонент связности: " + i);
#endregion

#region Достижимость O(N + M)
var u = 0;
used = GraphAlgorithms.BFS(g, u);
Console.WriteLine($"Из вершины {u+1} достижимы: ");
for (int k = 0; k < g.Count; k++)
{
    if (used[k] && k != u)
        Console.WriteLine(k+1);
}
#endregion

#region Отыскание остовного дерева O(N + M)
u = 0;
(used, var Y) = GraphAlgorithms.BFSModified(g, u);

for (int k = 0; k < Y.Length; k++)
{
    if(k != u && Y[k] != -1)
    {
        Console.WriteLine($"Из {Y[k]} в {k}");
    }
}
#endregion


