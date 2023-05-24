using System.Drawing;

namespace Engine.Physics;

public class StaticCollider : Module
{
    public Vec2 size
    {
        get => _size;
        set {
            _size = value;
            rect.Size = new(value.x, value.y);
        }
    }
    public Vec2 offset
    {
        get => _offset;
        set {
            _offset = value;
            rect.Location = new(value.x, value.y);
        }
    }
    public bool isStatic;
    public bool enabled;

    private RectangleF rect = new();
    private Vec2 _size, _offset;


    public StaticCollider(Object obj, Vec2 size) : base(obj)
    {
        this.size = size;
        this.offset = Vec2.zero;

        GameLoop.update += Update;
    }

    public StaticCollider(Object obj, Vec2 size, Vec2 offset) : base(obj)
    {
        this.size = size;
        this.offset = offset;

        GameLoop.update += Update;
    }


    private void Update(float dt)
    {
        if(!enabled)
            return;


    }
}