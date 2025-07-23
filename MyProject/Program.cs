
using System;
using System.Numerics;
using Raylib_cs;


namespace MyProject
{
    class Program
    {


        static void Main(string[] Args)
        {
            Player player = new(440f, 120f, Node2D.DrawShape.RECTANGLE, null);
            StaticObject Dummy = new(320f, 120f, Node2D.DrawShape.CIRCLE, null);
            StaticObject Dummy2 = new(0f, 200f, Node2D.DrawShape.RECTANGLE, null);
            PhysicsResolver ps = new();


            ps.AddObject(player);
            ps.AddObject(Dummy);
            ps.AddObject(Dummy2);



            player.GiveRectShape(new(new(60f), Color.Gold));
            Dummy.GiveCircleShape(new(60f, Color.Black));
            Dummy2.GiveRectShape(new(new Vector2(640f, 60f), Color.Red));

            Dummy.AddCollisionShape(new CircleCollisionShape(Dummy.Position, 60f));
            player.AddCollisionShape(new RectangleCollisionShape(player.Position, new(60f)));
            Dummy2.AddCollisionShape(new RectangleCollisionShape(Dummy2.Position, new Vector2(640f, 60f)));


            Raylib.InitWindow(640, 360, "MyGame");

            while (!Raylib.WindowShouldClose()){
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                player._Process();
                Dummy.Draw();
                Dummy2.Draw();
                player.CollShape.DebugDraw();
                Dummy2.CollShape.DebugDraw();
                ps.ResolveCollision();
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();



        }
    }
}