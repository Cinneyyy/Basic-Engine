using System;

namespace Engine;

public class Module : IDisposable
{
    public Object obj { get; private init; }


    public Module(Object obj)
        => this.obj = obj;

    ~Module()
        => Dispose();


    public virtual void Dispose()
        => GC.SuppressFinalize(this);
}