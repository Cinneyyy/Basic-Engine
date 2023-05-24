using System;

namespace Engine;

public class ObjectConstraint
{
    private Action applyFunc;


    public ObjectConstraint(Action applyFunc)
    {
        this.applyFunc = applyFunc;
        GameLoop.update += Update;
    }


    private void Update(float dt)
        => applyFunc();
}