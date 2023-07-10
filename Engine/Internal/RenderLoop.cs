using System;
using System.Threading;
using System.Windows.Forms;

namespace Engine.Internal;

public class RenderLoop
{
    public const int ThreadSleep = 1;

    public Thread thread { get; private init; }
    public bool run = true;

    private readonly Canvas canvas;

    public static float deltaTime => InternalGetters.renderDeltaTime;

    public static event SafeGameLoopUpdateCallback draw = delegate { };


    public RenderLoop(Canvas canvas)
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

        while(run && thread.IsAlive)
        {
            if(!canvas.Created)
                if(!wasAlive)
                {
                    while(!canvas.Created)
                        Thread.Sleep(1);

                    wasAlive = true;
                }

            try
            {
                canvas.BeginInvoke((MethodInvoker)delegate
                {
                    canvas.Refresh();
                });
            }
            catch
            {
                Application.Quit(-1);
                break;
            }

            InternalGetters.renderDeltaTime = (float)(DateTime.Now - lFrame).TotalSeconds;
            lFrame = DateTime.Now;

            draw(deltaTime);

            var secDelta = (float)(DateTime.Now - lSec).TotalSeconds;
            if(secDelta >= 1f)
            {
                Console.WriteLine($"Executed {loopsLastSec} render loops in the last {secDelta.Ts()} seconds.");
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