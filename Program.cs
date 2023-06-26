using Piyton_v0._0._1;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Console;


class Programm
{
    public const int MapWidth = 30;
    public const int MapHeight = 20;

    private const int ScreenWidth = MapWidth * 3;
    private const int ScreenHeight = MapHeight * 3;

    private const int FrameMd = 200;

    private const ConsoleColor BorderColor = ConsoleColor.Gray;
    private const ConsoleColor HeadColor = ConsoleColor.Cyan;
    private const ConsoleColor BodyColor = ConsoleColor.Blue;
    private const ConsoleColor FoodColor = ConsoleColor.Green;
    private static readonly Random Random = new Random();
    static void Main() 
    { 
        SetWindowSize(ScreenWidth, ScreenHeight);
        SetBufferSize(ScreenWidth, ScreenHeight);
        CursorVisible = false;              // отключаем курсор
        
        while (true) 
        {
            StartGame();
            Thread.Sleep(100);
            ReadKey();
        }

    }

    static void StartGame() 
    {
        Clear();
        DrawBorder();

        Direction currentMovement = Direction.Right;

        var snake = new Snake(10, 5, HeadColor, BodyColor);

        Pixel food = GenFood(snake);
        food.Draw();
        int score = 0;
        Stopwatch sw = new Stopwatch();
        while (true)
        {
            sw.Restart();
            Direction oldMovement = currentMovement;
            while (sw.ElapsedMilliseconds <= FrameMd)
            {
                if (currentMovement == oldMovement)
                {
                    currentMovement = ReadMovement(currentMovement);

                }
            }

            if(snake.Head.X == food.X && snake.Head.Y == food.Y) 
            {
                snake.Move(currentMovement,true);
                food = GenFood(snake);
                food.Draw();
                score++;
                Task.Run(() => Beep(1200, 200));
            }
            else 
            {
                snake.Move(currentMovement);

               
            }
            

            if (snake.Head.X == MapWidth - 1
                || snake.Head.Y == MapHeight - 1
                || snake.Head.X == 0
                || snake.Head.Y == 0
                || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                break;
        }

        snake.Clear();
        food.Clear();

        SetCursorPosition(ScreenWidth / 3, ScreenHeight / 2);
        WriteLine($"Game over Score {score}");

        Task.Run(() => Beep(200, 600));
    }

    static Pixel GenFood(Snake snake) 
    {
        Pixel food;

        do
        {
            food = new Pixel(Random.Next(1, MapWidth - 2), Random.Next(1, MapHeight - 2), FoodColor);
        } while (snake.Head.X == food.X && snake.Head.Y == food.Y
        || snake.Body.Any(b => b.X == food.X && b.Y == food.Y));
        return food;
    }

    static Direction ReadMovement(Direction currentDirection) 
    {
        if(!KeyAvailable) { return currentDirection; }
        ConsoleKey key = ReadKey(true).Key;
        currentDirection = key switch
        {
            ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
            ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
            ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
            ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
            _ => currentDirection
        };
        return currentDirection;
    }

    static void DrawBorder() 
    {
        for(int i = 0; i < MapWidth; i++) 
        {
            new Pixel(i, 0, BorderColor).Draw();
            new Pixel(i,MapHeight-1, BorderColor).Draw();
        }
        for(int i = 0;i < MapHeight; i++) 
        {
            new Pixel(0,i, BorderColor).Draw();
            new Pixel(MapWidth-1,i, BorderColor).Draw();
        }
    }
}