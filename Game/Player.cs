using Engine;
using Engine.Physics;
using System.Windows.Forms;

namespace Game;

public class Player : Module
{
    private bool kLeft, kRight, kUp, kDown;
    private bool oLeft, oRight, oUp, oDown;
    private Actor actor;


    public Player(Actor actor) : base()
    {
        this.actor = actor;

        Input.keyDown += key => {
            switch(key)
            {
                case Keys.A: kLeft = true; oRight = kRight; kRight = false; break;
                case Keys.D: kRight = true; oLeft = kLeft; kLeft = false; break;
                case Keys.W: kUp = true; oDown = kDown; kDown = false; break;
                case Keys.S: kDown = true; oUp = kUp; kUp = false; break;
            }
        };
        Input.keyUp += key => {
            switch(key)
            {
                case Keys.A: kLeft = oLeft = false; kRight = oRight; break;
                case Keys.D: kRight = oRight = false; kLeft = oLeft; break;
                case Keys.W: kUp = oUp = false; kDown = oDown; break;
                case Keys.S: kDown = oDown = false; kUp = oUp; break;
            }
        };
    }


    protected override void Tick(in float dt)
    {
        actor.vel = new Vec2(kLeft ? -1 : kRight ? 1 : 0, kDown ? -1 : kUp ? 1 : 0).normalized * 2.5f;
    }
}