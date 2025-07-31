using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string inputPath = "DemLienThong.INP.txt";
        string outputPath = "DemLienThong.OUT.txt";

        int n;
        List<int>[] adj;

        // Đọc danh sách kề
        using (StreamReader reader = new StreamReader(inputPath))
        {
            n = int.Parse(reader.ReadLine()?.Trim() ?? "0");
            adj = new List<int>[n + 1];

            for (int i = 1; i <= n; i++)
            {
                adj[i] = new List<int>();
                string? line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] tokens = line.Trim().Split();
                    foreach (var token in tokens)
                    {
                        if (int.TryParse(token, out int v))
                            adj[i].Add(v);
                    }
                }
            }
        }

        // Đếm miền liên thông
        bool[] visited = new bool[n + 1];
        int count = 0;

        void BFS(int start)
        {
            Queue<int> q = new();
            q.Enqueue(start);
            visited[start] = true;

            while (q.Count > 0)
            {
                int u = q.Dequeue();
                foreach (int v in adj[u])
                {
                    if (!visited[v])
                    {
                        visited[v] = true;
                        q.Enqueue(v);
                    }
                }
            }
        }

        for (int i = 1; i <= n; i++)
        {
            if (!visited[i])
            {
                BFS(i);
                count++;
            }
        }

        // Ghi kết quả
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(count);
        }

        Console.WriteLine($"Số miền liên thông: {count}");
    }
}
