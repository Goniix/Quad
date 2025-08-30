using System;
using Godot;
using Quad.scripts;

namespace Quad.scenes.menu.screens;



public partial class TitleScreen : Control
{
    [Export] private Button _hostButton;
    [Export] private Button _joinButton;
    [Export] private LineEdit _nameEdit;

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
        _hostButton!.Pressed += OnCreateLobbyButtonPressed;
        _joinButton!.Pressed += OnJoinLobbyButtonPressed;
        _nameEdit.TextChanged += SaveName;

        SetRandomDefaultName();
    }

    private void SetRandomDefaultName()
    {
        _nameEdit.Text = Random.Shared.GetItems(_defaultNames, 1)[0];
        SaveName(_nameEdit.Text);
    }
    
    private void SaveName(string text)
    {
        OnlineLobby.Instance.LocalPlayerInfo.Name = text;
    }
    
    private void OnCreateLobbyButtonPressed()
    {
        OnlineLobby.Instance.CreateGame();
        Menu.Instance.GotoLobby();
    }

    private void OnJoinLobbyButtonPressed()
    {
        Menu.Instance.GotoConnection();

    }
}