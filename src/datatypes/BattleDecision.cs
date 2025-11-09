using System;
using Godot;

[GlobalClass]
public partial class BattleDecision : Resource
{
    [Export]
    public string Description { get; set; }

    [Export]
    public Decision Outcome1 { get; set; }

    [Export]
    public Decision Outcome2 { get; set; }

    [Export]
    public int ConditionIndex = -1;

    [Export]
    public Godot.Collections.Dictionary<Modifiers, int> Influence;

    public BattleDecision() { }
}
