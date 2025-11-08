using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.IO;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Preparation
{
    public string Prep1Intro { get; set; }
    public Decision Prep1Dec1 { get; set; }
    public Decision Prep1Dec2 { get; set; }
    public Decision Prep1Dec3 { get; set; }
    public Decision Prep1Dec4 { get; set; }
    public Outro Prep1Outro { get; set; }
}

public class Decision
{
    public string Prompt { get; set; }
    public Answer Answer1 { get; set; }
    public Answer Answer2 { get; set; }
}

public class Answer
{
    public string Option { get; set; }
    public string Outcome { get; set; }
    public int[] Influence { get; set; }
}

public class Outro
{
    public string Prompt { get; set; }
    public Answer Answer1 { get; set; }
    public Answer Answer2 { get; set; }
}

public class Preperation() { }

public class Battle() { }

public partial class Main : Control
{
    public override void _Ready()
    {
        LoadDialogue();
    }

    public static (Dictionary<string, Preperation>, Dictionary<string, Battle>) LoadDialogue()
    {
        Preparation preparation = JsonConvert.DeserializeObject<Preparation>(jsonString);
        return ([], []);
    }
}
