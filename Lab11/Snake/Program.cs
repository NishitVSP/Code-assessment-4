﻿using System;
using System.Collections.Generic;
using System.Linq;

Exception? exception = null;
int speedInput;
string prompt = $"Select speed [1], [2] (default), or [3]: ";
string? input;
Console.Write(prompt);
while (!int.TryParse(input = Console.ReadLine(), out speedInput) || speedInput < 1 || 3 < speedInput)
{
    if (string.IsNullOrWhiteSpace(input))
    {
        speedInput = 2;
        break;
    }
    else
    {
        Console.WriteLine("Invalid Input. Try Again...");
        Console.Write(prompt);
    }
}
int[] velocities = [50, 70, 100];
int velocity = velocities[speedInput - 1];
char[] DirectionChars = ['^', 'v', '<', '>'];
TimeSpan sleep = TimeSpan.FromMilliseconds(velocity);
int width = Console.WindowWidth;
int height = Console.WindowHeight;
Tile[,] map = new Tile[width, height];
Direction? direction = null;
Queue<(int X, int Y)> snake = new();
(int X, int Y) = (width / 2, height / 2);
bool closeRequested = false;

try
{
    Console.CursorVisible = false;
    Console.Clear();
    snake.Enqueue((X, Y));
    map[X, Y] = Tile.Snake;
    PositionFood();
    Console.SetCursorPosition(X, Y);
    Console.Write('@');
    while (!direction.HasValue && !closeRequested)
    {
        GetDirection();
    }
    while (!closeRequested)
    {
        if (Console.WindowWidth != width || Console.WindowHeight != height)
        {
            Console.Clear();
            Console.Write("Console was resized. Snake game has ended.");
            return;
        }

        // Store tail before moving
        var tail = snake.Count > 0 ? snake.Peek() : (X, Y);

        // Move head
        switch (direction)
        {
            case Direction.Up: Y--; break;
            case Direction.Down: Y++; break;
            case Direction.Left: X--; break;
            case Direction.Right: X++; break;
        }

        // Collision check (allow tail's previous position)
        if (X < 0 || X >= width ||
            Y < 0 || Y >= height ||
            (map[X, Y] is Tile.Snake && (X, Y) != tail))
        {
            Console.Clear();
            Console.Write("Game Over. Score: " + (snake.Count - 1) + ".");
            return;
        }

        Console.SetCursorPosition(X, Y);
        Console.Write(DirectionChars[(int)direction!]);
        snake.Enqueue((X, Y));
        if (map[X, Y] is Tile.Food)
        {
            PositionFood();
        }
        else
        {
            (int x, int y) = snake.Dequeue();
            map[x, y] = Tile.Open;
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }
        map[X, Y] = Tile.Snake;
        if (Console.KeyAvailable)
        {
            GetDirection();
        }
        System.Threading.Thread.Sleep(sleep);
    }
}
catch (Exception e)
{
    exception = e;
    throw;
}
finally
{
    Console.CursorVisible = true;
    Console.Clear();
    Console.WriteLine(exception?.ToString() ?? "Snake was closed.");
}

void GetDirection()
{
    Direction? newDirection = direction;
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow when direction != Direction.Down:
            newDirection = Direction.Up;
            break;
        case ConsoleKey.DownArrow when direction != Direction.Up:
            newDirection = Direction.Down;
            break;
        case ConsoleKey.LeftArrow when direction != Direction.Right:
            newDirection = Direction.Left;
            break;
        case ConsoleKey.RightArrow when direction != Direction.Left:
            newDirection = Direction.Right;
            break;
        case ConsoleKey.Escape:
            closeRequested = true;
            break;
    }

    // Allow 180-degree turn only if snake length is 1
    if (snake.Count == 1)
    {
        direction = newDirection;
    }
    else
    {
        direction = newDirection ?? direction;
    }
}

void PositionFood()
{
    List<(int X, int Y)> possibleCoordinates = new();
    for (int i = 0; i < width; i++)
    {
        for (int j = 0; j < height; j++)
        {
            if (map[i, j] is Tile.Open)
            {
                possibleCoordinates.Add((i, j));
            }
        }
    }
    int index = Random.Shared.Next(possibleCoordinates.Count);
    (int X, int Y) = possibleCoordinates[index];
    map[X, Y] = Tile.Food;
    Console.SetCursorPosition(X, Y);
    Console.Write('+');
}

enum Direction
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3,
}

enum Tile
{
    Open = 0,
    Snake,
    Food,
}
