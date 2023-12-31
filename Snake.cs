﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piyton_v0._0._1
{
    public class Snake
    {
        private readonly ConsoleColor _headColor;
        private readonly ConsoleColor _bodyColor;


        public Snake(int initialeX, 
                        int initialeY,
                        ConsoleColor headColor,
                        ConsoleColor bodyColor,
                        int bodyLeght = 3) 
        {
            _headColor = headColor;
            _bodyColor = bodyColor;
            Head = new Pixel(initialeX, initialeY, headColor);

            for(int i = bodyLeght; i >= 0; i--) 
            {
                Body.Enqueue(new Pixel(Head.X - i -1 , initialeY, _bodyColor));
            }
            Draw();
        }
        public Pixel Head { get; private set; }
        public Queue<Pixel> Body { get; } = new Queue<Pixel>(); 
        
        public void Move(Direction direction, bool eat = false) 
        {
            Clear();
            Body.Enqueue(new Pixel(Head.X, Head.Y, _bodyColor));
            if(!eat) 
                Body.Dequeue();
            Head = direction switch
            {
                Direction.Up => new Pixel(Head.X, Head.Y - 1, _headColor),
                Direction.Down => new Pixel(Head.X, Head.Y + 1, _headColor),
                Direction.Left => new Pixel(Head.X - 1, Head.Y, _headColor),
                Direction.Right => new Pixel(Head.X + 1, Head.Y, _headColor),
                _ => Head
            };
            Draw();
        }
        public void Draw() 
        {
            Head.Draw();
            foreach(Pixel pixel in Body) 
            {
                pixel.Draw();
            }
        }
        public void Clear()
        {
            Head.Clear();
            foreach(Pixel pixel in Body)
            {   
                pixel.Clear();
            }
        }
    }
}
