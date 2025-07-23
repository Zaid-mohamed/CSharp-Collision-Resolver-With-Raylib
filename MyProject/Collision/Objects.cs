using System.Numerics;
using System.Security.Cryptography;
using Raylib_cs;



/// <summary>
/// Base Class for Collision Objects,
/// Collision Objects uses CollisionShapes to resolve themselves from other Collision Objects.
/// </summary>
/// <param name="PosX"></param>
/// <param name="PosY"></param>
/// <param name="PShape"></param>
/// <param name="PParent"></param>
class CollisionObject(float PosX, float PosY, Node2D.DrawShape PShape, Node? PParent) : Node2D(PosX, PosY, PShape, PParent) {
    /// <summary>
    /// The CollisionShape of this CollisionObject
    /// </summary>
    public CollisionShape CollShape = new CircleCollisionShape(new Vector2(0f), 0.0f);

    /// <summary>
    /// Sets the CollShape member to he given PShape
    /// </summary>
    /// <param name="PShape"></param>
    public void AddCollisionShape(CollisionShape PShape) {
        CollShape = PShape;
    }

    /// <summary>
    /// Resolves the Collision between this CollisionObject and the give Another CollisionObject.
    /// </summary>
    /// <param name="Another"></param>
    public virtual void ResolveCollision(CollisionObject Another){}
}



/// <summary>
/// An CollisionObject that does not move or interact in any means with other CollisionObjects
/// </summary>
/// <param name="PosX"></param>
/// <param name="PosY"></param>
/// <param name="PShape"></param>
/// <param name="p_parent"></param>
class StaticObject(float PosX, float PosY, Node2D.DrawShape PShape, Node? p_parent) : CollisionObject(PosX, PosY, PShape, p_parent) {}


/// <summary>
/// An Object that its Velocity controlled by code, and Collides with all CollisionObjects without pushing them.
/// </summary>
/// <param name="PosX"></param>
/// <param name="PosY"></param>
/// <param name="PShape"></param>
/// <param name="PParent"></param>
class KinematicObject(float PosX, float PosY, Node2D.DrawShape PShape, Node? PParent) : CollisionObject(PosX, PosY, PShape, PParent) {

    /// <summary>
    /// The rate of the changing of this.Position
    /// </summary>
    public Vector2 Velocity;

    /// <summary>
    /// KinematicObjects does not push any CollisionObjects thus just moving by the full Intersection Displacement.
    /// </summary>
    /// <param name="Another"></param>
    public override void ResolveCollision(CollisionObject Another)
    {
        if (Another.CollShape == CollShape) return;
        if (!CollShape.IntersectsWith(Another.CollShape)) return;

        switch (Another.CollShape)
        {
            case CircleCollisionShape CircShape:
                Vector2 Dis1 = CollShape.GetIntersectionDisplacement(CircShape);
                Position -= Dis1;
                Velocity = new Vector2(0f);
                break;
            case RectangleCollisionShape RectShape:
                Vector2 Dis2 = CollShape.GetIntersectionDisplacement(RectShape);
                if (CollShape is CircleCollisionShape){
                    Position -= Dis2;
                }
                else if (CollShape is RectangleCollisionShape){
                    Position += Dis2;
                }
                Velocity = new Vector2(Dis2.X != 0f ? 0f : Velocity.X, Dis2.Y != 0f ? 0f : Velocity.Y);
                break;
        }
    }



    /// <summary>
    /// Changes the Position with the Velocity
    /// And Syncing the CollShape.Position with the new position.
    /// </summary>
    public void MoveAndCollide() {
        Position += Velocity;
        CollShape.Position = Position;
    }
}