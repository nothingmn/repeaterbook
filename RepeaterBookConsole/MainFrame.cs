using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Terminal.Gui;

namespace RepeaterBookConsole
{
    public class MainFrame
    {
        private readonly Toplevel _top;
        private MainWindow mainWindow = null;
        private MainMenu mainMenu = null;
        private Label statusLabel;

        public MainFrame(Toplevel top)
        {
            _top = top;
            MainWindowFrame = new FrameView(new Rect(0, 0, top.Frame.Width, top.Frame.Height - StatusBarHeight), "Main");

            StatusBarFrame = new FrameView(new Rect(0, top.Frame.Height - StatusBarHeight, top.Frame.Width, StatusBarHeight), "Status");

            top.Add(MainWindowFrame, StatusBarFrame);

            mainWindow = new MainWindow(MainWindowFrame);
            mainMenu = new MainMenu(this);
            mainMenu.OnMenuItemSelected += MainMenu_OnMenuItemSelected;

            statusLabel = new Label(0, 0, "Ready");
            StatusBarFrame.Add(statusLabel);
        }

        private void MainMenu_OnMenuItemSelected(string command)
        {
            if (command == "Quit")
            {
                _top.Running = false;
            }
        }

        public int StatusBarHeight { get; set; } = 3;
        public FrameView MainWindowFrame { get; set; }
        public FrameView StatusBarFrame { get; set; }
    }
}