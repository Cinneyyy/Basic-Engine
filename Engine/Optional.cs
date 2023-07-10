namespace Engine;

public struct Optional<T>
{
    public T value;
    public bool enabled;


    public Optional(T value, bool enabled)
    {
        this.value = value;
        this.enabled = enabled;
    }

    public Optional(bool enabled)
    {
        this.value = default;
        this.enabled = enabled;
    }


    public bool TryGetValue(out T value)
    {
        value = this.value;
        return enabled;
    }


    public static implicit operator T(Optional<T> o) => o.value;
}