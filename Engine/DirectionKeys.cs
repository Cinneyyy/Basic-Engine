using System;
using System.Windows.Forms;

namespace Engine;

public class DirectionKeys : IDisposable
{
    public Keys leftKey, rightKey, upKey, downKey;
    public bool left, right, up, down;
    public Vec2 move => new(left ? -1 : right ? 1 : 0, down ? -1 : up ? 1 : 0);

    public static readonly DirectionKeys wasdKeys = new(Keys.A, Keys.D, Keys.W, Keys.S);
    public static readonly DirectionKeys arrowKeys = new(Keys.Left, Keys.Right, Keys.Up, Keys.Down);


    public DirectionKeys(Keys left, Keys right, Keys up, Keys down)
    {
        leftKey = left;
        rightKey = right;
        upKey = up;
        downKey = down;

        Input.keyDown += GetKeyDown;
        Input.keyUp += GetKeyUp;
    }


    private void GetKeyDown(Keys key)
    {
        if(key == leftKey) left = true;
        else if(key == rightKey) right = true;
        else if(key == upKey) up = true;
        else if(key == downKey) down = true;
    }

    private void GetKeyUp(Keys key)
    {
        if(key == leftKey) left = false;
        else if(key == rightKey) right = false;
        else if(key == upKey) up = false;
        else if(key == downKey) down = false;
    }


    public void Dispose()
    {
        Input.keyDown -= GetKeyDown;
        Input.keyUp -= GetKeyUp;
        GC.SuppressFinalize(this);
    }
}