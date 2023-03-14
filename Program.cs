using ClickableTransparentOverlay;
using System.Threading;
using ImGuiNET;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Drawing;
using Pastel;
using System.Diagnostics;

namespace Exo_Clicker
{
    internal class SampleOverlay : Overlay
    {
        // Parameters for the clicker
        private int leftClickerMin = 1;
        private int leftClickerMax = 1;
        private int rightClickerMin = 1;
        private int rightClickerMax = 1;
        private bool jitterEnabled = false;
        private bool isLeftEnabled = false;
        private bool isRightEnabled = false;
        private int jitterStrength = 1;
        private bool disableShift = false;
        private bool stealthMode = false;
        private bool isOpenWindow = true;
        private Vector2 windowSize = new Vector2(445, 300);
        private Vector2 windowPos = new Vector2(0, 0);
        

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int key);

        utils.ProcessUtils processUtils = new utils.ProcessUtils();
        utils.ClickerUtils clickerUtils = new utils.ClickerUtils();

        private async Task LoadLeftClickerAsync()
        {
            IntPtr hWnd = processUtils.FindProcesshWnd();
            await Task.Run(() =>
            {
                while (true)
                {
                    if (isLeftEnabled)
                    {
                        if (disableShift && !((GetAsyncKeyState(16) & 0x8000) > 0))
                        {
                            if (Control.MouseButtons == MouseButtons.Left)
                            {
                                clickerUtils.leftClicker(leftClickerMin, leftClickerMax, hWnd);

                                if (jitterEnabled)
                                    clickerUtils.jitter(jitterStrength);
                            }
                        }
                        else if (!disableShift)
                        {
                            if (Control.MouseButtons == MouseButtons.Left)
                            {
                                clickerUtils.leftClicker(leftClickerMin, leftClickerMax, hWnd);

                                if (jitterEnabled)
                                    clickerUtils.jitter(jitterStrength);
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
            });
        }

        private async Task LoadRightClickerAsync()
        {
            IntPtr hWnd = processUtils.FindProcesshWnd();
            await Task.Run(() =>
            {
                while (true)
                {
                    if (isRightEnabled)
                    {
                        if (disableShift && !((GetAsyncKeyState(16) & 0x8000) > 0))
                        {
                            if (Control.MouseButtons == MouseButtons.Right)
                            {
                                clickerUtils.rightClicker(rightClickerMin, rightClickerMax, hWnd);

                                if (jitterEnabled)
                                    clickerUtils.jitter(jitterStrength);
                            }
                        }
                        else if (!disableShift)
                        {
                            if (Control.MouseButtons == MouseButtons.Right)
                            {
                                clickerUtils.rightClicker(rightClickerMin, rightClickerMax, hWnd);

                                if (jitterEnabled)
                                    clickerUtils.jitter(jitterStrength);
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
            });
        }

        protected override Task PostInitialized()
        {
            LoadLeftClickerAsync();
            LoadRightClickerAsync();
            this.VSync = true; // else runs at ~10k fps
            return Task.CompletedTask;
        }

        protected override void Render()
        {
            ImGui.Begin("Exo Clicker", ref isOpenWindow);
            ImGui.SetNextWindowPos(windowPos, ImGuiCond.Always);
            ImGui.SetNextWindowSize(windowSize, ImGuiCond.Always);
            ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 4f); // Set the border rounding
            ImGuiStylePtr style = ImGui.GetStyle();
            style.WindowRounding = 8.0f;

            // Set tab colors
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(63f / 255f, 18f / 255f, 81f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(91f / 255f, 24f / 255f, 126f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.TabActive] = new Vector4(127f / 255f, 40f / 255f, 178f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.TabUnfocused] = new Vector4(63f / 255f, 18f / 255f, 81f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(91f / 255f, 24f / 255f, 126f / 255f, 0.8f);

            // Set slider colors
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(1f, 1f, 1f, 1f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1f, 1f, 1f, 1f);

            // Set checkbox colors
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(1f, 1f, 1f, 1f);

            // Set colors for everything such as buttons and everything
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(63f / 255f, 18f / 255f, 81f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(91f / 255f, 24f / 255f, 126f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.TabActive] = new Vector4(127f / 255f, 40f / 255f, 178f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.TabUnfocused] = new Vector4(63f / 255f, 18f / 255f, 81f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(91f / 255f, 24f / 255f, 126f / 255f, 0.8f);

            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(63f / 255f, 18f / 255f, 81f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(91f / 255f, 24f / 255f, 126f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(127f / 255f, 40f / 255f, 178f / 255f, 0.8f);

            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(63f / 255f, 18f / 255f, 81f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(91f / 255f, 24f / 255f, 126f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(127f / 255f, 40f / 255f, 178f / 255f, 0.8f);

            style.Colors[(int)ImGuiCol.Button] = new Vector4(63f / 255f, 18f / 255f, 81f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(91f / 255f, 24f / 255f, 126f / 255f, 0.8f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(127f / 255f, 40f / 255f, 178f / 255f, 0.8f);


            // Set the overall color scheme
            style.Colors[(int)ImGuiCol.Border] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(72f, 0f, 67f, 0.15f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(72f, 0f, 67f, 0.1f);

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
                
                ImGui.BeginGroup();
                if (!ImGui.Checkbox("Stealth Mode", ref stealthMode))
                {
                    var processes = Process.GetProcessesByName("Taskmgr");
                    if (processes.Length > 0 & stealthMode == true)
                    {
                        Environment.Exit(0);
                    }
                }

                ImGui.EndGroup();

                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Debug"))
            {
                IntPtr hWnd = processUtils.FindProcesshWnd();
                if (hWnd == IntPtr.Zero)
                {
                    ImGui.TextColored(new Vector4(1f, 0f, 0f, 1f), "Couldn't connect to any client!\nYou must restart the application.");
                } else {
                    ImGui.TextColored(new Vector4(0f, 1f, 0f, 1f), $"Connected to client HWND ({hWnd})!");
                }
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();

            // Closes the program if the X button in the top-corner is clicked.
            if (isOpenWindow == false)
            {
                this.Close();
            }
        }
    }

    class Program
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        static async Task Main(string[] args)
        {   
            ShowWindow(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle, SW_HIDE);
            using var ExoOverlay = new SampleOverlay();
            await ExoOverlay.Run();
        }
    }
}