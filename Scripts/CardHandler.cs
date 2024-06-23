using Godot;
using System;
using static CardManager;

public partial class CardHandler : Button
{
	[Export]
	public TextureRect SlimeTextureRect { get; set; }
    [Export]
    public TextureRect BoneTextureRect { get; set; }

    [Export]
    public TextureRect AcidTextureRect { get; set; }

    [Export]
    public TextureRect HealTextureRect { get; set; }

    [Export]
    public TextureRect BloodSpikeTextureRect { get; set; }

    [Export]
    public TextureRect NothingTextureRect { get; set; }
    
	[Export]
	public Label Title { get; set; }
	[Export]
	public Label Description { get; set; }
    public CardType CardType { get; set; }
    public int Damage { get; set; }
	public CompressedTexture2D CompressedTexture { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//InitializeCard("Sticky Slime", "Launches a sticky liquid which has a chance of slowing the target", CardType.slime);

    }

    public CardHandler InitializeCard(string title, string description, CardType cardType)
    {
        switch (cardType)
        {
            case CardType.slime:

                Title.Text = title;
                Description.Text = description;
                SetTextureTrue(cardType);
                Damage = 10;
                CardType = cardType;
                //later apply texture of card
                // apply title and description
                break;
            case CardType.skeleton:

                Title.Text = title;
                Description.Text = description;
                SetTextureTrue(cardType);
                CardType = cardType;
                Damage = 20;
                break;
            case CardType.goblin:

                Title.Text = title;
                Description.Text = description;
                SetTextureTrue(cardType);
                CardType = cardType;
                Damage = 30;
                break;
            case CardType.troll:

                Title.Text = title;
                Description.Text = description;
                SetTextureTrue(cardType);
                CardType = cardType;
                Damage = 50;
                break;
            case CardType.vampire:

                Title.Text = title;
                Description.Text = description;
                SetTextureTrue(cardType);
                CardType = cardType;
                Damage = 80;
                break;
            default:
                Title.Text = title;
                Description.Text = description;
                SetTextureTrue(cardType);
                CardType = CardType.nothing;
                Damage = 0;
                break;
        }
        return this;

    }

    private void SetTextureTrue(CardType cardType)
    {
        switch (cardType)
        {
            case CardType.slime:

                SlimeTextureRect.Visible = true;
                BoneTextureRect.Visible = false;
                AcidTextureRect.Visible = false;
                HealTextureRect.Visible = false;
                BloodSpikeTextureRect.Visible = false;
                // apply title and description
                break;
            case CardType.skeleton:
                SlimeTextureRect.Visible = false;
                BoneTextureRect.Visible = true;
                AcidTextureRect.Visible = false;
                HealTextureRect.Visible = false;
                BloodSpikeTextureRect.Visible = false;

                break;
            case CardType.goblin:

                SlimeTextureRect.Visible = false;
                BoneTextureRect.Visible = false;
                AcidTextureRect.Visible = true;
                HealTextureRect.Visible = false;
                BloodSpikeTextureRect.Visible = false;
                break;
            case CardType.troll:

                SlimeTextureRect.Visible = false;
                BoneTextureRect.Visible = false;
                AcidTextureRect.Visible = false;
                HealTextureRect.Visible = true;
                BloodSpikeTextureRect.Visible = false;
                break;
            case CardType.vampire:

                SlimeTextureRect.Visible = false;
                BoneTextureRect.Visible = false;
                AcidTextureRect.Visible = false;
                HealTextureRect.Visible = false;
                BloodSpikeTextureRect.Visible = true;
                break;
            default:
                SlimeTextureRect.Visible = false;
                BoneTextureRect.Visible = false;
                AcidTextureRect.Visible = false;
                HealTextureRect.Visible = false;
                BloodSpikeTextureRect.Visible = false;
                break;
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


}