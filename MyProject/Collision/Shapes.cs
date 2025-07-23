using System.Numerics;
using Raylib_cs;


/// <summary>
/// Base class for all Collision Shapes
/// </summary>
/// <param name="PPosition"></param>
abstract class CollisionShape(Vector2 PPosition)
{
    /// <summary>
    /// Position of the Collision Shape
    /// In case of CircleCollisioShape it will be in the center of the circle
    /// But in the RectangleCollisionShape it will be in the top-left corner
    /// </summary>
    public Vector2 Position = PPosition;

    /// <summary>
    /// The Fill Color of the shape when debugging
    /// </summary>
    protected Color DebugColor = new(0, 50, 255, 155);
    /// <summary>
    /// the Outline Color of the shape while debugging
    /// </summary>
    protected Color DebugColorOutline = new(0, 50, 255, 200);

    /// <summary>
    /// returns true if this shape intersects with the given Another shape, otherwise returns false
    /// </summary>
    /// <param name="Another"></param>
    /// <returns>bool</returns>
    public abstract bool IntersectsWith(CollisionShape Another);

    /// <summary>
    /// Returns the Displacement needed to be reversed to resolve the collision between this CollisionShape and the
    /// given Another shape.
    /// </summary>
    /// <param name="Another"></param>
    /// <returns></returns>
    public abstract Vector2 GetIntersectionDisplacement(CollisionShape Another);
    /// <summary>
    /// Draws the shape for debugging.
    /// </summary>
    public abstract void DebugDraw();

}


/// <summary>
/// The Circle Collision Shape
/// </summary>
/// <param name="PPosition"></param>
/// <param name="PRadius"></param>
class CircleCollisionShape(Vector2 PPosition, float PRadius) : CollisionShape(PPosition) {
    /// <summary>
    /// Radius of the circle
    /// </summary>
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
        if (!IntersectsWith(Another)) return new Vector2(-1f, -1f);

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



/// <summary>
/// Rectangle Collision Shape
/// </summary>
/// <param name="PPosition"></param>
/// <param name="PSize"></param>
class RectangleCollisionShape(Vector2 PPosition, Vector2 PSize) : CollisionShape(PPosition) {
    /// <summary>
    /// Size of the Rectangle Shape,
    /// X: Width
    /// Y: Height
    /// </summary>
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

