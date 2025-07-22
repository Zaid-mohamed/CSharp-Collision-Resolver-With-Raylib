


/// <summary>
/// The class responsible for calling the ResolveCollision() method inside all CollisionObjects he has access to.
/// </summary>
class PhysicsResolver {

    /// <summary>
    /// All Collision Objects This Resolver Will Resolve Their Collisions
    /// </summary>
    List<CollisionObject> Objs = [];


    /// <summary>
    /// Iterates over each object in [`Obj`] and calls the ResolveCollision Method to resolve him with all other Objs
    /// Note: there is no need to check if it is resolving with self because the method already has this check.
    /// </summary>
    public void ResolveCollision() {
        foreach (CollisionObject i in Objs) {
            foreach (CollisionObject j in Objs) {
                i.ResolveCollision(j);
            }
        }
    }
    /// <summary>
    /// Adds An Object to the Objs List if not already there.
    /// </summary>
    /// <param name="Obj"></param>
    public void AddObject(CollisionObject Obj) {
        if (Objs.Contains(Obj)) return;

        Objs.Add(Obj);
    }
}