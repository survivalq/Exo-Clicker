using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Exo_Clicker.utils
{
    public class ClickerUtils
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint message, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private const int leftDown = 0x201;
        private const int leftUp = 0x202;

        private const int rightDown = 0x204;
        private const int rightUp = 0x205;

        Random rnd = new Random();

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public void leftClicker(int minCPS, int maxCPS, IntPtr hWnd)
        {
            if (minCPS > maxCPS)
            {
                minCPS = maxCPS - 1;
            }

            int randomValue = rnd.Next(minCPS, maxCPS);
            float interval = 925 / randomValue;
            int value = (int)Math.Round(interval);

            PostMessage(hWnd, leftDown, 0, 0);
            PostMessage(hWnd, leftUp, 0, 0);

            Thread.Sleep(value);
        }
        public void rightClicker(int minCPS, int maxCPS, IntPtr hWnd)
        {
            if (minCPS > maxCPS)
            {
                minCPS = maxCPS - 1;
            }

            int randomValue = rnd.Next(minCPS, maxCPS);
            float interval = 925 / randomValue;
            int value = (int)Math.Round(interval);

            PostMessage(hWnd, rightDown, 0, 0);
            PostMessage(hWnd, rightUp, 0, 0);

            Thread.Sleep(value);
        }

        public void jitter(int strength)
        {
            if (strength <= 0)
                strength = 1;

            Process[] processes = Process.GetProcessesByName("javaw");
            foreach (Process process in processes)
            {
                if (GetActiveWindowTitle() == process.MainWindowTitle)
                {
                    int randx = rnd.Next(1, strength);
                    int randy = rnd.Next(1, strength);

                    int randomPath = rnd.Next(1, 4);

                    int mX = Control.MousePosition.X;
                    int mY = Control.MousePosition.Y;

                    if (randomPath == 1)
                        Cursor.Position = new Point(mX - randx, mY - randy);
                    if (randomPath == 2)
                        Cursor.Position = new Point(mX + randx, mY - randy);
                    if (randomPath == 3)
                        Cursor.Position = new Point(mX - randx, mY + randy);
                    if (randomPath == 4)
                        Cursor.Position = new Point(mX + randx, mY + randy);
                }
            }
        }
    }
}
