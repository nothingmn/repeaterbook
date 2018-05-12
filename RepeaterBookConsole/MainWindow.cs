using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace RepeaterBookConsole
{
    public class MainWindow
    {
        private Window win;

        public MainWindow(FrameView parentFrame)
        {
            // Creates the top-level window to show
            win = new Window(new Rect(0, 0, parentFrame.Frame.Width - 2, parentFrame.Frame.Height - 2), "Repeater Book Data Management Tool");
            parentFrame.Add(win);
        }
    }
}