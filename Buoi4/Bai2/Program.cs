using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string inputPath = "CanhCau.INP.txt";
        string outputPath = "CanhCau.OUT.txt";

        int n, u, v;
        List<int>[] adj;

        // đọc input
        using (StreamReader reader = new(inputPath))
        {
            string[] firstLine = reader.ReadLine()?.Trim().Split() ?? Array.Empty<string>();
            n = int.Parse(firstLine[0]);
            u = int.Parse(firstLine[1]);
            v = int.Parse(firstLine[2]);

            adj = new List<int>[n + 1];
            for (int i = 1; i <= n; i++)
            {
                adj[i] = new List<int>();
            }

            for (int i = 1; i <= n; i++)
            {
                string? line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    foreach (var token in line.Trim().Split())
                    {
                        int to = int.Parse(token);
                        adj[i].Add(to);
                    }
                }
            }
        }
        // Hàm đếm số miền liên thông
        int CountConnectedComponents(List<int>[] graph, int n)
        {
            bool[] visited = new bool[n + 1];
            int count = 0;

            for (int i = 1; i <= n; i++)
            {
                if (!visited[i])
                {
                    count++;
                    Queue<int> q = new();
                    q.Enqueue(i);
                    visited[i] = true;

                    while (q.Count > 0)
                    {
                        int curr = q.Dequeue();
                        foreach (int neighbor in graph[curr])
                        {
                            if (!visited[neighbor])
                            {
                                visited[neighbor] = true;
                                q.Enqueue(neighbor);
                            }
                        }
                    }
                }
            }

            return count;
        }

        int original = CountConnectedComponents(adj, n);

        // Xoá cạnh (u,v)
        adj[u].Remove(v);
        adj[v].Remove(u);

        int afterRemove = CountConnectedComponents(adj, n);

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(afterRemove > original ? "YES" : "NO");
        }

        Console.WriteLine("Đã kiểm tra cạnh cầu và ghi kết quả.");

    }
}