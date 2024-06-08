using Godot;
using System;

public partial class CardManager : Control
{
	[Export]
	public TextureButton UnknownCard;
	[Export]
	public TextureButton slimeCard;
	[Export]
	public VBoxContainer controls;
	[Signal]
	public delegate void EquipEventHandler(CardType cardType);
    public enum CardType
    {
        slime = 0,
        skeleton = 1,
        goblin = 2,
        troll = 3,
        vampire = 4
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnUnknownCardPressed()
	{
		UnknownCard.Visible = false;
		slimeCard.Visible = true;
		controls.Visible = true;

    }

	public void OnEquip()
	{
		CardType cardType = CardType.slime;
		EmitSignal(SignalName.Equip, 0);
	}

	public void OnDiscard()
	{

	}
}
