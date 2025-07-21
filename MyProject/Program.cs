
using System;
using System.Numerics;
using Raylib_cs;



namespace MyProject
{
    class Program
    {


        static void Main(string[] Args)
        {
            Player player = new(340f, 0f, Node2D.Shape.CIRCLE, null);
            // StaticObject Dummy = new(280f, 360f, Node2D.Shape.CIRCLE, null);
            StaticObject Dummy2 = new(0f, 200f, Node2D.Shape.RECTANGLE, null);
            PhysicsResolver ps = new();


            ps.AddObject(player);
            // ps.AddObject(Dummy);
            ps.AddObject(Dummy2);



            player.GiveCircleShape(new(60f, Color.Gold));
            // Dummy.GiveCircleShape(new(30f, Color.Black));
            Dummy2.GiveRectShape(new(new Vector2(640f, 60f), Color.Red));

            // Dummy.AddShape(new CircleCollisionShape(Dummy.position, 30f));
            player.AddShape(new CircleCollisionShape(player.position, 60f));
            Dummy2.AddShape(new RectangleCollisionShape(Dummy2.position, new Vector2(640f, 60f)));


            Raylib.InitWindow(640, 360, "MyGame");

            while (!Raylib.WindowShouldClose()){
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                player._Process();
                Dummy2.Draw();
                ps.ResolveCollision();
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();



        }
    }
}