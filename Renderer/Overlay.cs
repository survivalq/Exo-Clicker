using System.Runtime.InteropServices;
using ClickableTransparentOverlay;
using ImGuiNET;
using System.Numerics;
using Exo_Clicker.Utility;
using Exo_Clicker.Style;

namespace Exo_Clicker.Renderer
{
    internal class Exo_ClickerOverlay : Overlay
    {
        private bool isWindowActive = true;
        private int leftClickerMin = 1;
        private int leftClickerMax = 1;
        private int rightClickerMin = 1;
        private int rightClickerMax = 1;
        private bool jitterEnabled = false;
        private bool isLeftEnabled = false;
        private bool isRightEnabled = false;
        private int jitterStrength = 1;
        private bool disableShift = false;

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int key);

        [DllImport("user32.dll")]
        static extern short GetKeyState(int key);

        ProcessUtils processUtils = new ProcessUtils();
        ClickerUtils clickerUtils = new ClickerUtils();

        private void LoadLeftClicker()
        {
            IntPtr hWnd = processUtils.FindProcesshWnd();
            new Thread(() =>
            {
                while (true)
                {
                    if (isLeftEnabled)
                    {
                        if (disableShift && !((GetAsyncKeyState(0x10) & 0x8000) > 0))
                        {
                            if ((GetAsyncKeyState(0x01) & 0x8000) > 0)
                            {
                                clickerUtils.LeftClicker(leftClickerMin, leftClickerMax, hWnd);

                                if (jitterEnabled)
                                    clickerUtils.Jitter(jitterStrength);
                            }
                        }
                        else if (!disableShift)
                        {
                            if ((GetAsyncKeyState(0x01) & 0x8000) > 0)
                            {
                                clickerUtils.LeftClicker(leftClickerMin, leftClickerMax, hWnd);

                                if (jitterEnabled)
                                    clickerUtils.Jitter(jitterStrength);
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
            }).Start();
        }

        private void LoadRightClicker()
        {
            IntPtr hWnd = processUtils.FindProcesshWnd();
            new Thread(() =>
            {
                while (true)
                {
                    if (isRightEnabled)
                    {
                        if (disableShift && !((GetAsyncKeyState(0x10) & 0x8000) > 0))
                        {
                            if ((GetAsyncKeyState(0x02) & 0x8000) > 0)
                            {
                                clickerUtils.RightClicker(rightClickerMin, rightClickerMax, hWnd);

                                if (jitterEnabled)
                                    clickerUtils.Jitter(jitterStrength);
                            }
                        }
                        else if (!disableShift)
                        {
                            if ((GetAsyncKeyState(0x02) & 0x8000) > 0)
                            {
                                clickerUtils.RightClicker(rightClickerMin, rightClickerMax, hWnd);

                                if (jitterEnabled)
                                    clickerUtils.Jitter(jitterStrength);
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
            }).Start();
        }

        // Enables the DPIAwareness
        public Exo_ClickerOverlay() : base(true) { }

        protected override Task PostInitialized()
        {
            this.VSync = true;

            LoadLeftClicker();
            LoadRightClicker();

            Theme.SetTheme();

            ReplaceFont(@"C:\Windows\Fonts\segoeui.ttf", 23, FontGlyphRangeType.English);

            return Task.CompletedTask;
        }

        protected override void Render()
        {
            ImGui.Begin("Exo Clicker", ref isWindowActive);

            ImGui.BeginTabBar("##tabs");
            if (ImGui.BeginTabItem("Left Clicker"))
            {
                ImGui.Checkbox("Enabled", ref isLeftEnabled);

                ImGui.Text("Min:");
                ImGui.SliderInt("##leftclickermin", ref leftClickerMin, 1, 24);

                ImGui.Text("Max:");
                ImGui.SliderInt("##leftclickermax", ref leftClickerMax, 1, 24);

                if (leftClickerMin > leftClickerMax)
                {
                    leftClickerMax = leftClickerMin;
                }

                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Right Clicker"))
            {
                ImGui.Checkbox("Enabled", ref isRightEnabled);

                ImGui.Text("Min:");
                ImGui.SliderInt("##rightclickermin", ref rightClickerMin, 1, 24);

                ImGui.Text("Max:");
                ImGui.SliderInt("##rightclickermax", ref rightClickerMax, 1, 24);

                if (rightClickerMin > rightClickerMax)
                {
                    rightClickerMax = rightClickerMin;
                }

                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Settings"))
            {
                ImGui.Checkbox("Jitter Enabled", ref jitterEnabled);

                ImGui.Text("Jitter Strength:");
                ImGui.SliderInt("##jitterstrength", ref jitterStrength, 1, 20);

                ImGui.Checkbox("Disable Shift", ref disableShift);

                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Logs"))
            {
                IntPtr hWnd = processUtils.FindProcesshWnd();
                if (hWnd == IntPtr.Zero)
                {
                    ImGui.TextColored(new Vector4(1f, 0f, 0f, 1f), "Couldn't connect to any client!\nYou must restart the application.");
                }
                else
                {
                    ImGui.TextColored(new Vector4(0f, 1f, 0f, 1f), $"Connected to a client successfully! (window handle: {hWnd})");
                }
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();

            if (isWindowActive == false)
            {
                this.Close();
            }
        }
    }
}