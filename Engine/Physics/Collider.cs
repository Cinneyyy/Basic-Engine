using Engine.Internal;
using System.Drawing;
using System;

namespace Engine.Physics;

public class Collider : Module
{
    public bool enabled
    {
        get => _enabled;
        set {
            if(_enabled = value)
                InternalGetters.enabledColliders.Add(this);
            else
                InternalGetters.enabledColliders.Remove(this);
        }
    }
    public bool isStatic;
    public bool isTrigger;
    public Vec2 size
    {
        get => _size;
        set {
            _size = value;
            colliderInfo.Size = new(value.x, value.y);
        }
    }
    public Vec2 offset
    {
        get => _offset;
        set {
            _offset = value;
            colliderInfo.Location = new(value.x, value.y);
        }
    }
    public Actor actor;

    private RectangleF colliderInfo = new(), rect = new();
    private Vec2 _size, _offset;
    public event EnterCollisionCallback onCollide = delegate { };
    public event EnterTriggerCallback onEnterTrigger = delegate { };

    private bool _enabled;


    public Collider(Actor actor, Vec2 offset, Vec2 size, bool enabled = true, bool isStatic = false, bool isTrigger = false) : base(actor.obj)
    {
        this.actor = actor;
        this.size = size;
        this.offset = offset;
        this.enabled = enabled;
        this.isStatic = isStatic;
        this.isTrigger = isTrigger;
    }

    public Collider(Object obj, Vec2 offset, Vec2 size, bool enabled = true, bool isStatic = true, bool isTrigger = false) : base(obj)
    {
        this.offset = offset;
        this.size = size;
        this.enabled = enabled;
        this.isStatic = isStatic;
        this.isTrigger = isTrigger;
    }

    public Collider(Vec2 offset, Vec2 size, bool enabled = true, bool isStatic = true, bool isTrigger = false) : base()
    {
        this.offset = offset;
        this.size = size;
        this.isStatic = isStatic;
        this.isTrigger = isTrigger;
        this.enabled = enabled;
    }


    public void PreSimulate()
        => rect = new((PointF)(colliderInfo.Location - colliderInfo.Size*.5f + obj.globalPos), (SizeF)(colliderInfo.Size /*+ Vec2.oneHalf*/));


    private static void StartSolve(Collider a, Collider b)
    {
        if(a.isStatic && b.isStatic) 
            throw new Exception("Can't solve conflict between 2 static colliders.");

        if(a.isStatic)
            SolveByMoveOne(b, a.rect); // Move only b
        else if(b.isStatic)
            SolveByMoveOne(a, b.rect); // Move only a
        else
            SolveByMoveTwo(a, b); // Move both a & b
    }

    private static void SolveByMoveOne(Collider move, RectangleF staticBox)
    {
        // The lower, the more efficient
        // The higher, the more accurate
        const int iterations = 16;
        Vec2 delta = move.obj.localPos - move.actor.prevPos;

        move.actor.vel = -3 * delta;
       // move.obj.localPos = move.actor.prevPos;
       // return;
        
        delta *= .5f;
        for(int i = 0; i < iterations; i++)
        {
            if(staticBox.IntersectsWith(move.rect))
                move.obj.localPos -= delta;
            else
                move.obj.localPos += delta;

            move.PreSimulate();
            delta *= .5f;
        }
    }

    private static void SolveByMoveTwo(Collider a, Collider b)
    {

    }


    public static void Simulate(Collider a, Collider b)
    {
        if(a.isStatic && b.isStatic)
            return;

        if(!a.rect.IntersectsWith(b.rect))
            return;

        if(a.isTrigger || b.isTrigger)
        {
            if(a.isTrigger) b.onEnterTrigger(a);
            if(b.isTrigger) a.onEnterTrigger(b);
        }
        else
        {
            a.onCollide(b);
            b.onCollide(a);
        }

        StartSolve(a, b);
    }
}