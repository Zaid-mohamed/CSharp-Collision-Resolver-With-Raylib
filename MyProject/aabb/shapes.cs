using System.Collections.ObjectModel;
using System.Numerics;
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
        else if (another is RectangleCollisionShape) {
            Vector2 NearestPoint = new(
                Util.Clamp(
                    (((RectangleCollisionShape)another)).Position.X,
                    another.Position.X,
                    another.Position.X + ((RectangleCollisionShape)another).Size.X
                    ),
                Util.Clamp(
                    ((RectangleCollisionShape)another).Position.Y,
                    another.Position.Y,
                    another.Position.Y + ((RectangleCollisionShape)another).Size.Y
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

        if (Another is CircleCollisionShape shape) {
            // radii - distance
            float RadiiSum = this.radius + shape.radius;
            float Distance = Util.DistanceBetween(this.Position, shape.Position);
            return Util.GetDirectionBetween(this.Position, shape.Position) * (RadiiSum - Distance);
        }
        //TODO: add handling for remaining shapes, aka RectangleCollisionShape;
        return new Vector2(-1f, -1f);
    }

    public override void DebugDraw()
    {
        Raylib.DrawCircle((int)Position.X, (int)Position.Y, radius, new Color(0, 50, 255, 155));
        Raylib.DrawRing(Position, radius - 3.0f, radius, 0f, 360f, 30, new Color(0, 50, 255, 200));
    }

}


class RectangleCollisionShape : CollisionShape {
    public RectangleCollisionShape(Vector2 p_position, Vector2 p_size) : base(p_position) {
        Size = p_size;
    }

    public Vector2 Size;




public override bool IntersectsWith(CollisionShape another)
    {
        if (another is CircleCollisionShape) {
            return another.IntersectsWith(this);
        }
        else if (another is RectangleCollisionShape) {
            return (Position.X > another.Position.X + ((RectangleCollisionShape)another).Size.X
            &&      Position.X + Size.X < another.Position.X
            &&      Position.Y > another.Position.Y + ((RectangleCollisionShape)another).Size.Y
            &&      Position.Y + Size.Y < another.Position.Y);
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

