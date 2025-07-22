using System.Numerics;
using System.Security.Cryptography;
using Raylib_cs;



class CollisionObject(float PosX, float PosY, Node2D.DrawShape PShape, Node? PParent) : Node2D(PosX, PosY, PShape, PParent) {
    public CollisionShape CollShape = new CircleCollisionShape(new Vector2(0f), 0.0f);

    public void AddCollisionShape(CollisionShape PShape) {
        CollShape = PShape;
    }


    public virtual void ResolveCollision(CollisionObject Another){}
}



class StaticObject(float PosX, float PosY, Node2D.DrawShape PShape, Node? p_parent) : CollisionObject(PosX, PosY, PShape, p_parent) {}



class KinematicObject(float PosX, float PosY, Node2D.DrawShape PShape, Node? PParent) : CollisionObject(PosX, PosY, PShape, PParent) {

    public Vector2 Velocity;

    public override void ResolveCollision(CollisionObject Another)
    {
        switch (Another) {
            case StaticObject Stat:
                ResolveStaticObject(Stat);
                break;
            case KinematicObject Kine:
                ResolveKinematicObject(Kine);
                break;
        }
    }



    private void ResolveStaticObject(StaticObject Obj) {
        if (!CollShape.IntersectsWith(Obj.CollShape)) return;
        if (Obj.CollShape == this.CollShape) return;


        switch (Obj.CollShape)
        {
            case CircleCollisionShape CircShape:
                Vector2 Dis1 = CollShape.GetIntersectionDisplacement(CircShape);
                Position -= Dis1;
                Velocity = new Vector2(0f);
                break;
            case RectangleCollisionShape RectShape:
                Vector2 Dis2 = CollShape.GetIntersectionDisplacement(RectShape);
                Position += Dis2;
                Velocity = new Vector2(0f);
                break;

        }
    }

    private void ResolveKinematicObject(KinematicObject Obj) {
        if (!CollShape.IntersectsWith(Obj.CollShape)) return;
        if (Obj.CollShape == this.CollShape) return;




        switch (Obj.CollShape)
        {
            case CircleCollisionShape CircShape:
                Vector2 Dis1 = CollShape.GetIntersectionDisplacement(Obj.CollShape);
                Position -= Dis1;
                Velocity = new Vector2(0f);
                break;
            case RectangleCollisionShape RectShape:
                Vector2 Dis2 = CollShape.GetIntersectionDisplacement(Obj.CollShape);
                Position += Dis2;
                Velocity = new Vector2(0f);
                break;
        }
    }

    public void MoveAndCollide() {
        Position += Velocity;
        CollShape.Position = Position;
    }
}