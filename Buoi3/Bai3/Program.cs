using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string inputPath = "LienThong.INP.txt";
        string outputPath = "LienThong.OUT.txt";

        int n;
        List<int>[] adj, reverseAdj;

        // Đọc file input
        using (StreamReader reader = new StreamReader(inputPath))
        {
            n = int.Parse(reader.ReadLine()?.Trim() ?? "0");

            adj = new List<int>[n + 1];
            reverseAdj = new List<int>[n + 1];

            for (int i = 1; i <= n; i++)
            {
                adj[i] = new List<int>();
                reverseAdj[i] = new List<int>();

                string line = reader.ReadLine()?.Trim() ?? "";
                if (line != "")
                {
                    string[] neighbors = line.Split();
                    foreach (var token in neighbors)
                    {
                        int v = int.Parse(token);
                        adj[i].Add(v);             // Gốc → Đích
                        reverseAdj[v].Add(i);      // Đảo chiều: Đích → Gốc
                    }
                }
            }
        }

        // Hàm kiểm tra BFS có đi được hết không
        bool IsReachable(List<int>[] graph, int start, int nodeCount)
        {
            bool[] visited = new bool[nodeCount + 1];
            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            visited[start] = true;

            while (q.Count > 0)
            {
                int u = q.Dequeue();
                foreach (int v in graph[u])
                {
                    if (!visited[v])
                    {
                        visited[v] = true;
                        q.Enqueue(v);
                    }
                }
            }

            // Kiểm tra tất cả đỉnh đều được thăm
            for (int i = 1; i <= nodeCount; i++)
            {
                if (!visited[i])
                    return false;
            }

            return true;
        }

        // BFS cả 2 chiều
        bool forwardOK = IsReachable(adj, 1, n);        // Gốc → Đích
        bool backwardOK = IsReachable(reverseAdj, 1, n); // Đích → Gốc

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            if (forwardOK && backwardOK)
                writer.WriteLine("YES");
            else
                writer.WriteLine("NO");
        }

        Console.WriteLine("Đã kiểm tra liên thông và ghi kết quả.");
    }
}
