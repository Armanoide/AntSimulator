using System;
using SFML.Graphics;
using SFML.Window;

namespace AntManager
{
    public static class Program
    {

        public static void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            var window = (RenderWindow)sender;
            window.Close();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            var windowColor = new Color(0, 0, 0);
            var mode = new VideoMode(800, 600);
            var app = new RenderWindow(mode, "Ant Simulator");
            var startUp = new StartUp(app) {Active = true};
            app.Closed += OnClose;


            while (app.IsOpen)
            {
                // Process events
                app.DispatchEvents();


                // Clear screen
                app.Clear(windowColor);
                app.Draw(startUp);

                // Update the window
                app.Display();
            }

        }
    }
}
