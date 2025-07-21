

using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ObjectiveC;



class CollisionObject(float PosX, float PosY, Node2D.Shape p_shape, Node? p_parent) : Node2D(PosX, PosY, p_shape, p_parent) {
    public CollisionShape CollShape = new CircleCollisionShape(new Vector2(0f), 0.0f);

    public void AddShape(CollisionShape PShape) {
        CollShape = PShape;
    }


    public virtual void ResolveCollision(CollisionObject Another){}
}



class StaticObject(float PosX, float PosY, Node2D.Shape p_shape, Node? p_parent) : CollisionObject(PosX, PosY, p_shape, p_parent) {
    public override void ResolveCollision(CollisionObject Another)
    {
        
    }


}

class KinematicObject : CollisionObject {
    public KinematicObject(float PosX, float PosY, Shape p_shape, Node? p_parent) : base(PosX, PosY, p_shape, p_parent){}


    public Vector2 Velocity;

    public override void ResolveCollision(CollisionObject Another)
    {
        if (Another is StaticObject stat) {
            ResolveStaticObject(stat);
        }
        else if (Another is KinematicObject kine){
            ResolveKinematicObject(kine);
        }
    }


    public void MoveAndCollide() {
        position += Velocity;
        CollShape.Position = position;
    }

    private void ResolveStaticObject(StaticObject Obj) {
        if (!CollShape.IntersectsWith(Obj.CollShape)) return;
        if (Obj.CollShape == this.CollShape) return;


        switch (Obj.CollShape)
        {
            
            case CircleCollisionShape:
                position -= CollShape.GetIntersectionDisplacement(Obj.CollShape);
                Velocity = new Vector2(0f);
                break;
            case RectangleCollisionShape:
                // rad - dis_to_neearest
                Vector2 Dis = CollShape.GetIntersectionDisplacement(Obj.CollShape);
                position -= Dis;
                Velocity = new Vector2(0f);
                break;

        }
    }

    private void ResolveKinematicObject(KinematicObject Obj) {
        if (!CollShape.IntersectsWith(Obj.CollShape)) return;

        
        if (Obj.CollShape == this.CollShape) return;


        switch (Obj.CollShape)
        {
            case CircleCollisionShape:
                position -= CollShape.GetIntersectionDisplacement(Obj.CollShape);
                Velocity = new Vector2(0f);
                break;
            case RectangleCollisionShape:
                // rad - dis_to_neearest
                position -= CollShape.GetIntersectionDisplacement(Obj.CollShape);
                Velocity = new Vector2(0f);
                break;

        }
    }

}