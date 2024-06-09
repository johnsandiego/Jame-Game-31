using Godot;
using System;

public partial class CardManager : Control
{
	[Export]
	public TextureButton UnknownCard;
	[Export]
	public CardHandler CardNoHover;
    [Export]
    public TextureButton NoCard;
    [Export]
	public VBoxContainer controls;
	[Signal]
	public delegate void EquipEventHandler(CardType cardType);
    [Signal]
    public delegate void DiscardEventHandler();

	public CardHandler slimeCard;
	public CardHandler boneCard;
	public CardHandler acidCard;
	public CardHandler healCard;
	public CardHandler bloodSpikeCard;
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
        //CardNoHover = slimeCard.InitializeCard("Sticky Slime", "Launches a sticky goo causing slowness", CardType.slime);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void InitializeValues()
    {
        slimeCard = new CardHandler();
        CardNoHover = CardNoHover.InitializeCard("Sticky Slime", "Launches a sticky goo causing slowness", CardType.slime);

        UnknownCard.Visible = true;
        CardNoHover.Visible = false;
        controls.Visible = false;
    }


    public void OnUnknownCardPressed()
	{
		UnknownCard.Visible = false;
        CardNoHover.Visible = true;
		controls.Visible = true;

    }

	public void OnEquip()
	{
		CardType cardType = CardType.slime;
		EmitSignal(SignalName.Equip, 0);

	}

	public void OnDiscard()
	{
		EmitSignal(SignalName.Discard);

	}
}
