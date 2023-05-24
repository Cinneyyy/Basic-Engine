using System.Windows.Forms;

namespace Engine.Internal;

public class Canvas : Form
{
    public Canvas()
        => DoubleBuffered = true;
}