using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string inputPath = "Canh2Ke.INP.txt";
        string outputPath = "Canh2Ke.OUT.txt";

        int n;
        List<int>[] adj;
        List<(int, int)> edges = new();

        using (StreamReader reader = new StreamReader(inputPath))
        {
            n = int.Parse(reader.ReadLine() ?? "0");
            adj = new List<int>[n + 1];

            for (int i = 1; i <= n; i++)
            {
                string line = reader.ReadLine() ?? "";
                adj[i] = new List<int>();

                if (line.Trim() == "") continue;

                string[] tokens = line.Trim().Split();
                foreach (string s in tokens)
                {
                    int v = int.Parse(s);
                    adj[i].Add(v);
                }
            }
        }

        // Chuyển sang danh sách cạnh (chỉ lấy (u,v) với u < v)
        for (int u = 1; u <= n; u++)
        {
            foreach (int v in adj[u])
            {
                if (u < v)
                    edges.Add((u, v));
            }
        }

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine($"{n} {edges.Count}");
            foreach (var edge in edges)
            {
                writer.WriteLine($"{edge.Item1} {edge.Item2}");
            }
        }

        Console.WriteLine("Đã chuyển danh sách kề sang danh sách cạnh.");
    }
}
