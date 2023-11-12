using System;

class Program
{
    static int[,] map = new int[4, 5]
    {
        { 0, 0, 0, 0, 3},
        { 0, 0, 1, 1, 1},
        { 1, 0, 0, 0, 0},
        { 1, 0, 1, 1, 2},
    };

    static int playerX = -1;
    static int playerY = -1;

    static void Main()
    {
        // 초기 플레이어 위치 설정
        SetInitPlayerPosition();

        // 초기 맵 출력
        PrintMap();

        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey();

            // 사용자가 오른쪽 화살표 키를 누르면 오른쪽으로 이동
            if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                MoveRight();
                PrintMap();
            }

            // 사용자가 왼쪽 화살표 키를 누르면 왼쪽으로 이동
            if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                MoveLeft();
                PrintMap();
            }

            if(keyInfo.Key == ConsoleKey.UpArrow)
            {
                MoveUp();
                PrintMap();
            }

            if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                MoveDown();
                PrintMap();
            }

        } while (!(playerX == 4 && playerY == 3) && keyInfo.Key != ConsoleKey.Escape); // ESC 키를 누를 때까지 반복
    }

    static void SetInitPlayerPosition()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 3)
                {
                    playerX = j;
                    playerY = i;
                    return;
                }
            }
        }
    }

    static void MoveRight()
    {
        if (playerX < map.GetLength(1) - 1 && map[playerY, playerX + 1] != 1)
        {
            map[playerY, playerX] = 0;
            playerX++;
            map[playerY, playerX] = 3;
        }
    }

    static void MoveLeft()
    {
        if (playerX > 0 && map[playerY, playerX - 1] != 1)
        {
            map[playerY, playerX] = 0;
            playerX--;
            map[playerY, playerX] = 3;
        }
    }

    static void MoveUp()
    {
        if (playerY > 0 && map[playerY -1, playerX ] != 1)
        {
            map[playerY, playerX] = 0;
            playerY--;
            map[playerY, playerX] = 3;
        }
    }

    static void MoveDown()
    {
        if (playerY < map.GetLength(0) - 1 && map[playerY + 1, playerX] != 1)
        {
            map[playerY, playerX] = 0;
            playerY++;
            map[playerY, playerX] = 3;
        }
    }

    static void PrintMap()
    {
        Console.Clear(); // 화면 지우기
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}