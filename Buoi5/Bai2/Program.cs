using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static int n, start, end;
    static List<int>[] adj;
    static bool[] visited;
    static int[] parent;
    static bool found = false;

    static void DFS(int u)
    {
        if (found) return;
        visited[u] = true;
        if (u == end)
        {
            found = true;
            return;
        }

        foreach (var v in adj[u])
        {
            if (!visited[v])
            {
                parent[v] = u;
                DFS(v);
                if (found) return;
            }
        }
    }

    static void Main()
    {
        string inputPath = "TimDuongDFS.INP.txt";
        string outputPath = "TimDuongDFS.OUT.txt";

        using (StreamReader reader = new(inputPath))
        {
            string[] header = reader.ReadLine()!.Split();
            n = int.Parse(header[0]);
            start = int.Parse(header[1]);
            end = int.Parse(header[2]);

            adj = new List<int>[n + 1];
            visited = new bool[n + 1];
            parent = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                adj[i] = new List<int>();
                parent[i] = -1;
            }

            for (int i = 1; i <= n; i++)
            {
                string? line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] tokens = line.Trim().Split();
                    foreach (var token in tokens)
                    {
                        int v = int.Parse(token);
                        adj[i].Add(v);
                    }
                }
            }
        }

        DFS(start);

        using (StreamWriter writer = new(outputPath))
        {
            if (!visited[end])
            {
                writer.WriteLine(0);
                Console.WriteLine("Không tìm thấy đường đi.");
            }
            else
            {
                Stack<int> path = new();
                int curr = end;
                while (curr != -1)
                {
                    path.Push(curr);
                    curr = parent[curr];
                }

                writer.WriteLine(path.Count);
                writer.WriteLine(string.Join(" ", path));
                Console.WriteLine("Đã ghi đường đi DFS đầu tiên tìm thấy.");
            }
        }
    }
}
