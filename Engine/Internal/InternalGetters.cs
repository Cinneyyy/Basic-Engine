namespace Engine.Internal;

public static class InternalGetters
{
    public static float deltaTime
    {
        get => _deltaTime;
        set => scaledDeltaTime = (_deltaTime = value) * Time.timeScale;
    }
    public static float renderDeltaTime
    {
        get => _renderDeltaTime;
        set => scaledRenderDeltaTime = (_renderDeltaTime = value) * Time.timeScale;
    }
    public static float scaledDeltaTime { get; private set; }
    public static float scaledRenderDeltaTime { get; private set; }
    public static float unmodifiedFps { get; set; }
    public static float fpsSampleTime { get; set; }
    public static float modifiedFps { get; set; }
    public static float fpsSampleTimeError { get; set; }

    private static float _deltaTime, _renderDeltaTime;
}