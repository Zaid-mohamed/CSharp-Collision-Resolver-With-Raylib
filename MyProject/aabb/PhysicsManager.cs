


class PhysicsResolver {
    List<CollisionObject> Objs = [];


    public void ResolveCollision() {
        foreach (CollisionObject i in Objs) {
            foreach (CollisionObject j in Objs) {
                i.ResolveCollision(j);
            }
        }
    }

    public void AddObject(CollisionObject Obj) {
        Objs.Add(Obj);
    }
}