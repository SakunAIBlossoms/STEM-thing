using Godot;
using System;
using System.Collections.Generic;

public partial class esc_back : Node2D
{
    private Control Interactables;

	static private Variant ShutUp;

    Dictionary<string, Dictionary<string, Variant>> InteractableList;

    public override void _Ready()
    {
        Interactables = GetNode("Interactables") as Control;
        InteractableList.Add("Buttons", new Dictionary<string, Variant> {});
        InteractableList.Add("Sliders", new Dictionary<string, Variant> {});
        InteractableList.Add("Checkboxes", new Dictionary<string, Variant> {});

        foreach (var obj in Interactables.GetChildren()) {
            string name = obj.Name;
            string[] NameParts = name.Split("|");
            if (NameParts[1] == "Button") {
                InteractableList["Buttons"].Add(NameParts[0], obj);
            }
            else if (NameParts[1] == "Slider") {
                InteractableList["Sliders"].Add(NameParts[0], obj);
            }
            else if (NameParts[1] == "Checkbox") {
                InteractableList["Checkboxes"].Add(NameParts[0], obj);
            }
        }
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent &&
            keyEvent.Pressed &&
            keyEvent.Keycode == Key.Escape)
        {
            GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
        }
    }
}
