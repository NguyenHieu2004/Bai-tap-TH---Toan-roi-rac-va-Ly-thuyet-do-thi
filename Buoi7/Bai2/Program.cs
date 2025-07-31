using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    class Edge
    {
        public int To, Weight;
        public Edge(int to, int weight)
        {
            To = to;
            Weight = weight;
        }
    }

    static int n, m, s, t, x;
    static List<Edge>[] graph;

    static int[] Dijkstra(int start, out int[] trace)
    {
        int[] dist = new int[n + 1];
        trace = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            dist[i] = int.MaxValue;
            trace[i] = -1;
        }
        dist[start] = 0;

        var pq = new PriorityQueue<(int node, int dist), int>();
        pq.Enqueue((start, 0), 0);

        while (pq.Count > 0)
        {
            var (u, d) = pq.Dequeue();
            if (d > dist[u]) continue;

            foreach (var edge in graph[u])
            {
                int v = edge.To, w = edge.Weight;
                if (dist[v] > dist[u] + w)
                {
                    dist[v] = dist[u] + w;
                    trace[v] = u;
                    pq.Enqueue((v, dist[v]), dist[v]);
                }
            }
        }

        return dist;
    }

    static List<int> ReconstructPath(int[] trace, int start, int end)
    {
        var path = new List<int>();
        for (int u = end; u != -1; u = trace[u])
            path.Add(u);
        path.Reverse();

        if (path[0] != start) return new List<int>(); // Không tồn tại đường
        return path;
    }

    static void Main()
    {
        string inputPath = "NganNhat.INP.txt";
        string outputPath = "NganNhat.OUT.txt";

        string[] lines = File.ReadAllLines(inputPath);
        var tokens = lines[0].Split();
        n = int.Parse(tokens[0]);
        m = int.Parse(tokens[1]);
        s = int.Parse(tokens[2]);
        t = int.Parse(tokens[3]);
        x = int.Parse(tokens[4]);

        graph = new List<Edge>[n + 1];
        for (int i = 1; i <= n; i++)
            graph[i] = new List<Edge>();

        for (int i = 1; i <= m; i++)
        {
            var edgeInfo = lines[i].Split();
            int u = int.Parse(edgeInfo[0]);
            int v = int.Parse(edgeInfo[1]);
            int w = int.Parse(edgeInfo[2]);

            // ✅ Đồ thị vô hướng: thêm cả hai chiều
            graph[u].Add(new Edge(v, w));
            graph[v].Add(new Edge(u, w));
        }

        int[] trace1, trace2;
        var dist1 = Dijkstra(s, out trace1); // s -> x
        var dist2 = Dijkstra(x, out trace2); // x -> t

        var path1 = ReconstructPath(trace1, s, x);
        var path2 = ReconstructPath(trace2, x, t);

        using (var writer = new StreamWriter(outputPath))
        {
            if (path1.Count == 0 || path2.Count == 0)
            {
                writer.WriteLine("NO PATH");
                Console.WriteLine("❌ Không tìm được đường đi s → x → t.");
            }
            else
            {
                path2.RemoveAt(0); // bỏ x tránh trùng
                path1.AddRange(path2);
                int total = dist1[x] + dist2[t];

                writer.WriteLine(total);
                writer.WriteLine(string.Join(" ", path1));

                Console.WriteLine($"✅ Tổng độ dài: {total}");
                Console.WriteLine("Đường đi s → x → t: " + string.Join(" ", path1));
            }
        }
    }
}
