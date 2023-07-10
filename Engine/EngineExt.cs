using Engine.Physics;

namespace Engine;

public static class EngineExt
{
    /// <summary>
    /// Adds the actor and collider to the object but also sets the collider's actor field and makes it not static
    /// </summary>
    public static Object WithActorAndCollider(this Object obj, Actor actor, Collider collider)
    {
        collider.actor = actor;
        collider.isStatic = false;
        return obj.WithModule(actor).WithModule(collider);
    }

    /// <summary>
    /// Identical to WithActorAndCollider but it returns a tuple containing the actor and collider instead of just the object the components were added to
    /// </summary>
    public static (Actor actor, Collider collider) AddActorAndCollider(this Object obj, Actor actor, Collider collider)
    {
        collider.actor = actor;
        collider.isStatic = false;
        obj.AddModule(actor);
        obj.AddModule(collider);
        return (actor, collider);
    }
}