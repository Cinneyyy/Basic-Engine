namespace Engine;

public abstract class Module
{
    public Object obj
    {
        get => _obj;
        set {
            _obj = value;
            UpdateTickVirtuals();
        }
    }
    public bool tick
    {
        get => _tick;
        set {
            _tick = value;
            UpdateTickVirtuals();
        }
    }
    
    private bool disposed, _tick = true;
    private readonly uint uuid;
    private Object _obj;

    private static uint nextUUID = 1u << 31; // The first bit is set to 1, which is not done in the object class. It is not currently impacting much, but at a later time, if I want to check if a UUID belongs to a module or an object, this will become useful


    public Module()
    {
        uuid = nextUUID++;
        UpdateTickVirtuals();
        Awake();
    }

    public Module(Object obj) : this()
        => obj.AddModule(this);


    internal void UpdateTickVirtuals()
    {
        Time.earlyUpdate -= EarlyTick;
        Time.update -= Tick;
        Time.lateUpdate -= LateTick;

        if(tick && !(obj?.disableModuleTick ?? true))
        {
            Time.earlyUpdate += EarlyTick;
            Time.update += Tick;
            Time.lateUpdate += LateTick;
        }
    }

    internal Module Clone()
        => MemberwiseClone() as Module;


    protected virtual void Awake() { }

    protected virtual void EarlyTick(in float dt) { }
    protected virtual void Tick(in float dt) { }
    protected virtual void LateTick(in float dt) { }


    public void Destroy()
    {
        if(obj?.ContainsModule(this) ?? false)
            obj!.RemoveModule(this);

        disposed = true;
    }

    public override bool Equals(object obj)
        => (obj is Module) && (obj as Module) != null && (obj as Module).uuid == uuid;

    public override int GetHashCode()
        => uuid.GetHashCode();


    public static bool operator ==(Module a, Module b)
    {
        if(a is null) return b is null || b.disposed;
        if(b is null) return a.disposed;

        return a.uuid == b.uuid;
    }
    public static bool operator !=(Module a, Module b)
        => !(a == b);
}