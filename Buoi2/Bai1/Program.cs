using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string inputPath = "Canh2ke.INP.txt";
        string outputPath = "Canh2ke.OUT.txt";

        int n, m;
        List<int>[] adjList;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string[] firstLine = reader.ReadLine()?.Split() ?? new string[0];
            n = int.Parse(firstLine[0]);
            m = int.Parse(firstLine[1]);

            adjList = new List<int>[n + 1];
            for (int i = 1; i <= n; i++)
                adjList[i] = new List<int>();

            for (int i = 0; i < m; i++)
            {
                string[] parts = reader.ReadLine()?.Split() ?? new string[0];
                int u = int.Parse(parts[0]);
                int v = int.Parse(parts[1]);

                adjList[u].Add(v);
                adjList[v].Add(u); // Vì là đồ thị vô hướng
            }
        }

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(n);
            for (int i = 1; i <= n; i++)
            {
                adjList[i].Sort(); // Sắp xếp tăng dần
                writer.WriteLine(string.Join(" ", adjList[i]));
            }
        }

        Console.WriteLine("Đã chuyển danh sách cạnh sang danh sách kề.");
    }
}
// This code reads an edge list from a file, converts it to an adjacency list representation,