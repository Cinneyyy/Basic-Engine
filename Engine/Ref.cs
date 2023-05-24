namespace Engine;

public sealed class Ref<T>
{
    public T value;


    public Ref(T value) => this.value = value;


    public T GetValue() => value;
    public void GetValue(out T value) => value = GetValue();

    public void SetValue(T value) => this.value = value;


    public static explicit operator T(Ref<T> refT) => refT.GetValue();
}