using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Exo_Clicker.utils
{
    public class ProcessUtils
    {
        private IntPtr hWnd;

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lClass, string wName);
        public IntPtr FindProcesshWnd() // Fetches the programs HWND, knows which client to target.
        {
            Process[] processes = Process.GetProcessesByName("javaw");
            foreach (Process process in processes)
            {
                hWnd = FindWindow(null, process.MainWindowTitle);
            }
            return hWnd;
        }
    }
}
