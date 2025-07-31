using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string inputPath = "PhanDoi.INP.txt";
        string outputPath = "PhanDoi.OUT.txt";

        int n;
        List<int>[] adj;
        int[] color;

        using (StreamReader reader = new(inputPath))
        {
            n = int.Parse(reader.ReadLine()!);
            adj = new List<int>[n + 1];
            color = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {
                adj[i] = new List<int>();
                string? line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    foreach (var v in line.Trim().Split())
                        adj[i].Add(int.Parse(v));
                }
            }
        }

        bool isBipartite = true;
        for (int i = 1; i <= n; i++)
        {
            if (color[i] == 0)
            {
                Queue<int> q = new();
                q.Enqueue(i);
                color[i] = 1;

                while (q.Count > 0 && isBipartite)
                {
                    int u = q.Dequeue();
                    foreach (int v in adj[u])
                    {
                        if (color[v] == 0)
                        {
                            color[v] = 3 - color[u]; // xen kẽ 1 và 2
                            q.Enqueue(v);
                        }
                        else if (color[v] == color[u])
                        {
                            isBipartite = false;
                            break;
                        }
                    }
                }
            }
        }

        using (StreamWriter writer = new(outputPath))
        {
            writer.WriteLine(isBipartite ? "YES" : "NO");
        }

        Console.WriteLine($"Kết quả đã ghi ra file {outputPath}");
    }
}
