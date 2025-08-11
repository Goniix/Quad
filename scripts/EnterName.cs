using System;
using Godot;

namespace Quad.scripts;

public partial class EnterName : LineEdit
{
    private string[] _defaultNames =
    [
        "WoodRaptor",
        "EpicSteel",
        "ViperPandora",
        "CookieSniper",
        "SwordCraby",
        "MoonWolf",
        "CrabyDoor",
        "DeathShield",
        "DoctorAim",
        "DeathLotus",
        "BadSword",
        "SmallMorphine",
        "EpicDragon",
        "NightMilo",
        "FreezShadow",
        "FlyingBronze",
        "WarDream",
        "EpicCheat",
        "BetaIron",
        "WolfGt",
        "BreadCar",
        "LuckOmega",
        "NeverRiku",
        "RexEagle"
    ];

    public override void _Ready()
    {
        TextChanged += SaveName;

        Text = Random.Shared.GetItems(_defaultNames, 1)[0];
        SaveName(Text);
    }

    private void SaveName(string text)
    {
        OnlineLobby.Instance.LocalPlayerInfo.Name = text;
    }
}