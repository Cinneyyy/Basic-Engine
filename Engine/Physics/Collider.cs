using System.Collections.Generic;
using System.Drawing;

namespace Engine.Physics;

public class Collider : Module
{
    public Vec2 size
    {
        get => _size;
        set {
            _size = value;
            transform.Size = new(value.x, value.y);
        }
    }
    public Vec2 offset
    {
        get => _offset;
        set {
            _offset = value;
            transform.Location = new(value.x, value.y);
        }
    }
    public Actor actor;
    public bool enabled
    {
        get => _enabled;
        set  {
            _enabled = value;

            if(_enabled)
            {
                actingColliders.Add(this);
                GameLoop.earlyUpdate += EarlyUpdate;
                GameLoop.update += Update;
            }
            else
            {
                actingColliders.Remove(this);
                GameLoop.earlyUpdate -= EarlyUpdate;
                GameLoop.update -= Update;
            }
        }
    }

    private RectangleF transform = new(), rect = new();
    private Vec2 _size, _offset;
    private bool _enabled;

    private static readonly List<Collider> actingColliders = new();


    public Collider(Object obj, Vec2 size, Actor actor = null) : base(obj)
    {
        this.size = size;
        this.offset = Vec2.zero;
        this.actor = actor;
    }

    public Collider(Object obj, Vec2 size, Vec2 offset, Actor actor = null) : base(obj)
    {
        this.size = size;
        this.offset = offset;
        this.actor = actor;
    }


    private void EarlyUpdate(float dt)
    {
        rect = transform;
        rect.Location += (SizeF)(actor?.obj?.localPos ?? Vec2.zero);
    }

    private void Update(float dt)
    {
        int colliderCount = actingColliders.Count;
        for(int i = 0; i < colliderCount; i++)
        {
            var other = actingColliders[i];
            if(!rect.IntersectsWith(other.rect))
                return;


        }
    }
}