using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static int n;
    static List<int>[] adj;
    static bool[] visited;
    static bool hasCycle = false;

    static void DFS(int u, int parent)
    {
        visited[u] = true;
        foreach (int v in adj[u])
        {
            if (!visited[v])
            {
                DFS(v, u);
            }
            else if (v != parent)
            {
                hasCycle = true;
            }
        }
    }

    static void Main()
    {
        string inputPath = "ChuTrinh.INP.txt";
        string outputPath = "ChuTrinh.OUT.txt";

        using (StreamReader reader = new(inputPath))
        {
            n = int.Parse(reader.ReadLine()!);
            adj = new List<int>[n + 1];
            visited = new bool[n + 1];

            for (int i = 1; i <= n; i++)
            {
                adj[i] = new List<int>();
                string? line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var tokens = line.Trim().Split();
                    foreach (var token in tokens)
                    {
                        int v = int.Parse(token);
                        adj[i].Add(v);
                    }
                }
            }
        }

        for (int i = 1; i <= n && !hasCycle; i++)
        {
            if (!visited[i])
            {
                DFS(i, -1);
            }
        }

        using (StreamWriter writer = new(outputPath))
        {
            writer.WriteLine(hasCycle ? "YES" : "NO");
        }

        Console.WriteLine("Đã kiểm tra chu trình và ghi ra file ChuTrinh.OUT.txt");
    }
}
