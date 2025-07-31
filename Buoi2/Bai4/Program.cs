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
        List<int>[] reverseAdj;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            n = int.Parse(reader.ReadLine() ?? "0");
            reverseAdj = new List<int>[n + 1];
            for (int i = 1; i <= n; i++)
                reverseAdj[i] = new List<int>();

            for (int u = 1; u <= n; u++)
            {
                string line = reader.ReadLine() ?? "";
                if (line.Trim() == "") continue;

                string[] tokens = line.Trim().Split();
                foreach (string token in tokens)
                {
                    int v = int.Parse(token);
                    reverseAdj[v].Add(u); // Đảo chiều: đỉnh v nhận cạnh từ u
                }
            }
        }

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(n);
            for (int i = 1; i <= n; i++)
            {
                writer.WriteLine(string.Join(" ", reverseAdj[i]));
            }
        }

        Console.WriteLine("Đã đảo chiều danh sách kề thành danh sách các đỉnh đi vào mỗi đỉnh.");
    }
}
