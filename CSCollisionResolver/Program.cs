
using System;
using System.Numerics;
using System.Reflection;
using Raylib_cs;


namespace MyProject
{
    class Program
    {


        static void Main(string[] Args)
        {
            PhysicsResolver ps = new();
            // Player player = new(440f, 120f, Node2D.DrawShape.RECTANGLE, null);
            Player player = MakeCircleKinematicObject(new(320f, 60f), 50f, Color.Black, null, ps);
            StaticObject Dummy = MakeCircleStaticObject(new(320f, 120f), 60f, Color.Red, null, ps );
            StaticObject Dummy2 = MakeRectangleStaticObject(new(0f, 200f), new(640f, 60f), Color.Black, null, ps);


            Raylib.InitWindow(640, 360, "MyGame");

            while (!Raylib.WindowShouldClose()){
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                player._Process();
                Dummy.Draw();
                Dummy2.Draw();
                player.CollShape.DebugDraw();
                Dummy2.CollShape.DebugDraw();
                Dummy.CollShape.DebugDraw();
                ps.ResolveCollision();
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();



        }


        public static StaticObject MakeRectangleStaticObject(Vector2 PPosition, Vector2 PSize, Color PColor, Node? PParent, PhysicsResolver PPS) {
            StaticObject Result = new StaticObject(PPosition.X, PPosition.Y, Node2D.DrawShape.RECTANGLE, PParent);
            Result.GiveRectShape(new(PSize, PColor));
            Result.AddCollisionShape(new RectangleCollisionShape(PPosition, PSize));
            PPS.AddObject(Result);
            return Result;
        } 
        
        public static StaticObject MakeCircleStaticObject(Vector2 PPosition, float PRadius, Color PColor, Node? PParent, PhysicsResolver PPS) {
            StaticObject Result = new StaticObject(PPosition.X, PPosition.Y, Node2D.DrawShape.CIRCLE, PParent);
            Result.GiveCircleShape(new(PRadius, PColor));
            Result.AddCollisionShape(new CircleCollisionShape(PPosition, PRadius));
            PPS.AddObject(Result);
            return Result;

        }

        public static Player MakeRectangleKinematicObject(Vector2 PPosition, Vector2 PSize, Color PColor, Node? PParent, PhysicsResolver PPS) {
            Player Result = new Player(PPosition.X, PPosition.Y, Node2D.DrawShape.RECTANGLE, PParent);
            Result.GiveRectShape(new(PSize, PColor));
            Result.AddCollisionShape(new RectangleCollisionShape(PPosition, PSize));
            PPS.AddObject(Result);
            return Result;
        } 
        
        public static Player MakeCircleKinematicObject(Vector2 PPosition, float PRadius, Color PColor, Node? PParent, PhysicsResolver PPS) {
            Player Result = new Player(PPosition.X, PPosition.Y, Node2D.DrawShape.CIRCLE, PParent);
            Result.GiveCircleShape(new(PRadius, PColor));
            Result.AddCollisionShape(new CircleCollisionShape(PPosition, PRadius));
            PPS.AddObject(Result);
            return Result;

        } 
    }
}