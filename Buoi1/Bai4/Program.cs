using System;
using System.IO;

class Program
{
    static void Main()
    {
        string inputPath = "EdgeList.INP.txt";
        string outputPath = "EdgeList.OUT.txt";

        int n, m;
        int[] degree;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string[] header = reader.ReadLine()?.Trim().Split() ?? new string[0];
            n = int.Parse(header[0]);
            m = int.Parse(header[1]);

            degree = new int[n + 1]; // 1-based indexing

            for (int i = 0; i < m; i++)
            {
                string[] parts = reader.ReadLine()?.Trim().Split() ?? new string[0];
                int u = int.Parse(parts[0]);
                int v = int.Parse(parts[1]);

                degree[u]++;
                degree[v]++;
            }
        }

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(n);
            for (int i = 1; i <= n; i++)
            {
                writer.Write($"{degree[i]} ");
            }
        }

        Console.WriteLine("Đã tính bậc các đỉnh từ danh sách cạnh.");
    }
}
