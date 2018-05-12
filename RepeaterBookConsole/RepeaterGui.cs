using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Terminal.Gui;

namespace RepeaterBookConsole
{
    public class RepeaterGui
    {
        private Toplevel top;
        private MainFrame mainFrame = null;

        public RepeaterGui()
        {
            Application.Init();
        }

        public int Run()
        {
            top = Application.Top;
            mainFrame = new MainFrame(top);
            Application.Run(top);
            return -1;
        }
    }
}