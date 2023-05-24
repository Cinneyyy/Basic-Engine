using System;
using System.Threading;
using Engine.Internal;
using Engine.Physics;

namespace Engine;

public class GameLoop
{
    public const int ThreadSleep = 1;

    private Thread thread;
    private Canvas canvas;

    public static float deltaTime => InternalGetters.deltaTime;

    public static event GameLoopUpdateCallback earlyUpdate = delegate { };
    public static event GameLoopUpdateCallback update = delegate { };
    public static event GameLoopUpdateCallback lateUpdate = delegate { };


    public GameLoop(Canvas canvas)
    {
        this.canvas = canvas;
        thread = new(ThreadLoop);
        thread.SetApartmentState(ApartmentState.STA);
    }


    private void ThreadLoop()
    {
        DateTime lFrame = DateTime.Now, lSec = DateTime.Now;
        bool wasAlive = false;
        int loops = 0, loopsLastSec = 0;

        while(thread.IsAlive)
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

            GamePhysics.Update(deltaTime);

            earlyUpdate(deltaTime);
            update(deltaTime);
            lateUpdate(deltaTime);

            var secDelta = (float)(DateTime.Now - lSec).TotalSeconds;
            if(secDelta >= Time.fpsTargetSampleTime)
            {
                InternalGetters.unmodifiedFps = loopsLastSec / Time.fpsTargetSampleTime;
                InternalGetters.fpsSampleTime = secDelta;
                InternalGetters.modifiedFps = loopsLastSec / Time.fpsSampleTime;
                InternalGetters.fpsSampleTimeError = Time.fpsSampleTime - Time.fpsTargetSampleTime;

               // Console.WriteLine($"Executed {loopsLastSec} loops in the last {secDelta.Ts()} seconds.");
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