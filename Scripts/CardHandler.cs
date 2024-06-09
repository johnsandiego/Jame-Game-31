using Godot;
using System;
using static CardManager;

public partial class CardHandler : Control
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
	public Label Title { get; set; }
	[Export]
	public Label Description { get; set; }

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
				SlimeTextureRect.Visible = true;
                
                //later apply texture of card
                // apply title and description
                break;
            case CardType.skeleton:

                Title.Text = title;
                Description.Text = description;
                SlimeTextureRect.Visible = true;
                break;
            case CardType.goblin:

                Title.Text = title;
                Description.Text = description;
                SlimeTextureRect.Visible = true;
                break;
            case CardType.troll:

                Title.Text = title;
                Description.Text = description;
                SlimeTextureRect.Visible = true;
                break;
            case CardType.vampire:

                Title.Text = title;
                Description.Text = description;
                SlimeTextureRect.Visible = true;
                break;
        }
        return this;

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


}