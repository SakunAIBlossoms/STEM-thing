using Godot;
using Godot.NativeInterop;
using ImGuiNET;
using System;
using System.Collections.Generic;

public partial class esc_back : Node2D
{
    private Control Interactables;
    bool Fullscreen = false;

    public static bool IsOverlay = true;

    Dictionary<string, DdCheckbox> InteractableCheckboxes = [];
    Dictionary<string, Slider> InteractableSliders = [];
    public override void _Ready()
    {
        Fullscreen = GetWindow().Mode == Window.ModeEnum.ExclusiveFullscreen ? true : false;
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
        Fullscreen = !Fullscreen;
        var DisplayResolution = DisplayServer.ScreenGetSize(DisplayServer.GetPrimaryScreen());
        GetWindow().Size = new Vector2I(DisplayResolution.X / 2, DisplayResolution.Y / 2);
        GetWindow().MoveToCenter();
        GetWindow().Mode = Fullscreen ? Window.ModeEnum.Fullscreen : Window.ModeEnum.Windowed;
        InteractableSliders.TryGetValue("UIScale", out var slider);
        slider.Editable = !Fullscreen;
    }

    public void UIScaleValueChanged(float Value)
    {
        var DisplayResolution = DisplayServer.ScreenGetSize(DisplayServer.GetPrimaryScreen());
        if (!Fullscreen)
        {
            GetWindow().Size = new Vector2I((int)(DisplayResolution.X * Value), (int)(DisplayResolution.Y * Value));
            GetWindow().MoveToCenter();
        }
    }

    private void QuitOptions()
    {
        GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }
}
