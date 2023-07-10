using Engine.Physics;
using System.Collections.Generic;

namespace Engine.Internal;

internal static class InternalGetters
{
    #region Fps / render time
    internal static float deltaTime
    {
        get => _deltaTime;
        set => scaledDeltaTime = (_deltaTime = value) * Time.timeScale;
    }
    internal static float renderDeltaTime
    {
        get => _renderDeltaTime;
        set => scaledRenderDeltaTime = (_renderDeltaTime = value) * Time.timeScale;
    }
    internal static float scaledDeltaTime { get; private set; }
    internal static float scaledRenderDeltaTime { get; private set; }
    internal static float unmodifiedFps { get; set; }
    internal static float fpsSampleTime { get; set; }
    internal static float modifiedFps { get; set; }
    internal static float fpsSampleTimeError { get; set; }

    private static float _deltaTime, _renderDeltaTime;
    #endregion

    #region Physics
    internal static readonly List<Collider> enabledColliders = new();
    #endregion

    #region Threading
    internal static GameLoop gameLoop { get; set; }
    internal static RenderLoop renderLoop { get; set; }
    #endregion
}