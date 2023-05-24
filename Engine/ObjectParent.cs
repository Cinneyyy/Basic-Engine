using System.Collections.Generic;
using System.Linq;

namespace Engine;

public class ObjectParent
{
    public readonly List<Object> children = new();


    public ObjectParent(List<Object> children) 
        => this.children = children;

    public ObjectParent(params Object[] children) : this(children.ToList()) { }
}