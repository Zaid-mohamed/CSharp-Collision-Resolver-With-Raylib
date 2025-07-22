using System.Numerics;
using Raylib_cs;


abstract class CollisionShape(Vector2 p_position)
{
    public Vector2 Position = p_position;

    protected Color DebugColor = new(0, 50, 255, 155);
    protected Color DebugColorOutline = new(0, 50, 255, 200);

    public abstract bool IntersectsWith(CollisionShape Another);
    public abstract Vector2 GetIntersectionDisplacement(CollisionShape Another);
    public abstract void DebugDraw();

}



class CircleCollisionShape(Vector2 PPosition, float PRadius) : CollisionShape(PPosition) {
    public float radius = PRadius;


    public override bool IntersectsWith(CollisionShape Another)
    {

        switch (Another) {
            case CircleCollisionShape Circle:
                float Distance = Util.DistanceBetween(Circle.Position, this.Position);
                return Distance < this.radius + Circle.radius;
            case RectangleCollisionShape Rect:
                Vector2 NearestPoint = new(
                Util.Clamp(
                    Position.X,
                    Rect.Position.X,
                    Rect.Position.X + Rect.Size.X
                    ),
                Util.Clamp(
                    Position.Y,
                    Rect.Position.Y,
                    Rect.Position.Y + Rect.Size.Y
                    )

                
                );

                // because case 1 has a variable of name Distance.
                float Distance2 = Util.DistanceBetween(this.Position, NearestPoint);
                return this.radius > Distance2;
            default:
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
            default:
                return new Vector2(-1f);
        }
    }

    public override void DebugDraw()
    {
        Raylib.DrawCircle((int)Position.X, (int)Position.Y, radius, DebugColor);
        Raylib.DrawRing(Position, radius - 3.0f, radius, 0f, 360f, 30, DebugColorOutline);
    }

}


class RectangleCollisionShape(Vector2 PPosition, Vector2 PSize) : CollisionShape(PPosition) {
    public Vector2 Size = PSize;



    public override bool IntersectsWith(CollisionShape Another) {

        return Another switch
        {
            CircleCollisionShape CirShape => CirShape.IntersectsWith(this),
            RectangleCollisionShape RectShape =>
                Position.X < Another.Position.X + RectShape.Size.X
                && Position.X + Size.X > Another.Position.X
                && Position.Y < Another.Position.Y + RectShape.Size.Y
                && Position.Y + Size.Y > Another.Position.Y,

            _ => false,

        };
    }



    public override void DebugDraw()
    {
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, DebugColor);
        Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, DebugColorOutline);

    }

    public override Vector2 GetIntersectionDisplacement(CollisionShape Another)
    {
        if (!this.IntersectsWith(Another)) return new Vector2(-1f, -1f);

        switch (Another) {
            case CircleCollisionShape Shape:
                return -Shape.GetIntersectionDisplacement(this);
            case RectangleCollisionShape Shape:
                float XDiff = MathF.Max(0f, MathF.Min(Position.X + Size.X, Shape.Position.X + Shape.Size.X) - MathF.Max(Position.X, Shape.Position.X));
                float YDiff = MathF.Max(0f, MathF.Min(Position.Y + Size.Y, Shape.Position.Y + Shape.Size.Y) - MathF.Max(Position.Y, Shape.Position.Y));
                
                return new Vector2(XDiff < YDiff ? (Position.X > Shape.Position.X ? XDiff : -XDiff) : 0f,
                YDiff < XDiff ? (Position.Y > Shape.Position.Y ? YDiff : -YDiff ) : 0f);
            default:
                return new Vector2(-1f);
        }

    }
}



class Util
{
    public static float DistanceBetween(Vector2 A, Vector2 B)
    {
        return MathF.Sqrt(MathF.Pow(B.X - A.X, 2f) + MathF.Pow(B.Y - A.Y, 2f));
    }
    
    public static float Clamp(float Value, float Min, float Max)
    {
        if (Min > Max) Min = Max;
        if (Max < Min) Max = Min;

        if (Value < Min) return Min;
        if (Value > Max) return Max;

        return Value;
    }

    public static Vector2 GetDirectionBetween(Vector2 A, Vector2 B)
    {
        return Util.Normalized(B - A);
    }

    public static float GetLength(Vector2 A)
    {
        return Util.DistanceBetween(Vector2.Zero, A);
    }
    public static Vector2 Normalized(Vector2 A)
    {
        return new Vector2(A.X / Util.GetLength(A), A.Y / Util.GetLength(A));
    }
}

