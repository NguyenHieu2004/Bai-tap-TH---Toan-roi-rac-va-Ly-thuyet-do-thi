using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    class Edge
    {
        public int To;
        public int Weight;
        public Edge(int to, int weight)
        {
            To = to;
            Weight = weight;
        }
    }

    static void Main()
    {
        string inputPath = "Dijkstra.INP.txt";
        string outputPath = "Dijkstra.OUT.txt";

        int n, m, s, t;
        List<Edge>[] graph;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            var firstLine = reader.ReadLine()!.Split();
            n = int.Parse(firstLine[0]);
            m = int.Parse(firstLine[1]);
            s = int.Parse(firstLine[2]);
            t = int.Parse(firstLine[3]);

            graph = new List<Edge>[n + 1];
            for (int i = 1; i <= n; i++)
                graph[i] = new List<Edge>();

            for (int i = 0; i < m; i++)
            {
                var parts = reader.ReadLine()!.Split();
                int u = int.Parse(parts[0]);
                int v = int.Parse(parts[1]);
                int w = int.Parse(parts[2]);
                graph[u].Add(new Edge(v, w));
            }
        }

        int[] dist = new int[n + 1];
        int[] prev = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            dist[i] = int.MaxValue;
            prev[i] = -1;
        }
        dist[s] = 0;

        var pq = new PriorityQueue<int, int>();
        pq.Enqueue(s, 0);

        while (pq.Count > 0)
        {
            pq.TryDequeue(out int u, out int du);
            if (du > dist[u]) continue;

            foreach (var edge in graph[u])
            {
                int v = edge.To;
                int weight = edge.Weight;
                if (dist[v] > dist[u] + weight)
                {
                    dist[v] = dist[u] + weight;
                    prev[v] = u;
                    pq.Enqueue(v, dist[v]);
                }
            }
        }

        List<int> path = new();
        for (int v = t; v != -1; v = prev[v])
            path.Add(v);
        path.Reverse();

        using (StreamWriter writer = new(outputPath))
        {
            writer.WriteLine(dist[t]);
            writer.WriteLine(string.Join(" ", path));
        }

        Console.WriteLine("✅ Đã tìm đường đi ngắn nhất từ {0} đến {1}", s, t);
    }
}
