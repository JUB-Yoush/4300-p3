using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public enum Modifiers
{
    NONE,
    SWORDSMEN,
    SHIELDMEN,
    ARCHERS,
    HORSE,
    STRENGTH,
}

public enum PromptType
{
    BATTLE,
    PREP,
}

public enum HeroType
{
    NONE,
    KING,
    BARB,
}

public enum GameState
{
    PLAYING,
    WIN,
    LOSE,
    WIPED,
}

public interface INode { } // we have union types at home

public partial class Main : Control
{
    Button Choice1Btn,
        Choice2Btn = null!;

    Decision Choice1 = null;
    Decision Choice2 = null;

    RichTextLabel Prompt = null!;

    Decision NextNode = null;

    INode currentNode = null;

    HeroType hero = HeroType.NONE;

    public int[] Stats = new int[Enum.GetNames(typeof(Modifiers)).Length];

    public bool Conditional(string key)
    {
        GD.Print($"condition:{key}");
        return key switch
        {
            "has_horse" => Stats[(int)Modifiers.HORSE] > 0,
            "has_archers" => Stats[(int)Modifiers.ARCHERS] > 0,
            "has_swords" => Stats[(int)Modifiers.SWORDSMEN] > 0,
            "has_weak" => Stats[(int)Modifiers.STRENGTH] < 0,
            "not_weak" => Stats[(int)Modifiers.STRENGTH] > -1,
            "has_strength" => Stats[(int)Modifiers.STRENGTH] > 0,
            "has_shieldmen" => Stats[(int)Modifiers.SHIELDMEN] > 0,
            "chance_25" => GD.Randf() <= 0.25,
            "chance_50" => GD.Randf() <= 0.5,
            "chance_75" => GD.Randf() <= 0.75,
            null => true,
            "" => true,
            _ => throw new Exception(),
        };
    }

    public override void _Ready()
    {
        Choice1Btn = GetNode<Button>("%Choice1");
        Choice2Btn = GetNode<Button>("%Choice2");
        Prompt = GetNode<RichTextLabel>("%Prompt");
        Choice1Btn.Pressed += OnChoice1Pressed;
        Choice2Btn.Pressed += OnChoice2Pressed;
        LoadDescision(GD.Load<Decision>("res://assets/prep1/p1d0.tres"));
        UpdateStatLabel();
    }

    void EndGame(GameState state)
    {
        Choice1Btn.Text = "RESET";
        Choice2Btn.Visible = false;
        Choice1Btn.Pressed -= OnChoice1Pressed;
        Choice1Btn.Pressed += () => GetTree().ReloadCurrentScene();
    }

    public void LoadDescision(Decision decision)
    {
        if (decision.newState == GameState.WIN || decision.newState == GameState.LOSE)
        {
            currentNode = decision;
            Prompt.Text = decision.Outcome;
            EndGame(decision.newState);
            return;
        }

        Choice1Btn.Disabled = false;
        Choice2Btn.Disabled = false;
        currentNode = decision;
        if (decision.ConditionKey != null && decision.ConditionKey != "")
        {
            if (Conditional(decision.ConditionKey))
            {
                LoadDescision(decision.ConditionalOutcome1);
            }
            else
            {
                LoadDescision(decision.ConditionalOutcome2);
            }
            return;
        }
        currentNode = decision;
        Prompt.Text = decision.Outcome;

        foreach (var statChange in decision.Influence)
        {
            if (statChange.Key != Modifiers.STRENGTH)
            {
                Stats[(int)statChange.Key] = Math.Max(
                    0,
                    Stats[(int)statChange.Key] + statChange.Value
                );
            }
            else
            {
                Stats[(int)statChange.Key] += statChange.Value;
            }
        }

        if (decision.newState == GameState.WIPED)
        {
            Stats = new int[Enum.GetNames(typeof(Modifiers)).Length];
        }

        Choice1Btn.Text = "Continue";
        Choice2Btn.Visible = false;
        UpdateStatLabel();

        if (decision.setHero != HeroType.NONE && hero != HeroType.NONE)
        {
            hero = decision.setHero;
        }
    }

    public void LoadPrompt(Prompt prompt)
    {
        Choice1Btn.Visible = false;
        Choice2Btn.Visible = false;
        Choice1Btn.Disabled = false;
        Choice2Btn.Disabled = false;
        GD.Print($"titles???{prompt.Answer1.Description}");

        currentNode = prompt;
        Prompt.Text = prompt.PromptText;

        if (Conditional(prompt.Answer1ConditionKey))
        {
            GD.Print(prompt.Answer1ConditionKey);
            Choice1Btn.Disabled = false;
            Choice1Btn.Visible = true;
            Choice1Btn.Text = prompt.Answer1.Description;
            Choice1 = prompt.Answer1;
        }
        else
        {
            Choice1Btn.Visible = true;
            Choice1Btn.Text = prompt.Answer1.Description;
            Choice1Btn.Disabled = true;
        }

        if (Conditional(prompt.Answer2ConditionKey))
        {
            GD.Print(prompt.Answer2ConditionKey);
            Choice2Btn.Visible = true;
            Choice2Btn.Text = prompt.Answer2.Description;
            Choice2 = prompt.Answer2;
        }
        else
        {
            Choice2Btn.Visible = true;
            Choice2Btn.Text = prompt.Answer2.Description;
            Choice2Btn.Disabled = true;
        }
    }

    public void OnChoice1Pressed()
    {
        if (currentNode is Decision decision)
        {
            if (hero == HeroType.NONE)
            {
                hero = decision.setHero;
            }

            GD.Print($"hero:{hero}, kingchoice:{decision.NextPromptKing != null}");

            if (decision.NextDecision != null)
            {
                LoadDescision(decision.NextDecision);
            }
            else if (decision.NextPromptKing != null && hero == HeroType.KING)
            {
                LoadPrompt(decision.NextPromptKing);
            }
            else if (decision.NextPromptBarb != null && hero == HeroType.BARB)
            {
                LoadPrompt(decision.NextPromptBarb);
            }
            else
            {
                LoadPrompt(decision.NextPrompt);
            }
        }
        else if (currentNode is Prompt prompt)
        {
            LoadDescision(prompt.Answer1);
        }
    }

    public void OnChoice2Pressed()
    {
        if (currentNode is Decision decision) { }
        else if (currentNode is Prompt prompt)
        {
            LoadDescision(prompt.Answer2);
        }
    }

    public void UpdateStatLabel()
    {
        GetNode<Label>("%Stats").Text =
            $"SWORDS:{Stats[(int)Modifiers.SWORDSMEN]}\n SHEILD:{Stats[(int)Modifiers.SHIELDMEN]}\n ARCHERS:{Stats[(int)Modifiers.ARCHERS]}\n HORSE:{Stats[(int)Modifiers.HORSE]}\n STRENGTH:{Stats[(int)Modifiers.STRENGTH]}\n";
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("reset"))
            GetTree().ReloadCurrentScene();
    }
}
