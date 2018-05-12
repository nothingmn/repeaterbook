using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace RepeaterBookConsole
{
    public class MainMenu
    {
        public delegate void MenuItemSelected(string command);

        public event MenuItemSelected OnMenuItemSelected;

        private readonly MainFrame _parent;

        public MainMenu(MainFrame parent)
        {
            _parent = parent;

            // Creates a menubar, the item "New" has a help menu.
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_New", "Creates a new export", ()=> { OnMenuItemSelected?.Invoke("New");} ),
                    new MenuItem ("_Export", "Export based on current configuration", ()=> { OnMenuItemSelected?.Invoke("Export");} ),
                    new MenuItem ("_Quit", "", ()=> { OnMenuItemSelected?.Invoke("Quit");} )
                }),
            });

            _parent.MainWindowFrame.Add(menu);
        }
    }
}