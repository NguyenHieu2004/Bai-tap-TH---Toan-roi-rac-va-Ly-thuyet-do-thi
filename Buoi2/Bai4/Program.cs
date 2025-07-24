using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string inputPath = "DSKe2Canh.INP.txt";
        string outputPath = "DSKe2Canh.OUT.txt";

        int n;
        List<int>[] adj;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            n = int.Parse(reader.ReadLine() ?? "0");
            adj = new List<int>[n + 1];

            for (int i = 1; i <= n; i++)
            {
                string line = reader.ReadLine() ?? "";
                adj[i] = new List<int>();

                if (line.Trim() == "") continue;

                string[] tokens = line.Trim().Split();
                foreach (string token in tokens)
                {
                    int v = int.Parse(token);
                    adj[i].Add(v);
                }
            }
        }

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(n);
            for (int i = 1; i <= n; i++)
            {
                writer.WriteLine(string.Join(" ", adj[i]));
            }
        }

        Console.WriteLine("✅ Đã chuyển danh sách kề sang danh sách cạnh (theo dạng dòng).");
    }
}
