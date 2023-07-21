namespace Engine;

public class Camera : Module
{
    public float xSize
    {
        get => _xSize;
        set {
            _xSize = value;
            if(isActive && Window.instance != null)
                Window.instance.unitsPerScreenWidth = value;
        }
    }
    public float ySize => xSize / Window.instance.ratio;
    public bool isActive { get; private set; }

    private float _xSize = 16;
    private ObjectController controller = null;
    private bool setupController, setupZoom;
    private (float minSize, float maxSize, float stepSize) zoomParams;

    public static Camera active
    {
        get => _active;
        set {
            if(active != null)
                active.isActive = false;
            _active = value;
            active.isActive = true;
            if(Window.instance != null)
                Window.instance.unitsPerScreenWidth = active.xSize;
        }
    }

    private static Camera _active;


    public Camera(Object obj, Vec2 pos, float size) : base(obj)
    {
        base.obj.globalPos = pos;
        this.xSize = size;
    }


    private void Scroll(int delta)
        => xSize = (xSize - (delta * (xSize.Map(0f, zoomParams.maxSize/2, 0f, zoomParams.minSize).Squared() + zoomParams.stepSize))).Clamp(zoomParams.minSize, zoomParams.maxSize);


    public Vec2 ScreenToWorld(Vec2i pos)
        => (Vec2)((Window.instance.center - pos) * new Vec2i(-1, 1)) / Window.instance.screenToWorldRatio + obj.globalPos;

    public Vec2i WorldToScreen(Vec2 pos)
        => (Vec2i)((obj.globalPos - pos) * Window.instance.screenToWorldRatio) * new Vec2i(-1, 1) + Window.instance.center;

    public void SetupMovement(float moveSpeed = 10f)
    {
        if(setupController)
        {
            CWriteErr("Tried to setup movement on a camera which already has it.");
            return;
        }

        controller = new ObjectController(obj, moveSpeed);
        setupController = true;
    }
    public void SetupMovement(DirectionKeys keys, float moveSpeed = 10f)
    {
        if(setupController)
        {
            CWriteErr("Tried to setup movement on a camera which already has it.");
            return;
        }

        controller = new ObjectController(obj, moveSpeed, keys);
        setupController = true;
    }
    public void SetupZoom(float minSize = 3f, float maxSize = 200f, float stepSize = .75f)
    {
        if(setupZoom)
        {
            CWriteErr("Tried to setup zoom on a camera which already has it.");
            return;
        }

        zoomParams = (minSize, maxSize, stepSize);
        Input.wheelScroll += Scroll;
        setupZoom = true;
    }

    public void ModifyMovement(float newMoveSpeed = float.NaN, DirectionKeys newKeys = null)
    {
        if(!setupController)
        {
            CWriteErr("Tried to modify camera movement which has not been setup yet.");
            return;
        }

        if(!float.IsNaN(newMoveSpeed)) controller.speed = newMoveSpeed;
        if(newKeys != null) controller.keys = newKeys;
    }
    public void ModifyZoom(float newMinSize = float.NaN, float newMaxSize = float.NaN, float newStepSize = float.NaN)
    {
        if(!setupZoom)
        {
            CWriteErr("Tried to modify camera zoom which has not been setup yet.");
            return;
        }

        if(!float.IsNaN(newMinSize)) zoomParams.minSize = newMinSize;
        if(!float.IsNaN(newMaxSize)) zoomParams.maxSize = newMaxSize;
        if(!float.IsNaN(newStepSize)) zoomParams.stepSize = newStepSize;
    }

    public void RemoveMovement()
    {
        if(!setupController)
        {
            CWriteErr("Tried to remove camera movement which has not been setup yet.");
            return;
        }

        setupController = false;
        controller = null;
    }
    public void RemoveZoom()
    {
        if(!setupZoom)
        {
            CWriteErr("Tried to remove camera zoom which has not been setup yet.");
            return;
        }

        setupZoom = false;
        Input.wheelScroll -= Scroll;
    }
}