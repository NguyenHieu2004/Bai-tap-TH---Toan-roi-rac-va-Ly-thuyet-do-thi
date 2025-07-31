using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static List<int>[] adj = new List<int>[100005]; // danh sách kề
    static bool[] visited = new bool[100005];       // đánh dấu đã thăm
    static Stack<int> topoStack = new();            // lưu thứ tự topo

    static void DFS(int u)
    {
        visited[u] = true;
        foreach (int v in adj[u])
        {
            if (!visited[v])
                DFS(v);
        }
        topoStack.Push(u);
    }

    static void Main()
    {
        string inputPath = "TopoSort.INP.txt";
        string outputPath = "TopoSort.OUT.txt";

        int n;
        using (StreamReader reader = new(inputPath))
        {
            n = int.Parse(reader.ReadLine()!);
            for (int i = 1; i <= n; i++)
                adj[i] = new List<int>();

            for (int u = 1; u <= n; u++)
            {
                string? line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] tokens = line.Split();
                    foreach (string token in tokens)
                    {
                        int v = int.Parse(token);
                        adj[u].Add(v);
                    }
                }
            }
        }

        for (int i = 1; i <= n; i++)
        {
            if (!visited[i])
                DFS(i);
        }

        using (StreamWriter writer = new(outputPath))
        {
            while (topoStack.Count > 0)
                writer.Write(topoStack.Pop() + " ");
        }

        Console.WriteLine("✅ Đã sắp xếp topo bằng DFS và ghi ra file.");
    }
}
