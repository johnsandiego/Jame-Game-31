using Godot;
using System;
using System.Collections.Generic;
using static CardManager;

public partial class TurnManager : Node
{
    [Export]
    public Node2D enemyNode;
    [Export]
    public Node2D playerNode;

    //ui
    [Export]
    public ProgressBar playerhealth;
    [Export]
    public ProgressBar enemyhealth;
    [Export]
    public Panel randomCard;
    [Export]
    public TextureButton card1Button;
    [Export]
    public TextureButton card2Button;
    [Export]
    public TextureButton card3Button;

    public PackedScene playerScene = ResourceLoader.Load<PackedScene>("res://Scene/Player.tscn");
    public PackedScene enemyScene = ResourceLoader.Load<PackedScene>("res://Scene/Slime.tscn");
    private State state;

    public Character player;

    public bool isPlayerTurn;
    private PlayerAction playerAction;

    public Character enemy;
    public bool isEnemyTurn;
    public Character activeCharacter;
    public CardManager cardManager;



    private enum State
    {
        WaitingForPlayer = 0,
        Busy = 1
    }
    public enum PlayerAction
    {
        Attacked = 0,
        Defended = 1,
        UseCard1 = 2,
        UseCard2 = 3,
        UseCard3 = 4
    }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        InitializeCharacters();
        SetActiveCharacter(player);        
        state = State.WaitingForPlayer;

        //get reference
        cardManager = randomCard.GetNode<CardManager>("RandomCard");
        GD.Print(cardManager is CardManager);
        cardManager.Equip += OnEquip;
    }

    public void OnEquip(CardType cardType)
    {
        switch(cardType)
        {
            case CardType.slime:
                card1Button.Visible = true;
                //later apply texture of card
                // apply title and description
                break;
            case CardType.skeleton: break;
            case CardType.goblin: break;
            case CardType.troll: break;
            case CardType.vampire: break;
        }
    }

    private void InitializeCharacters()
    {
        player = playerScene.Instantiate<Character>();
        enemy = enemyScene.Instantiate<Character>();
        playerNode.AddChild(player);
        player.GlobalPosition = playerNode.GlobalPosition;
        enemyNode.AddChild(enemy);
        enemy.GlobalPosition = enemyNode.GlobalPosition;
        player.StartingPosition = playerNode.GlobalPosition;
        enemy.StartingPosition = enemyNode.GlobalPosition;

        player.Health = player.GetMaxHealthAmount();
        player.Strength = 90;
        player.Defense = 2;
        player.Speed = 2;

        enemy.Health = enemy.GetMaxHealthAmount();
        enemy.Strength = 90;
        enemy.Defense = 2;
        enemy.Speed = 2;


        playerhealth.MaxValue = player.GetMaxHealthAmount();
        playerhealth.Value = player.GetHealthAmount();
        enemyhealth.MaxValue = enemy.GetMaxHealthAmount();
        enemyhealth.Value = enemy.GetHealthAmount();

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (state == State.WaitingForPlayer && isPlayerTurn)
        {
            
            switch(playerAction)
            {
                case PlayerAction.Attacked:
                    state = State.Busy;
                    player.Attack(enemy, () =>
                    {
                        ChooseNextActiveCharacter();
                    });
                    break;
                case PlayerAction.Defended:
                    state = State.Busy;
                    //player.Defend(enemy, playerAction);
                    break;
                case PlayerAction.UseCard1:
                    state = State.Busy;
                    //player.UseCard(enemy, card, playerAction);
                    break;
                case PlayerAction.UseCard2:
                    state = State.Busy;
                    //player.UseCard(enemy, card, playerAction);
                    break;
                case PlayerAction.UseCard3:
                    state = State.Busy;
                    //player.UseCard(enemy, card, playerAction);
                    break;
            }
        }

        playerhealth.Value = player.GetHealthAmount();
        enemyhealth.Value = enemy.GetHealthAmount();
    }

    private Character SpawnCharacter(bool isPlayerTeam)
    {
        Vector2 position;
        if (isPlayerTeam)
        {
            position = new Vector2(-50, 0);
        }
        else
        {
            position = new Vector2(+50, 0);
        }

        var characterTransform = playerScene.Instantiate<Character>();
        characterTransform.Setup(isPlayerTeam);
        //characterBattle.Setup(isPlayerTeam);

        return characterTransform;
    }

    private void ChooseNextActiveCharacter()
    {
        if (IsBattleOver())
        {
            return;
        }

        if (activeCharacter == player)
        {
            SetActiveCharacter(enemy);
            state = State.Busy;
            playerAction = new Random().Next(0, 1) == 0 ? PlayerAction.Attacked : PlayerAction.Defended;
            enemy.Attack(player, () =>
            {
                //
                ChooseNextActiveCharacter();
            });
        }
        else
        {
            isPlayerTurn = false;
            SetActiveCharacter(player);
            state = State.WaitingForPlayer;
        }
    }

    private bool IsBattleOver()
    {
        if (player.IsDead())
        {
            GD.Print("player is dead");
            //show a card with the question mark. so player can click on it.
            //enable the next arrow for next fight
            return true;
        }

        if (enemy.IsDead())
        {
            enemy.QueueFree();
            randomCard.Visible = true;
            GD.Print("enemy is dead");
            return true;
        }

        return false;
    }

    private void SetActiveCharacter(Character character)
    {
        if (activeCharacter != null)
        {
            //activeCharacter.HideSelectionCircle();
        }

        activeCharacter = character;
        //activeCharacter.ShowSelectionCircle();
    }

    private void OnSlashPressed() 
    {
        playerAction = PlayerAction.Attacked;
        isPlayerTurn = true;
    }

    private void OnPlayerDefend()
    {
        playerAction = PlayerAction.Defended;
        isPlayerTurn = true;
    }

    private void OnPlayerUseCard1()
    {
        playerAction = PlayerAction.UseCard1;
        isPlayerTurn = true;
    }

    private void OnPlayerUseCard2()
    {
        playerAction = PlayerAction.UseCard2;
        isPlayerTurn = true;
    }

    private void OnPlayerUseCard3()
    {
        playerAction = PlayerAction.UseCard3;
        isPlayerTurn = true;
    }

    private List<Character> characters = new List<Character>();
    private int turnThreshold = 100;

    public void AddCharacter(Character character)
    {
        characters.Add(character);
    }

    private void UpdateTurnMeters()
    {
        foreach (Character character in characters)
        {
            character.TurnMeter += character.Speed;
        }
    }

    private Character GetNextTurnCharacter()
    {
        foreach (Character character in characters)
        {
            if (character.TurnMeter >= turnThreshold)
            {
                return character;
            }
        }
        return null;
    }
}