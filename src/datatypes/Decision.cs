using System;
using Godot;

[GlobalClass]
public partial class Decision : Resource
{
    [Export]
    public string Description { get; set; }

    [Export]
    public string Outcome { get; set; }

    [Export]
    public Godot.Collections.Dictionary<Modifiers, int> Influence;

    [Export]
    public Battle NextPrompt { get; set; }

    public Decision() { }
}
