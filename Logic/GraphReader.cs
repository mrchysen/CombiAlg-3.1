namespace Logic;

public static class GraphReader
{
    /// <summary>
    /// NW - неориентированный - если true то добавляется путь в обе стороны
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<List<int>> ReadGraph(string path, bool NW = false)
    {
        List<List<int>> ans = new();

        if(File.Exists(path))
        {
            try
            {
                using (StreamReader sr = new(path))
                {
                    var data = sr.ReadLine().Split().Select(int.Parse).ToArray();
                    int n = data[0];
                    int m = data[1];

                    for (int i = 0; i < n; i++)
                    {
                        ans.Add(new());
                    }

                    for (int i = 0; i < m; i++)
                    {
                        data = sr.ReadLine().Split().Select(int.Parse).ToArray();

                        ans[data[0] - 1].Add(data[1] - 1);
                        if(NW)
                            ans[data[1] - 1].Add(data[0] - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error_reading: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("info: No such file");
        }

        return ans;
    }
}
