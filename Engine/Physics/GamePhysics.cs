using Engine.Internal;
using System.Collections.Generic;

namespace Engine.Physics;

public static class GamePhysics
{
    public static Vec2 gravity { get; set; } = new(0, -5f);

    private static List<Actor> actors = new();
    private static List<Collider> colliders => InternalGetters.enabledColliders;


    public static void RegisterActor(Actor actor)
    {
        if(actor != null)
            actors.Add(actor);
    }

    public static void UnregisterActor(Actor actor)
    {
        if(actor != null)
            actors.Remove(actor);
    }

    public static void Simulate(float deltaTime)
    {
        foreach(var actor in actors)
            actor.Simulate(deltaTime);

        foreach(var collider in colliders)
            collider.PreSimulate();

        int collCount = colliders.Count;
        for(int x = 0; x < collCount-1; x++)
            for(int y = x+1; y < collCount; y++)
                Collider.Simulate(colliders[x], colliders[y]);
    }
}