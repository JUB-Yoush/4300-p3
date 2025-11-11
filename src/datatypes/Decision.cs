using System;
using System.Dynamic;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Decision : Resource, INode
{
    [Export]
    public string Description { get; set; }

    [Export]
    public string Outcome { get; set; }

    [Export]
    public Decision ConditionalOutcome1 { get; set; }

    [Export]
    public Decision ConditionalOutcome2 { get; set; }

    [Export]
    public Dictionary<Modifiers, int> Influence;

    [Export]
    public string ConditionKey = null;

    [Export]
    public int ConditionIndex = -1;

    [Export]
    public Prompt NextPrompt = null;

    [Export]
    public Prompt NextPromptKing = null;

    [Export]
    public Prompt NextPromptBarb = null;

    [Export]
    public Decision NextDecision = null;

    [Export]
    public HeroType setHero = HeroType.NONE;

    [Export]
    public GameState newState = GameState.PLAYING;

    public Decision() { }
}
