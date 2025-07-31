using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string inputPath = "BFS.INP.txt";
        string outputPath = "BFS.OUT.txt";

        int n, s;
        List<int>[] adj;

        using (var reader = new StreamReader(inputPath))
        {
            var firstLine = reader.ReadLine()?.Split();
            n = int.Parse(firstLine[0]);
            s = int.Parse(firstLine[1]);

            adj = new List<int>[n + 1];
            for (int i = 1; i <= n; i++)
                adj[i] = new List<int>();

            for (int i = 1; i <= n; i++)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Trim().Split().Select(int.Parse);
                foreach (var v in parts)
                    adj[i].Add(v);
                
                adj[i].Sort();
            }
        }

        var visited = new bool[n + 1];
        var result = new List<int>();
        var queue = new Queue<int>();

        visited[s] = true;
        queue.Enqueue(s);

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();
            result.Add(u);

            foreach (var v in adj[u])
            {
                if (!visited[v])
                {
                    visited[v] = true;
                    queue.Enqueue(v);
                }
            }
        }

        using (var writer = new StreamWriter(outputPath))
        {
            var reachable = result.Where(x => x != s).ToList();
            writer.WriteLine(reachable.Count);
            writer.WriteLine(string.Join(" ", reachable));
        }

        Console.WriteLine("Đã duyệt BFS xong từ đỉnh " + s);
    }
}
