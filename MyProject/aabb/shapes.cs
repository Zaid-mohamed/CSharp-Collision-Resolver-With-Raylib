using System.Numerics;
using Raylib_cs;

abstract class CollisionShape {
    public Vector2 Position;

    public CollisionShape(Vector2 p_position) {
        Position = p_position;
    }

    public abstract bool IntersectsWith(CollisionShape another);
    public abstract void DebugDraw();

}




class CircleCollisionShape : CollisionShape {

    public CircleCollisionShape(Vector2 p_position, float p_radius) : base(p_position){
        radius = p_radius;
    }

    public float radius;


    public override bool IntersectsWith(CollisionShape another)
    {
        if (another is CircleCollisionShape) {
            float Distance = Util.DistanceBetween(another.Position, this.Position);
            return Distance < this.radius + ((CircleCollisionShape)another).radius;
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
            &       Position.Y + Size.Y < another.Position.Y);
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
}

