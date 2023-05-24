using System.Collections.Generic;

namespace Engine.Physics;

public static class GamePhysics
{
    public static Vec2 gravity { get; set; } = new(0, -5f);

    private static List<Actor> actors = new();


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

    public static void Update(float deltaTime)
    {
        int actorCount = actors.Count;
        for(int i = 0; i < actorCount; i++)
            actors[i].Update(deltaTime);
    }
}