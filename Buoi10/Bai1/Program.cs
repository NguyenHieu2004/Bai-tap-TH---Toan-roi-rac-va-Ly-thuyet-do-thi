using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static int n;
    static int[,] adj;
    static bool[] visited;

    static void DFS(int u)
    {
        visited[u] = true;
        for (int v = 1; v <= n; v++)
            if (adj[u, v] > 0 && !visited[v])
                DFS(v);
    }

    static bool IsConnected()
    {
        visited = new bool[n + 1];

        // Tìm đỉnh đầu tiên có ít nhất 1 cạnh
        int start = -1;
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (adj[i, j] > 0)
                {
                    start = i;
                    break;
                }
            }
            if (start != -1) break;
        }

        if (start == -1) return true; // Đồ thị rỗng (coi như liên thông)

        DFS(start);

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if ((adj[i, j] > 0 || adj[j, i] > 0) && !visited[i])
                    return false;
            }
        }

        return true;
    }

    static void Main()
    {
        string inputPath = "Euler.INP.txt";
        string outputPath = "Euler.OUT.txt";

        string[] lines = File.ReadAllLines(inputPath);
        n = int.Parse(lines[0]);
        adj = new int[n + 1, n + 1];

        for (int i = 1; i <= n; i++)
        {
            var values = lines[i].Split();
            for (int j = 1; j <= n; j++)
                adj[i, j] = int.Parse(values[j - 1]);
        }

        if (!IsConnected())
        {
            File.WriteAllText(outputPath, "0");
            Console.WriteLine("❌ Không liên thông → Không có Euler");
            return;
        }

        int oddCount = 0;
        for (int i = 1; i <= n; i++)
        {
            int deg = 0;
            for (int j = 1; j <= n; j++)
                deg += adj[i, j];
            if (deg % 2 == 1) oddCount++;
        }

        int result = oddCount == 0 ? 1 : (oddCount == 2 ? 2 : 0);
        File.WriteAllText(outputPath, result.ToString());
        Console.WriteLine($"✅ Kết quả: {result}");
    }
}
