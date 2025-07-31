using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string inputPath = "DFS.INP.txt";
        string outputPath = "DFS.OUT.txt";

        int n, m, start = 7;
        List<int>[] adj;
        bool[] visited;
        List<int> result = new();

        // Đọc dữ liệu
        using (StreamReader reader = new(inputPath))
        {
            string[] header = reader.ReadLine()!.Split();
            n = int.Parse(header[0]);
            m = int.Parse(header[1]);

            adj = new List<int>[n + 1];
            visited = new bool[n + 1];
            for (int i = 1; i <= n; i++) adj[i] = new List<int>();

            for (int i = 1; i <= n; i++)
            {
                string? line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var tokens = line.Trim().Split().Select(int.Parse);
                    foreach (var v in tokens)
                        adj[i].Add(v);
                }
            }
        }

        // DFS từ đỉnh start
        void DFS(int u)
        {
            visited[u] = true;
            result.Add(u);
            foreach (int v in adj[u])
                if (!visited[v]) DFS(v);
        }

        DFS(start);

        // Loại bỏ đỉnh gốc start
        result.Remove(start);

        // Ghi ra file
        using (StreamWriter writer = new(outputPath))
        {
            writer.WriteLine(result.Count);
            writer.WriteLine(string.Join(" ", result));
        }

        Console.WriteLine($"Đã ghi ra các đỉnh liên thông với đỉnh {start}, không bao gồm chính nó.");
    }
}
