
using System;
using System.Numerics;
using Raylib_cs;



namespace MyProject
{
    class Program
    {


        static void Main(string[] Args)
        {
            Player player = new(340f, 0f, Node2D.Shape.RECTANGLE, null);
            StaticObject Dummy = new(320f, 0f, Node2D.Shape.RECTANGLE, null);
            StaticObject Dummy2 = new(0f, 200f, Node2D.Shape.RECTANGLE, null);
            PhysicsResolver ps = new();


            ps.AddObject(player);
            ps.AddObject(Dummy);
            ps.AddObject(Dummy2);



            player.GiveRectShape(new(new Vector2(60f, 60f), Color.Gold));
            Dummy.GiveRectShape(new(new Vector2(80f, 80f), Color.Black));
            Dummy2.GiveRectShape(new(new Vector2(640f, 60f), Color.Red));

            Dummy.AddShape(new RectangleCollisionShape(Dummy.position, new Vector2(80f, 80f)));
            player.AddShape(new RectangleCollisionShape(player.position, new Vector2(60f)));
            Dummy2.AddShape(new RectangleCollisionShape(Dummy2.position, new Vector2(640f, 60f)));


            Raylib.InitWindow(640, 360, "MyGame");

            while (!Raylib.WindowShouldClose()){
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                player._Process();
                Dummy.Draw();
                Dummy2.Draw();
                ps.ResolveCollision();
                // Console.WriteLine(player.CollShape.GetIntersectionDisplacement(Dummy2.CollShape));
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();



        }
    }
}