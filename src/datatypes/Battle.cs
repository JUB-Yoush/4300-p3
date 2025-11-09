using System;
using Godot;

[GlobalClass]
public partial class Battle : Resource
{
    [Export]
    public string Prompt { get; set; }

    [Export]
    BattleDecision Choice1 { get; set; }

    [Export]
    BattleDecision Choice2 { get; set; }

    Battle() { }
}
