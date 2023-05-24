namespace Engine.Physics;

public class Actor : Module
{
    public Vec2 vel;
    public Vec2 gravityScale;
    public event ActorMoveCallback move;


    public Actor(Object obj) : base(obj)
        => GamePhysics.RegisterActor(this);

    ~Actor()
        => GamePhysics.UnregisterActor(this);


    public void Update(float deltaTime)
    {
        vel += gravityScale * GamePhysics.gravity * deltaTime;
        Vec2 move = vel * deltaTime;
        obj.localPos += move;
        this.move(move);
    }
}