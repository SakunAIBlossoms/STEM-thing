using Godot;
using Godot.NativeInterop;
using ImGuiNET;
using System;
using System.Collections.Generic;

public partial class esc_back : Node2D
{
    private Control Interactables;
    bool Fullscreen = true;

    public static bool IsOverlay = false;

    private double scale = 0.0;

    Dictionary<string, DdCheckbox> InteractableCheckboxes = [];
    Dictionary<string, Slider> InteractableSliders = [];
    public override void _Ready()
    {
        Fullscreen = GetWindow().Mode == Window.ModeEnum.Fullscreen ? true : false;
        // We add every button to a dictionary that we use later
        Interactables = GetNode("Interactables") as Control;

        foreach (var obj in Interactables.GetChildren())
        {
            string name = obj.Name;
            string[] NameParts = name.Split("|");
            if (NameParts[1] == "Slider")
            {
                InteractableSliders.Add(NameParts[0], obj as Slider);
                GD.PrintRich("[color=gold]Successfully added slider " + NameParts[0] + " to the slider dictionary.");
            }
            else if (NameParts[1] == "Check")
            {
                InteractableCheckboxes.Add(NameParts[0], obj as DdCheckbox);
                GD.PrintRich("[color=gold]Successfully added checkbox " + NameParts[0] + " to the checkbox dictionary.");
            }
        }
        if (IsOverlay)
        {
            GetNode<Panel>("Panel").Visible = false;
            GetNode<ColorRect>("Water").Visible = false;
            GetNode<BackBufferCopy>("PS1Overlay").Visible = false;
        }
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent &&
            keyEvent.Pressed &&
            keyEvent.Keycode == Key.Escape)
        {
            QuitOptions();
        }
    }

    public void FullscreenToggled(bool Toggle)
    {
        InteractableSliders.TryGetValue("UIScale", out var slider);
        Fullscreen = !Fullscreen;
        var DisplayResolution = DisplayServer.ScreenGetSize(DisplayServer.GetPrimaryScreen());
        if (Fullscreen)
        {
           GetWindow().Size = new Vector2I((int)(DisplayResolution.X * slider.Value), (int)(DisplayResolution.Y * slider.Value));
           GetWindow().MoveToCenter(); 
        }
        GetWindow().Mode = Fullscreen ? Window.ModeEnum.Windowed : Window.ModeEnum.Fullscreen;
        slider.Editable = Fullscreen;
    }

    public void OnUIScaleDragEnded(bool Changed)
    {
        InteractableSliders.TryGetValue("UIScale", out var slider);
        var DisplayResolution = DisplayServer.ScreenGetSize(DisplayServer.GetPrimaryScreen());
        if (Fullscreen && Changed)
        {
            GetWindow().Size = new Vector2I((int)(DisplayResolution.X * slider.Value), (int)(DisplayResolution.Y * slider.Value));
            GetWindow().MoveToCenter();
        }
    }

    public void MouseParticlesCheckToggled(bool Value)
    {
        var s = GetNode("/root/MouseCursor/").GetScript();
    }

    private void QuitOptions()
    {
        GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }
}
