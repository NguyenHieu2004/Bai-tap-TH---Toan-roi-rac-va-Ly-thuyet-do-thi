using System;
using System.IO;

class Program
{
    static void Main()
    {
        string inputPath = "AdjecencyMatrix.INP.txt";
        string outputPath = "AdjecencyMatrix.OUT.txt";

        int[,] matrix;
        int n;

        // Đọc file input
        using (StreamReader reader = new StreamReader(inputPath))
        {
            n = int.Parse(reader.ReadLine());
            matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                string[] parts = reader.ReadLine().Split();
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = int.Parse(parts[j]);
                }
            }
        }

        // Tính bậc của từng đỉnh
        int[] degree = new int[n];
        for (int i = 0; i < n; i++)
        {
            int count = 0;
            for (int j = 0; j < n; j++)
            {
                count += matrix[i, j];
            }
            degree[i] = count;
        }

        // Ghi file output
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(n);
            writer.WriteLine(string.Join(" ", degree));
        }

        Console.WriteLine("Đã tính xong bậc các đỉnh. Kết quả nằm trong file AdjecencyMatrix.OUT");
    }
}
