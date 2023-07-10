using Engine.Internal;

namespace Engine;

public class ObjectController : Module
{
    public float speed;
    public DirectionKeys keys;


    public ObjectController(Object refObject, float speed) : this(refObject, speed, DirectionKeys.wasdKeys) { }

    public ObjectController(Object refObject, float speed, DirectionKeys keys) : base(refObject)
    {
        this.speed = speed;
        this.keys = keys;

        GameLoop.update += Update;
    }

    public ObjectController(float speed) : this(speed, DirectionKeys.wasdKeys) { }

    public ObjectController(float speed, DirectionKeys keys) : base()
    {
        this.speed = speed;
        keys = DirectionKeys.wasdKeys;

        GameLoop.update += Update;
    }


    private void Update(in float dt)
        => obj.globalPos += dt * speed * keys.move;
}