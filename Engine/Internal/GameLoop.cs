using System;
using System.Threading;
using Engine.Physics;

namespace Engine.Internal;

public class GameLoop
{
    public const int ThreadSleep = 1;

    public Thread thread { get; private init; }
    public bool run = true;

    private readonly Canvas canvas;

    public static float deltaTime => InternalGetters.deltaTime;

    public static event SafeGameLoopUpdateCallback earlyUpdate = delegate { }, update = delegate { }, lateUpdate = delegate { };
    public static event UnsafeGameLoopUpdateCallback earlyUpdateUnsafe = delegate { }, updateUnsafe = delegate { }, lateUpdateUnsafe = delegate { };


    public GameLoop(Canvas canvas)
    {
        this.canvas = canvas;
        thread = new(ThreadLoop);
        thread.SetApartmentState(ApartmentState.STA);
    }


    private unsafe void ThreadLoop()
    {
        DateTime lFrame = DateTime.Now, lSec = DateTime.Now;
        bool wasAlive = false;
        int loops = 0, loopsLastSec = 0;

        while(run && thread.IsAlive)
        {
            if(!canvas.Created)
                if(!wasAlive)
                {
                    while(!canvas.Created)
                        Thread.Sleep(1);

                    wasAlive = true;
                }

            InternalGetters.deltaTime = (float)(DateTime.Now - lFrame).TotalSeconds;
            lFrame = DateTime.Now;

            GamePhysics.Simulate(deltaTime);

            float dt = deltaTime;
            float* dtPtr = &dt;

            earlyUpdate(dt);
            earlyUpdateUnsafe(dtPtr);

            update(dt);
            updateUnsafe(dtPtr);

            lateUpdate(dt);
            lateUpdateUnsafe(dtPtr);

            var secDelta = (float)(DateTime.Now - lSec).TotalSeconds;
            if(secDelta >= Time.fpsTargetSampleTime)
            {
                InternalGetters.unmodifiedFps = loopsLastSec / Time.fpsTargetSampleTime;
                InternalGetters.fpsSampleTime = secDelta;
                InternalGetters.modifiedFps = loopsLastSec / Time.fpsSampleTime;
                InternalGetters.fpsSampleTimeError = Time.fpsSampleTime - Time.fpsTargetSampleTime;

                Console.WriteLine($"Executed {loopsLastSec} loops in the last {secDelta.Ts()} seconds.");
                lSec = DateTime.Now;
                loopsLastSec = 0;
            }

            loops++;
            loopsLastSec++;
            Thread.Sleep(ThreadSleep);
        }
    }


    public void Start()
        => thread.Start();
}