
using System;
using System.Numerics;
using Raylib_cs;



namespace MyProject
{
    class Program
    {


        static void Main(string[] Args)
        {
            Player player = new(0f, 0f, Node2D.Shape.CIRCLE, null);
            player.GiveCircleShape(new(60f, Color.Gold));
            Raylib.InitWindow(640, 360, "MyGame");

            while (!Raylib.WindowShouldClose()){
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                player._Process();
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();



        }
    }
}