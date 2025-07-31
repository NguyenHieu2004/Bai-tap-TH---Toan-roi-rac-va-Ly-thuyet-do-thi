using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<int>[] adj;
    static bool[] visited;
    static List<int> result = new();

    static void DFS(int u)
    {
        visited[u] = true;
        foreach (int v in adj[u])
            if (!visited[v])
                DFS(v);
        result.Add(u); // Thêm sau cùng để khi đảo sẽ đúng thứ tự
    }

    static void Main()
    {
        string inputPath = "TopoSort.INP.txt";
        string outputPath = "TopoSort.OUT.txt";

        int n;
        using (StreamReader reader = new(inputPath))
        {
            n = int.Parse(reader.ReadLine()!);
            adj = new List<int>[n + 1];
            visited = new bool[n + 1];

            for (int i = 1; i <= n; i++)
                adj[i] = new List<int>();

            for (int u = 1; u <= n; u++)
            {
                string? line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var tokens = line.Split().Select(int.Parse);
                    foreach (var v in tokens)
                        adj[u].Add(v);
                }
            }
        }

        // Để đảm bảo ra đúng thứ tự, ta DFS theo thứ tự từ 1 đến n
        for (int i = 1; i <= n; i++)
            if (!visited[i])
                DFS(i);

        result.Reverse(); // Đảo ngược để ra thứ tự topo

        using (StreamWriter writer = new(outputPath))
        {
            writer.WriteLine(string.Join(" ", result));
        }

        Console.WriteLine("✅ Đã sắp xếp topo bằng DFS và ghi vào TopoSort.OUT.txt");
    }
}
