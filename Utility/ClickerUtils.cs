using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Exo_Clicker.Utility
{
    public class ClickerUtils
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint message, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        private const int leftDown = 0x201;
        private const int leftUp = 0x202;

        private const int rightDown = 0x204;
        private const int rightUp = 0x205;

        private struct POINT
        {
            public int X;
            public int Y;
        }

        private Random random = new Random();

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null!;
        }

        public void LeftClicker(int minCPS, int maxCPS, IntPtr hWnd)
        {
            if (minCPS > maxCPS)
            {
                minCPS = maxCPS - 1;
            }

            int randomValue = random.Next(minCPS, maxCPS);
            float interval = 925f / randomValue;
            int value = (int)Math.Round(interval);

            PostMessage(hWnd, leftDown, 0, 0);
            PostMessage(hWnd, leftUp, 0, 0);

            System.Threading.Thread.Sleep(value);
        }

        public void RightClicker(int minCPS, int maxCPS, IntPtr hWnd)
        {
            if (minCPS > maxCPS)
            {
                minCPS = maxCPS - 1;
            }

            int randomValue = random.Next(minCPS, maxCPS);
            float interval = 925f / randomValue;
            int value = (int)Math.Round(interval);

            PostMessage(hWnd, rightDown, 0, 0);
            PostMessage(hWnd, rightUp, 0, 0);

            System.Threading.Thread.Sleep(value);
        }

        public void Jitter(int strength)
        {
            if (strength <= 0)
                strength = 1;

            Process[] processes = Process.GetProcessesByName("javaw");
            foreach (Process process in processes)
            {
                if (GetActiveWindowTitle() == process.MainWindowTitle)
                {
                    int randx = random.Next(1, strength);
                    int randy = random.Next(1, strength);

                    int randomPath = random.Next(1, 4);

                    POINT currentPosition;
                    GetCursorPos(out currentPosition);

                    int mX = currentPosition.X;
                    int mY = currentPosition.Y;

                    if (randomPath == 1)
                        SetCursorPos(mX - randx, mY - randy);
                    if (randomPath == 2)
                        SetCursorPos(mX + randx, mY - randy);
                    if (randomPath == 3)
                        SetCursorPos(mX - randx, mY + randy);
                    if (randomPath == 4)
                        SetCursorPos(mX + randx, mY + randy);
                }
            }
        }
    }
}