﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piyton_v0._0._1
{
    public readonly struct Pixel 
    {
        private const char PixelChar = '█';
        public Pixel(int x, int y,ConsoleColor color, int pixelSize = 3) 
        {
            X = x;
            Y = y;  
            Color = color;
            PixelSize = pixelSize;
        }
        public int X {  get; }
        public int Y {  get; }

        public ConsoleColor Color { get; }

        private int PixelSize { get; }

        public void Draw() 
        {
            Console.ForegroundColor = Color;
            for(int x = 0; x < PixelSize; x++) 
            {
                for(int y = 0; y < PixelSize; y++) 
                {
                    Console.SetCursorPosition(X*PixelSize + x, Y*PixelSize + y);
                    Console.Write(PixelChar);
                }
            }
            
        }
        public void Clear()
        {
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);
                    Console.Write(' ');
                }
            }
        }
    }
}
