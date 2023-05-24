using System;

namespace Engine;

public class ObjectController : IDisposable
{
    public Object refObject;
    public float speed;
    public DirectionKeys keys;


    public ObjectController(Object refObject, float speed)
    {
        this.refObject = refObject;
        this.speed = speed;
        keys = DirectionKeys.wasdKeys;

        GameLoop.update += Update;
    }

    public ObjectController(Object refObject, float speed, DirectionKeys keys)
    {
        this.refObject = refObject;
        this.speed = speed;
        this.keys = keys;

        GameLoop.update += Update;
    }


    private void Update(float dt)
        => refObject.globalPos += dt * speed * keys.move;


    public void Dispose()
    {
        GameLoop.update -= Update;
        keys.Dispose();
        GC.SuppressFinalize(this);
    }
}