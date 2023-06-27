using System.Numerics;
using ImGuiNET;

namespace Exo_Clicker.Style
{
    public class Theme
    {
        private static Vector4 defaultBlack = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
        private static Vector4 defaultPurple = new Vector4(0.19339603f, 0.03854112f, 0.3184713f, 1f);
        private static Vector4 defaultPurpleLight = new Vector4(0.20788197f, 0.04730414f, 0.3375796f, 1f);
        private static Vector4 defaultPurpleDark = new Vector4(0.12941177f, 0.02745098f, 0.21176471f, 1);

        public static void SetTheme()
        {
            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 4f);
            ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 4f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowTitleAlign, 6f);

            ImGui.PushStyleColor(ImGuiCol.MenuBarBg, defaultBlack);
            ImGui.PushStyleColor(ImGuiCol.WindowBg, defaultBlack);
            ImGui.PushStyleColor(ImGuiCol.FrameBg, defaultPurpleDark);
            ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.FrameBgActive, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.Button, defaultPurpleDark);
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.Header, defaultPurpleDark);
            ImGui.PushStyleColor(ImGuiCol.HeaderHovered, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.HeaderActive, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.TextSelectedBg, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.TitleBg, defaultBlack);
            ImGui.PushStyleColor(ImGuiCol.TitleBgCollapsed, defaultBlack);
            ImGui.PushStyleColor(ImGuiCol.TitleBgActive, defaultBlack);
            ImGui.PushStyleColor(ImGuiCol.Tab, defaultPurpleDark);
            ImGui.PushStyleColor(ImGuiCol.TabHovered, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.TabActive, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.TabUnfocused, defaultPurpleDark);
            ImGui.PushStyleColor(ImGuiCol.TabUnfocusedActive, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.ResizeGrip, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.ResizeGripHovered, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.ResizeGripActive, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.CheckMark, new Vector4(1f, 1f, 1f, 1f));
            ImGui.PushStyleVar(ImGuiStyleVar.WindowTitleAlign, new Vector2(0.5f, 0.5f));
            ImGui.PushStyleColor(ImGuiCol.SliderGrab, new Vector4(1f, 1f, 1f, 1f));
            ImGui.PushStyleColor(ImGuiCol.SliderGrabActive, new Vector4(1f, 1f, 1f, 1f));
            ImGui.PushStyleColor(ImGuiCol.ScrollbarBg, defaultPurpleDark);
            ImGui.PushStyleColor(ImGuiCol.ScrollbarGrab, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.ScrollbarGrabActive, defaultPurple);
            ImGui.PushStyleColor(ImGuiCol.ScrollbarGrabHovered, defaultPurple);
        }
    }
}