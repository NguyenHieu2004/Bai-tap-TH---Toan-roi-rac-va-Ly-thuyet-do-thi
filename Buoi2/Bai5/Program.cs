using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main()
    {
        string inputPath = "TrungBinhCanh.INP.txt";
        string outputPath = "TrungBinhCanh.OUT.txt";

        int n, m;
        List<(int u, int v, int w)> edges = new();

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string[] firstLine = reader.ReadLine()?.Trim().Split() ?? new string[0];
            n = int.Parse(firstLine[0]);
            m = int.Parse(firstLine[1]);

            for (int i = 0; i < m; i++)
            {
                string[] parts = reader.ReadLine()?.Trim().Split() ?? new string[0];
                int u = int.Parse(parts[0]);
                int v = int.Parse(parts[1]);
                int w = int.Parse(parts[2]);
                edges.Add((u, v, w));
            }
        }

        double average = 0;
        foreach (var edge in edges)
            average += edge.w;

        average /= edges.Count;

        List<(int u, int v, int w)> greaterEdges = new();
        foreach (var edge in edges)
        {
            if (edge.w > average)
                greaterEdges.Add(edge);
        }

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(average.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteLine(greaterEdges.Count);
            foreach (var edge in greaterEdges)
            {
                writer.WriteLine($"{edge.u} {edge.v} {edge.w}");
            }
        }

        Console.WriteLine("Đã tính trung bình và lọc các cạnh lớn hơn trung bình.");
    }
}
