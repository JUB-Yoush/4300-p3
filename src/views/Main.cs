using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.IO;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// public class Preparation
// {
//     public string Prep1Intro { get; set; }
//     public Decision Prep1Dec1 { get; set; }
//     public Decision Prep1Dec2 { get; set; }
//     public Decision Prep1Dec3 { get; set; }
//     public Decision Prep1Dec4 { get; set; }
//     public Outro Prep1Outro { get; set; }
// }

public enum Modifiers
{
    SWORDSMEN,
    SHIELDMEN,
    ARCHERS,
    HORSE,
    STRENGTH,
    WEAKNESS,
}

[GlobalClass]
public partial class Preperation : Resource
{
    [Export]
    public string Prompt { get; set; }

    [Export]
    public Decision Answer1 { get; set; }

    [Export]
    public Decision Answer2 { get; set; }
}

[GlobalClass]
public partial class Decision : Resource
{
    [Export]
    public string Description { get; set; }

    [Export]
    public string Outcome { get; set; }

    [Export]
    public Dictionary<Modifiers, int> Influence { get; set; }

    [Export]
    public Preperation NextPrompt { get; set; }
}

[GlobalClass]
public partial class Battle : Resource
{
    public string Prompt { get; set; }
}

public partial class Main : Control
{
    public override void _Ready() { }
}
