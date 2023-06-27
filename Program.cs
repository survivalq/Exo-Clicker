using System.Runtime.InteropServices;
using Exo_Clicker.Renderer;

namespace Exo_Clicker
{
    class Program
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_HIDE = 0;
        static async Task Main(string[] args)
        {   
            ShowWindow(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle, SW_HIDE);
            using var ExoOverlay = new Exo_ClickerOverlay();
            await ExoOverlay.Run();
            Environment.Exit(0);
        }
    }
}