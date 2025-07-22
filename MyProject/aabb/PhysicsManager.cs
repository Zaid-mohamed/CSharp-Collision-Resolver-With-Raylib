


class PhysicsResolver {
    List<CollisionObject> Objs = [];


    public void ResolveCollision() {
        foreach (CollisionObject I in Objs) {
            foreach (CollisionObject J in Objs) {
                I.ResolveCollision(J);
            }
        }
    }

    public void AddObject(CollisionObject Obj) {
        Objs.Add(Obj);
    }
}