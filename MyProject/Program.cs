
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Raylib_cs;



namespace MyProject
{
    class Program
    {


        static void Main(string[] Args)
        {
            Player player = new(0f, 0f, Node2D.Shape.CIRCLE, null);
            StaticObject Dummy = new(340f, 180f, Node2D.Shape.CIRCLE, null);
            StaticObject Dummy2 = new(340f, 255f, Node2D.Shape.CIRCLE, null);
            PhysicsResolver ps = new();


            ps.AddObject(player);
            ps.AddObject(Dummy);
            ps.AddObject(Dummy2);



            player.GiveCircleShape(new(60f, Color.Gold));
            Dummy.GiveCircleShape(new(30f, Color.Black));
            Dummy2.GiveCircleShape(new(40, Color.Red));

            Dummy.AddShapeNoDel(new CircleCollisionShape(Dummy.position, 30f));
            Dummy2.AddShapeNoDel(new CircleCollisionShape(Dummy2.position, 40f));
            player.AddShapeNoDel(new CircleCollisionShape(player.position, 60f));


            Raylib.InitWindow(640, 360, "MyGame");

            while (!Raylib.WindowShouldClose()){
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                player._Process();
                Dummy.Draw();
                // Dummy.CollShape.DebugDraw();
                Dummy2.Draw();
                // Dummy2.CollShape.DebugDraw();
                
                ps.ResolveCollision();
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();



        }
    }
}