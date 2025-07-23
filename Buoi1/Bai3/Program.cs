using System;
using System.IO;

class Program
{
    static void Main()
    {
        string inputPath = "AdjecencyList.INP.txt";
        string outputPath = "AdjecencyList.OUT.txt";

        int n;
        int[] degrees;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            n = int.Parse(reader.ReadLine() ?? "0");
            degrees = new int[n];

            for (int i = 0; i < n; i++)
            {
                string line = reader.ReadLine() ?? "";
                if (line.Trim() == "") continue;

                string[] neighbors = line.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                degrees[i] = neighbors.Length;
            }
        }

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(n);
            for (int i = 0; i < n; i++)
            {
                writer.Write($"{degrees[i]} ");
            }
        }

        Console.WriteLine("Đã tính bậc các đỉnh từ danh sách kề.");
    }
}
