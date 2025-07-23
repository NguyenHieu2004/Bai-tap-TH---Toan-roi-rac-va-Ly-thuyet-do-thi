using System;
using System.IO;

class Program
{
    static void Main()
    {
        string inputPath = "BacVaoRa.INP.txt";
        string outputPath = "BacVaoRa.OUT.txt";

        int n;
        int[,] matrix;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            n = int.Parse(reader.ReadLine() ?? "0");
            matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                string[] parts = reader.ReadLine().Trim().Split('\t');
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = int.Parse(parts[j]);
                }
            }
        }

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(n);
            for (int i = 0; i < n; i++)
            {
                int outDegree = 0;
                int inDegree = 0;

                for (int j = 0; j < n; j++)
                {
                    outDegree += matrix[i, j]; // bậc ra
                    inDegree += matrix[j, i];  // bậc vào
                }

                writer.WriteLine($"{inDegree} {outDegree}");
            }
        }

        Console.WriteLine("Đã tính bậc vào và bậc ra thành công. Xem BacVaoRa.OUT nha!");
    }
}
// This program reads a matrix from a file, calculates the in-degree and out-degree for each vertex,