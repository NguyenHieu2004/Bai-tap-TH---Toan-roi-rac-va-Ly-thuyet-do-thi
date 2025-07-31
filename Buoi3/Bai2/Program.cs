using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 📄 Đọc file input và khai báo đường dẫn file output
        string inputPath = "TimDuong.INP.txt";
        string outputPath = "TimDuong.OUT.txt";

        int n, s, t;
        List<int>[] adj;

        // 📌 Đọc dữ liệu từ file
        using (StreamReader reader = new StreamReader(inputPath))
        {
            // Đọc dòng đầu: số đỉnh `n`, đỉnh bắt đầu `s`, đỉnh kết thúc `t`
            string[] firstLine = reader.ReadLine()?.Trim().Split() ?? new string[0];
            n = int.Parse(firstLine[0]);
            s = int.Parse(firstLine[1]);
            t = int.Parse(firstLine[2]);

            // Khởi tạo danh sách kề cho từng đỉnh
            adj = new List<int>[n + 1];
            for (int i = 1; i <= n; i++)
            {
                string line = reader.ReadLine()?.Trim() ?? "";
                adj[i] = new List<int>();

                if (line != "")
                {
                    string[] neighbors = line.Split();
                    foreach (string v in neighbors)
                    {
                        adj[i].Add(int.Parse(v));
                    }

                    // ⚠️ Sắp xếp để đảm bảo thứ tự duyệt tăng dần (đề yêu cầu)
                    adj[i].Sort();
                }
            }
        }

        // Thực hiện BFS từ đỉnh s
        int[] trace = new int[n + 1]; // truy vết đường đi
        bool[] visited = new bool[n + 1];

        Queue<int> queue = new Queue<int>();
        visited[s] = true;
        queue.Enqueue(s);

        while (queue.Count > 0) {
            int u = queue.Dequeue();
            foreach (int v in adj[u])
            {
                if (!visited[v])
                {
                    visited[v] = true;
                    trace[v] = u; // Ghi nhớ đỉnh cha để lát truy vết đường đi
                    queue.Enqueue(v);
                }
            }
        }

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            // ❌ Không tìm được đường đi
            if (!visited[t])
            {
                writer.WriteLine("-1");
            }
            else
            {
                // ✅ Truy vết đường đi ngược từ `t` về `s`
                List<int> path = new List<int>();
                int cur = t;
                while (cur != s)
                {
                    path.Add(cur);
                    cur = trace[cur];
                }
                path.Add(s);
                path.Reverse(); // Vì vừa rồi truy ngược nên cần đảo lại

                // ✍️ Ghi ra file
                writer.WriteLine(path.Count);
                writer.WriteLine(string.Join(" ", path));
            }
        }

        Console.WriteLine("Đã tìm đường đi từ s đến t và ghi vào file.");
    }
}
