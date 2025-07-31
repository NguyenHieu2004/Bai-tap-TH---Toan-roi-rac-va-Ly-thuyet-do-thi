using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static List<(int to, int weight)>[] adj;
    static bool[] visited;
    static List<(int u, int v, int w)> treeEdges = new();

    static void DFS(int u)
    {
        visited[u] = true;
        foreach (var (v, w) in adj[u])
        {
            if (!visited[v])
            {
                treeEdges.Add((u, v, w));
                DFS(v);
            }
        }
    }

    static void Main()
    {
        string inputPath = "CayKhung.INP.txt";
        string outputPath = "CayKhung.OUT.txt";

        string[] lines = File.ReadAllLines(inputPath);
        var tokens = lines[0].Split();
        int n = int.Parse(tokens[0]);
        int m = int.Parse(tokens[1]);

        adj = new List<(int, int)>[n + 1];
        visited = new bool[n + 1];
        for (int i = 1; i <= n; i++)
            adj[i] = new List<(int, int)>();

        for (int i = 1; i <= m; i++)
        {
            var edge = lines[i].Split();
            int u = int.Parse(edge[0]);
            int v = int.Parse(edge[1]);
            int w = int.Parse(edge[2]);

            adj[u].Add((v, w));
            adj[v].Add((u, w)); // vì đồ thị vô hướng
        }

        DFS(1);

        using (var writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(treeEdges.Count);
            foreach (var (u, v, w) in treeEdges)
                writer.WriteLine($"{u} {v} {w}");
        }

        Console.WriteLine("✅ Đã tạo cây khung và ghi vào CayKhung.OUT.txt");
    }
}
