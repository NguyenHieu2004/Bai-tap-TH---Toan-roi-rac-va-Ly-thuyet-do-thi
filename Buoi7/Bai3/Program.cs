using System;
using System.IO;

class Program
{
    static void Main()
    {
        string inputPath = @"D:\HieuNguyen\Đại học\Năm 3\HK TC\TH Toán rời rạc và Lý thuyết đồ thị\TH-TRRVLTDT\Buoi7\Bai3\FloydWarshall.INP.txt";
        string outputPath = "FloydWarshall.OUT.txt";

        string[] lines = File.ReadAllLines(inputPath);
        int n = int.Parse(lines[0]);
        int[,] dist = new int[n + 1, n + 1];

        // Đọc ma trận trọng số
        for (int i = 1; i <= n; i++)
        {
            var values = lines[i].Split();
            for (int j = 1; j <= n; j++)
            {
                dist[i, j] = int.Parse(values[j - 1]);

                // Nếu không có đường đi và i != j thì gán vô cực (giá trị rất lớn)
                if (i != j && dist[i, j] == 0)
                    dist[i, j] = int.MaxValue / 2; // Tránh tràn số khi cộng
            }
        }

        // Floyd-Warshall
        for (int k = 1; k <= n; k++)
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    if (dist[i, k] + dist[k, j] < dist[i, j])
                        dist[i, j] = dist[i, k] + dist[k, j];

        using (var writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(n);
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                    writer.Write((dist[i, j] >= int.MaxValue / 2 ? "0" : dist[i, j]) + " ");
                writer.WriteLine();
            }
        }

        Console.WriteLine("✅ Đã ghi kết quả Floyd-Warshall vào file.");
    }
}