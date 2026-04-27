using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;

public partial class esc_back : Node2D
{
    private Control Interactables;

    public static bool IsOverlay = false;

    Dictionary<string, Dictionary<string, Variant>> InteractableList = [];

    public override void _Ready()
    {
        // We add every button to a dictionary that we use later
        Interactables = GetNode("Interactables") as Control;
        InteractableList.Add("Sliders", []);
        InteractableList.Add("Checkboxes", []);

        foreach (var obj in Interactables.GetChildren())
        {
            string name = obj.Name;
            string[] NameParts = name.Split("|");
            if (NameParts[1] == "Slider")
            {
                InteractableList["Sliders"].Add(NameParts[0], obj);
                GD.PrintRich("[color=gold]Successfully added slider " + NameParts[0] + " to the slider dictionary.");
            }
            else if (NameParts[1] == "Check")
            {
                InteractableList["Checkboxes"].Add(NameParts[0], obj);
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

    private void QuitOptions()
    {
        // b is the checkbox in the dictionary
        foreach (var b in InteractableList["Checkboxes"])
        {
            GD.Print(b.Key);
        }
        GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }
}
