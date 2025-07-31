using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string inputPath = "MienLienThongBFS.INP.txt";
        string outputPath = "MienLienThongBFS.OUT.txt";

        int n;
        List<int>[] adj;
        List<List<int>> components = new();

        // Đọc dữ liệu
        using (StreamReader reader = new(inputPath))
        {
            n = int.Parse(reader.ReadLine()?.Trim() ?? "0");
            adj = new List<int>[n + 1];

            for (int i = 1; i <= n; i++)
            {
                adj[i] = new List<int>();
                string? line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] tokens = line.Trim().Split();
                    foreach (var token in tokens)
                    {
                        int v = int.Parse(token);
                        adj[i].Add(v);

                        if (adj[v] == null) // Kiểm tra xem có null không, nếu null thì tạo một list mới
                            adj[v] = new List<int>();

                        adj[v].Add(i); // vô hướng
                    }
                }
            }
        }

        // BFS tìm miền liên thông
        bool[] visited = new bool[n + 1];

        for (int i = 1; i <= n; i++)
        {
            if (!visited[i])
            {
                List<int> component = new();
                Queue<int> q = new();
                q.Enqueue(i);
                visited[i] = true;

                while (q.Count > 0)
                {
                    int u = q.Dequeue();
                    component.Add(u);

                    foreach (int v in adj[u].OrderBy(x => x))
                    {
                        if (!visited[v])
                        {
                            visited[v] = true;
                            q.Enqueue(v);
                        }
                    }
                }

                component.Sort();
                components.Add(component);
            }
        }

        // Ghi kết quả
        using (StreamWriter writer = new(outputPath))
        {
            writer.WriteLine(components.Count);
            foreach (var comp in components)
            {
                writer.WriteLine(string.Join(" ", comp));
            }
        }

        Console.WriteLine("Đã ghi số miền liên thông vào file.");
    }
}
