using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    class Edge : IComparable<Edge>
    {
        public int From, To, Weight;
        public Edge(int from, int to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public int CompareTo(Edge other) => Weight.CompareTo(other.Weight);
    }

    static int n, m;
    static List<(int to, int weight)>[] adj;
    static bool[] visited;
    static List<Edge> mstEdges = new();
    static int totalWeight = 0;

    static void Prim(int start)
    {
        visited = new bool[n + 1];
        var pq = new PriorityQueue<Edge, int>();

        visited[start] = true;
        foreach (var (to, w) in adj[start])
            pq.Enqueue(new Edge(start, to, w), w);

        while (pq.Count > 0 && mstEdges.Count < n - 1)
        {
            var edge = pq.Dequeue();
            if (visited[edge.To]) continue;

            visited[edge.To] = true;
            mstEdges.Add(edge);
            totalWeight += edge.Weight;

            foreach (var (next, weight) in adj[edge.To])
                if (!visited[next])
                    pq.Enqueue(new Edge(edge.To, next, weight), weight);
        }
    }

    static void Main()
    {
        string inputPath = "Prim.INP.txt";
        string outputPath = "Prim.OUT.txt";
        string[] lines = File.ReadAllLines(inputPath);
        var tokens = lines[0].Split();
        n = int.Parse(tokens[0]);
        m = int.Parse(tokens[1]);

        adj = new List<(int, int)>[n + 1];
        for (int i = 1; i <= n; i++) adj[i] = new List<(int, int)>();

        for (int i = 1; i <= m; i++)
        {
            var edge = lines[i].Split();
            int u = int.Parse(edge[0]);
            int v = int.Parse(edge[1]);
            int w = int.Parse(edge[2]);

            adj[u].Add((v, w));
            adj[v].Add((u, w));
        }

        Prim(1);

        using (var writer = new StreamWriter(outputPath))
        {
            writer.WriteLine($"{mstEdges.Count} {totalWeight}");
            foreach (var edge in mstEdges)
                writer.WriteLine($"{edge.From} {edge.To} {edge.Weight}");
        }

        Console.WriteLine("✅ Đã tìm xong cây khung nhỏ nhất bằng thuật toán Prim.");
    }
}
