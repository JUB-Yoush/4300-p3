using System;
using Godot;

[GlobalClass]
public partial class Preperation : Resource
{
    [Export]
    public string Prompt { get; set; }

    [Export]
    public Decision Answer1 { get; set; }

    [Export]
    public Decision Answer2 { get; set; }

    Preperation() { }
}
