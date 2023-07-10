namespace Engine.Physics;

public sealed class Actor : Module
{
    public Vec2 prevPos { get; private set; }
    public Vec2 vel;
    public Vec2 gravityScale = Vec2.one;
    public event ActorMoveCallback move = delegate { };


    public Actor(Object obj) : base(obj)
        => GamePhysics.RegisterActor(this);

    public Actor() : base()
        => GamePhysics.RegisterActor(this);

    public Actor(Vec2 vel) : this()
        => this.vel = vel;

    public Actor(Object obj, Vec2 vel) : base(obj)
        => this.vel = vel;

    public Actor(Vec2 vel, Vec2 gravityScale) : this(vel)
        => this.gravityScale = gravityScale;

    public Actor(Object obj, Vec2 vel, Vec2 gravityScale) : this(obj, vel)
        => this.gravityScale = gravityScale;
        

    ~Actor()
        => GamePhysics.UnregisterActor(this);


    public void Simulate(float deltaTime)
    {
        prevPos = obj.localPos;

        vel += deltaTime * gravityScale * GamePhysics.gravity;
        Vec2 move = vel * deltaTime;
        obj.localPos += move;

        this.move(move);
    }
}