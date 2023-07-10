using System;
using System.Collections.Generic;

namespace Engine;

public sealed class Object
{
    public Vec2 globalPos
    {
        get => localPos + (parent?.globalPos ?? Vec2.zero);
        set => localPos = value - (parent?.globalPos ?? Vec2.zero);
    }
    public Vec2 localPos;
    public string name;
    public bool disableModuleTick
    {
        get => _disableModuleTick;
        set {
            _disableModuleTick = value;
            modules.ForEach(m => m.UpdateTickVirtuals());
        }
    }

    public Object parent { get; private set; }

    private readonly List<Module> modules = new();
    private readonly List<Object> children = new();
    private readonly uint uuid;
    private bool disposed, _disableModuleTick;

    private static uint nextUUID = 0;


    public Object()
        => uuid = nextUUID++;

    public Object(string name) : this()
        => this.name = name;

    public Object(string name, Vec2 pos, bool localPos = true) : this()
    {
        this.name = name;
        if(localPos) this.localPos = pos;
        else globalPos = pos;
    }

    public Object(Object parent, string name, Vec2 pos, bool localPos) : this(name, pos, localPos)
        => this.parent = parent;

    public Object(Vec2 pos, bool localPos = true) : this()
    {
        if(localPos) this.localPos = pos;
        else globalPos = pos;
    }

    public Object(Object parent) : this()
        => this.parent = parent;

    public Object(Object parent, Vec2 pos, bool localPos = true) : this()
    {
        this.parent = parent;

        if(localPos) this.localPos = pos;
        else globalPos = pos;
    }


    private void RemoveModuleWithoutDestroy(Module module)
    {
        if(modules.Contains(module))
            modules.Remove(module);

        module.obj = null;
    }


    public void SetParent(Object parent)
    {
        if(parent == null)
            return;

        this.parent?.RemoveChild(this);
        this.parent = parent;
        parent.children?.Add(this);
    }
    public void RemoveParent()
    {
        parent?.RemoveChild(this);
        parent = null;
    }
    public void FreeChildren()
    {
        foreach(var child in children)
            if(child != null)
                child.parent = null;
        children.Clear();
    }
    public void AddChild(Object child)
        => child?.SetParent(this);
    public void RemoveChild(Object child)
    {
        if(child != null)
            children?.Remove(child);
    }

    public Object WithModule(Module module)
    {
        AddModule(module);
        return this;
    }
    public Object WithModule<T>(T module) where T : Module
    {
        AddModule(module);
        return this;
    }
    public Module AddModule(Module module)
    {
        if(module.obj != null)
            throw new Exception($"Module of type {module.GetType().FullName} cannot be added to object {name} because the module is already attached to object {module.obj.name}");

        if(!modules.Contains(module))
            modules.Add(module);

        module.obj = this;
        Console.WriteLine("Added module of type " + module.GetType() + " to object with name " + name);
        return module;
    }
    public void RemoveModule(Module module)
    {
        RemoveModuleWithoutDestroy(module);
        module.Destroy();
    }
    public bool ContainsModule(Module module)
        => modules.Contains(module);
    public T AddModule<T>(T module) where T : Module
    {
        if(module.obj != null)
        {
           // throw new Exception($"Module of type {typeof(T).FullName} cannot be added to object {name} because the module is already attached to object {module.obj.name}");
            if(module.obj == this) return module;
            module.obj.RemoveModuleWithoutDestroy(module);
        }

        if(!modules.Contains(module))
            modules.Add(module);

        module.obj = this;
        return module;
    }

    public IReadOnlyList<Module> GetModules()
        => modules.AsReadOnly();

    public Module GetModule(int index) => modules[index];
    public Module GetModule(Index index) => modules[index];
    public T GetModule<T>(int index) where T : Module => modules[index] as T;
    public T GetModule<T>(Index index) where T : Module => modules[index] as T;
    public T GetModule<T>() where T : Module
    {
        var tType = typeof(T);
        return modules.Find(m => m.GetType() == tType) as T;
    }
    public Module GetModule(Type type) => modules.Find(m => m.GetType() == type);

    public void Destroy()
    {
        foreach(var m in modules)
            m.Destroy();
        foreach(var c in children)
            c.Destroy();

        modules.Clear();
        children.Clear();

        parent?.RemoveChild(this);

        disposed = true;
    }

    public override bool Equals(object obj)
        => (obj is Object) && (obj as Object) != null && (obj as Object).uuid == uuid;

    public override int GetHashCode()
        => uuid.GetHashCode();

    public Object Clone()
    {
        var ret = MemberwiseClone() as Object;

        ret.parent = null;
        parent.AddChild(ret);

        for(int i = 0; i < modules.Count; i++)
        {
            ret.modules[i] = modules[i].Clone();
            ret.modules[i].obj = this;
        }

        return ret;
    }


    public static bool operator ==(Object a, Object b)
    {
        if(a is null) return b is null || b.disposed;
        if(b is null) return a.disposed;

        return a.uuid == b.uuid;
    }
    public static bool operator !=(Object a, Object b)
        => !(a == b);
}