using Engine.Internal;

namespace Engine;

public static class Time
{
    public static float deltaTime => InternalGetters.deltaTime;
    public static float scaledDeltaTime => InternalGetters.scaledDeltaTime;
    public static float renderDeltaTime => InternalGetters.renderDeltaTime;
    public static float scaledRenderDeltaTime => InternalGetters.scaledRenderDeltaTime;

    public static float timeScale { get; set; }

    public static float fps => InternalGetters.modifiedFps;
    public static float fpsSampleTime => InternalGetters.fpsSampleTime;
    public static float fpsTargetSampleTime { get; set; } = 1f;
    public static float fpsSampleTimeError => InternalGetters.fpsSampleTimeError;
    
    public static event SafeGameLoopUpdateCallback update
    {
        add => GameLoop.update += value;
        remove => GameLoop.update -= value;
    }    
    public static event SafeGameLoopUpdateCallback earlyUpdate
    {
        add => GameLoop.earlyUpdate += value;
        remove => GameLoop.earlyUpdate -= value;
    }    
    public static event SafeGameLoopUpdateCallback lateUpdate
    {
        add => GameLoop.lateUpdate += value;
        remove => GameLoop.lateUpdate -= value;
    }
}