

using System.Numerics;
using System.Runtime.InteropServices.ObjectiveC;



class CollisionObject : Node2D {
     public CollisionObject(float PosX, float PosY, Shape p_shape, Node p_parent) : base(PosX, PosY, p_shape, p_parent){

    }

    public CollisionShape CollShape;

    public void AddShapeNoDel(CollisionShape PShape) {
        if (CollShape != null) return;

        CollShape = PShape;
    }


    public virtual void ResolveCollision(CollisionObject Another){}
}



class StaticObject : CollisionObject {
    public StaticObject(float PosX, float PosY, Shape p_shape, Node p_parent) : base(PosX, PosY, p_shape, p_parent){}
}

class KinematicObject : CollisionObject {
    public KinematicObject(float PosX, float PosY, Shape p_shape, Node p_parent) : base(PosX, PosY, p_shape, p_parent){}


    Vector2 Velocity;

    public override void ResolveCollision(CollisionObject Another)
    {
        if (Another is StaticObject obj) {
            ResolveStaticObject(obj);
        }
        else {
        }
    }


    private void ResolveStaticObject(StaticObject Obj) {
        if (!CollShape.IntersectsWith(Obj.CollShape)) return;
        if (Obj.CollShape == this.CollShape) return;

        position -= CollShape.GetIntersectionDisplacement(Obj.CollShape);
    }


}