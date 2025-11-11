using System;
using Godot;

[GlobalClass]
public partial class Prompt : Resource, INode
{
    [Export]
    public string PromptText { get; set; }

    [Export]
    public string Answer1ConditionKey = null;

    [Export]
    public string Answer2ConditionKey = null;

    [Export]
    public Decision Answer1 { get; set; }

    [Export]
    public Decision Answer2 { get; set; }

    Prompt() { }
}
