using System.Collections.ObjectModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;


abstract class CollisionShape {
    public Vector2 Position;

    public CollisionShape(Vector2 p_position) {
        Position = p_position;
    }

    public abstract bool IntersectsWith(CollisionShape Another);
    public abstract Vector2 GetIntersectionDisplacement(CollisionShape Another);
    public abstract void DebugDraw();

}



class CircleCollisionShape : CollisionShape {

    public CircleCollisionShape(Vector2 p_position, float p_radius) : base(p_position){
        radius = p_radius;
    }

    public float radius;


    public override bool IntersectsWith(CollisionShape another)
    {
        if (another is CircleCollisionShape shape) {
            float Distance = Util.DistanceBetween(shape.Position, this.Position);
            return Distance < this.radius + shape.radius;
        }
        else if (another is RectangleCollisionShape Shape) {
            Vector2 NearestPoint = new(
                Util.Clamp(
                    Position.X,
                    Shape.Position.X,
                    Shape.Position.X + Shape.Size.X
                    ),
                Util.Clamp(
                    Position.Y,
                    Shape.Position.Y,
                    Shape.Position.Y + Shape.Size.Y
                    )

                
                );

            float Distance = Util.DistanceBetween(this.Position, NearestPoint);
            return this.radius > Distance;
        }
        else {
            return false;
        }
    }

    public override Vector2 GetIntersectionDisplacement(CollisionShape Another)
    {
        if (!this.IntersectsWith(Another)) return new Vector2(-1f, -1f);

        switch (Another) {
            case CircleCollisionShape Shape:
                float RadiiSum = this.radius + Shape.radius;
                float Distance = Util.DistanceBetween(this.Position, Shape.Position);
                return Util.GetDirectionBetween(this.Position, Shape.Position) * (RadiiSum - Distance);
            case RectangleCollisionShape Shape:
                Vector2 NearestPoint = new(
                Util.Clamp(
                    Position.X,
                    Shape.Position.X,
                    Shape.Position.X + Shape.Size.X
                    ),
                Util.Clamp(
                    Position.Y,
                    Shape.Position.Y,
                    Shape.Position.Y + Shape.Size.Y
                    )

                
                );

                float DistanceToNearest = Util.DistanceBetween(this.Position, NearestPoint);
                return Util.GetDirectionBetween(this.Position, NearestPoint) * (radius - DistanceToNearest);
        }

        return new Vector2(-1f, -1f);
    }

    public override void DebugDraw()
    {
        Raylib.DrawCircle((int)Position.X, (int)Position.Y, radius, new Color(0, 50, 255, 155));
        Raylib.DrawRing(Position, radius - 3.0f, radius, 0f, 360f, 30, new Color(0, 50, 255, 200));
    }

}


class RectangleCollisionShape(Vector2 p_position, Vector2 p_size) : CollisionShape(p_position) {
    public Vector2 Size = p_size;




    public override bool IntersectsWith(CollisionShape another)
        {
         if (another is CircleCollisionShape) {
             return another.IntersectsWith(this);
         }
         else if (another is RectangleCollisionShape RectShape) {
             return Position.X < another.Position.X + RectShape.Size.X
             &&      Position.X + Size.X > another.Position.X
             &&      Position.Y < another.Position.Y + RectShape.Size.Y
             &&      Position.Y + Size.Y > another.Position.Y;
         }
         else {
             return false;
         }
        }


    public override void DebugDraw()
    {
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, new Color(0, 50, 255, 150));
        Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, new Color(0, 50, 255, 200));

    }

    public override Vector2 GetIntersectionDisplacement(CollisionShape Another)
    {
        if (!this.IntersectsWith(Another)) return new Vector2(-1f, -1f);

        switch (Another) {
            case CircleCollisionShape Shape:
                return -Shape.GetIntersectionDisplacement(this);
            case RectangleCollisionShape Shape:
                // Vector2 DirToShape = Util.GetDirectionBetween(this.Position, Shape.Position);
                float XDiff = MathF.Max(0f, MathF.Min(Position.X + Size.X, Shape.Position.X + Shape.Size.X) - MathF.Max(Position.X, Shape.Position.X));
                float YDiff = MathF.Max(0f, MathF.Min(Position.Y + Size.Y, Shape.Position.Y + Shape.Size.Y) - MathF.Max(Position.Y, Shape.Position.Y));
                
                return new Vector2(XDiff < YDiff ? (Position.X > Shape.Position.X ? XDiff : -XDiff) : 0f,
                YDiff < XDiff ? (Position.Y > Shape.Position.Y ? YDiff : -YDiff ) : 0f);
        }

        return new Vector2(-1f, -1f);
    }
}



class Util {
    public static float DistanceBetween(Vector2 A, Vector2 B) =>
        MathF.Sqrt(MathF.Pow((B.X - A.X), 2f) + MathF.Pow((B.Y - A.Y), 2f));

    // Simple float clamp
    public static float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    public static Vector2 GetDirectionBetween(Vector2 A, Vector2 B) {
        return Util.Normalized(B - A);
    }

    public static float GetLength(Vector2 A) {
        return Util.DistanceBetween(new Vector2(0f, 0f), A);
    }
    public static Vector2 Normalized(Vector2 A) {
        return new Vector2(A.X / Util.GetLength(A), A.Y / Util.GetLength(A));
    }
}

