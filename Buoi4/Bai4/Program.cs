using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string inputPath = "Grid.INP.txt";
        string outputPath = "Grid.OUT.txt";

        int m, n, sx, sy, ex, ey;
        int[,] grid;
        (int, int)[,] prev;

        // Đọc input từ file
        using (StreamReader reader = new(inputPath))
        {
            string[] size = reader.ReadLine()!.Split();
            m = int.Parse(size[0]);
            n = int.Parse(size[1]);

            string[] points = reader.ReadLine()!.Split();
            sx = int.Parse(points[0]) - 1;
            sy = int.Parse(points[1]) - 1;
            ex = int.Parse(points[2]) - 1;
            ey = int.Parse(points[3]) - 1;

            grid = new int[m, n];
            prev = new (int, int)[m, n];

            for (int i = 0; i < m; i++)
            {
                string[] row = reader.ReadLine()!.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < n; j++)
                {
                    grid[i, j] = int.Parse(row[j]);
                    prev[i, j] = (-1, -1);
                }
            }
        }

        // Kiểm tra điểm bắt đầu và kết thúc có hợp lệ không
        if (grid[sx, sy] != 0 || grid[ex, ey] != 0)
        {
            using (StreamWriter writer = new(outputPath))
            {
                writer.WriteLine(0);
            }
            Console.WriteLine("⛔ Điểm bắt đầu hoặc kết thúc bị chặn.");
            return;
        }

        // BFS để tìm đường
        bool[,] visited = new bool[m, n];
        Queue<(int, int)> q = new();
        q.Enqueue((sx, sy));
        visited[sx, sy] = true;
        prev[sx, sy] = (-2, -2); // dấu hiệu bắt đầu

        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        while (q.Count > 0)
        {
            var (x, y) = q.Dequeue();
            for (int dir = 0; dir < 4; dir++)
            {
                int nx = x + dx[dir];
                int ny = y + dy[dir];
                if (nx >= 0 && nx < m && ny >= 0 && ny < n && !visited[nx, ny] && grid[nx, ny] == 0)
                {
                    visited[nx, ny] = true;
                    prev[nx, ny] = (x, y);
                    q.Enqueue((nx, ny));
                }
            }
        }

        // Truy vết đường đi
        List<(int, int)> path = new();
        int cx = ex, cy = ey;

        if (!visited[ex, ey])
        {
            using (StreamWriter writer = new(outputPath))
            {
                writer.WriteLine(0);
            }
            Console.WriteLine("❌ Không tìm thấy đường đi.");
            return;
        }

        while (cx != -2 && cy != -2)
        {
            path.Add((cx + 1, cy + 1)); // đưa về chỉ số 1-based
            (cx, cy) = prev[cx, cy];
        }

        path.Reverse();

        using (StreamWriter writer = new(outputPath))
        {
            writer.WriteLine(path.Count);
            foreach (var (x, y) in path)
                writer.WriteLine($"{x}\t{y}");
        }

        Console.WriteLine("Đã ghi đường đi vào file.");
    }
}
