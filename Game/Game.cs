using Engine;
using Engine.Physics;
using Engine.Rendering;

namespace Game;

public static class Game
{
    public static Player player { get; set; }


    public static void Start()
    {
        Vec2 size = new(Camera.active.xSize, Camera.active.xSize * (9f/16f));
        _ = new Object("Border")
            .WithModule(new RectRenderer(Color.white, Vec2.zero, size, .2f));

        Actor playerActor;
        player = new Object("Player")
            .WithActorAndCollider(playerActor = new(Vec2.zero, Vec2.zero), new(Vec2.zero, Vec2.one))
            .WithModule(new RectRenderer(Color.green, Vec2.zero, Vec2.one))
            .AddModule(new Player(playerActor));
    }
}