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

                string? line = reader.ReadLine();

                // Nếu thiếu dòng → cảnh báo
                if (line == null)
                {
                    Console.WriteLine($"⚠ Cảnh báo: Dữ liệu thiếu dòng thứ {i}");
                    continue;
                }

                line = line.Trim();
                if (line != "")
                {
                    string[] tokens = line.Split();

                    // Bắt lỗi
                    Console.WriteLine($"🔍 Đang xử lý đỉnh {i} với dữ liệu: '{line}'");

                    foreach (var token in tokens)
                    {
                        if (int.TryParse(token, out int v))
                        {
                            adj[i].Add(v);

                            // Kiểm tra kỹ trước khi truy cập reverseAdj[v]
                            if (v >= 1 && v <= n)
                            {
                                if (reverseAdj[v] == null)
                                {
                                    reverseAdj[v] = new List<int>();
                                }
                                reverseAdj[v].Add(i);
                            }
                            else
                            {
                                Console.WriteLine($"⚠ Cảnh báo: Đỉnh v={v} ở dòng {i} vượt phạm vi 1..{n} (bỏ qua)");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"⚠ Token '{token}' ở dòng {i} không hợp lệ.");
                        }
                    }

                }
            }
        }

        // Kiểm tra duyệt hết đồ thị từ 1
        bool IsReachable(List<int>[] graph, int start, int nodeCount)
        {
            bool[] visited = new bool[nodeCount + 1];
            Queue<int> q = new();
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

            for (int i = 1; i <= nodeCount; i++)
                if (!visited[i]) return false;

            return true;
        }

        bool forwardOK = IsReachable(adj, 1, n);
        bool backwardOK = IsReachable(reverseAdj, 1, n);

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(forwardOK && backwardOK ? "YES" : "NO");
        }

        Console.WriteLine("Đã kiểm tra liên thông và ghi kết quả.");
    }
}
