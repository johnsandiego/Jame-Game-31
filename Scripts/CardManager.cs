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
    [Export]
    public HBoxContainer replaceContainer;
	[Signal]
	public delegate void EquipEventHandler(CardType cardType);
    [Signal]
    public delegate void DiscardEventHandler();
    [Signal]
    public delegate void ReplaceEventHandler(int index, CardType cardType );

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
        vampire = 4,
		truckkun = 5,
        nothing = 6,
        nothing2 = 7,
        nothing3 = 8,
    }

    public int index;

	public CardType cardType;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        //CardNoHover = slimeCard.InitializeCard("Sticky Slime", "Launches a sticky goo causing slowness", CardType.slime);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void InitializeValues(CardType cardType)
    {
		this.cardType = cardType;
        slimeCard = new CardHandler();
		var skill = new SkillManager().GetSkill(cardType);
        CardNoHover = CardNoHover.InitializeCard(skill.Title, skill.Description, cardType);
		CardNoHover.Damage = skill.Stats.TryGetValue("Damage", out int value) ? value : 20;

        UnknownCard.Visible = true;
        CardNoHover.Visible = false;
        controls.Visible = false;
        replaceContainer.Visible = false;
        index = 0;
    }


    public void OnUnknownCardPressed()
	{
		UnknownCard.Visible = false;
        CardNoHover.Visible = true;
		controls.Visible = true;
    }

	private int CardTypeIndex(CardType cardType)
	{
        int index = 0;
        switch(cardType)
        {
            case CardType.slime:
                index = 0;
                break;
            case CardType.skeleton:
                index = 1;
                break;
            case CardType.goblin:
                index = 2;
                break;
            case CardType.troll:
                index = 3;
                break;
            case CardType.vampire:
                index = 4;
                break;
            }
            return index;
        }

	public void OnEquip()
	{
		EmitSignal(SignalName.Equip, CardTypeIndex(this.cardType));

	}

	public void OnDiscard()
	{
		EmitSignal(SignalName.Discard);

	}

    public void OnReplace()
    {
        replaceContainer.Visible = true;
    }

    public void OnFirst()
    {
        index = 0;
        EmitSignal(SignalName.Replace, index, CardTypeIndex(cardType));
    }

    public void OnSecond()
    {
        index = 1;
        EmitSignal(SignalName.Replace, index, CardTypeIndex(cardType));
    }

    public void OnThird()
    {
        index = 2;
        EmitSignal(SignalName.Replace, index, CardTypeIndex(cardType));
    }
}
