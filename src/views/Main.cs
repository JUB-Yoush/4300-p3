using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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

public partial class Main : Control
{
    public int[] Stats = new int[Enum.GetNames(typeof(Modifiers)).Length];

    public override void _Ready() { }

    public static bool Conditional(int[] stats, int i)
    {
        return i switch
        {
            0 => stats[(int)Modifiers.ARCHERS] > 1,
            _ => false,
        };
    }
}
