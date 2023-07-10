using System;
using Engine.Internal;

namespace Engine;

public class ObjectConstraint
{
    private Action applyFunc;


    public ObjectConstraint(Action applyFunc)
    {
        this.applyFunc = applyFunc;
        GameLoop.update += Update;
    }


    private void Update(in float dt)
        => applyFunc();
}