using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
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
    public CardHandler card1Button;
    [Export]
    public CardHandler card2Button;
    [Export]
    public CardHandler card3Button;
    [Export]
    public Button startNextBattle;

    public PackedScene playerScene = ResourceLoader.Load<PackedScene>("res://Scene/Player.tscn");
    public PackedScene enemyScene = ResourceLoader.Load<PackedScene>("res://Scene/Enemy.tscn");
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
        InitializePlayer(true);
        SpawnNewEnemy();
        SetActiveCharacter(player);        
        state = State.WaitingForPlayer;

        //get reference
        cardManager = randomCard.GetNode<CardManager>("RandomCard");
        GD.Print(cardManager is CardManager);
        cardManager.Equip += OnEquip;
        cardManager.Discard += OnDiscard;
    }

    public void OnEquip(CardType cardType)
    {
        switch(cardType)
        {
            case CardType.slime:
                if (player.cards.Length < 4)
                {
                    if (player.cards[0] == null)
                    {
                        player.cards[0] = card1Button;
                        card1Button = card1Button.InitializeCard("Sticky Slime", "Launches a sticky goo causing slowness", CardType.slime);
                        card1Button.Visible = true;

                    }
                    else if(player.cards[1] == null)
                    {
                        player.cards[1] = card2Button;
                        card2Button = card2Button.InitializeCard("Sticky Slime", "Launches a sticky goo causing slowness", CardType.slime);
                        card2Button.Visible = true;
                    }
                    else if (player.cards[2] == null)
                    {
                        player.cards[2] = card3Button;
                        card3Button = card3Button.InitializeCard("Sticky Slime", "Launches a sticky goo causing slowness", CardType.slime);
                        card3Button.Visible = true;
                    }
                }

                randomCard.Visible = false;
                startNextBattle.Visible = true;
                //later apply texture of card
                // apply title and description
                break;
            case CardType.skeleton:
                if (player.cards.Length < 4)
                {
                    if (player.cards[0] == null)
                    {
                        player.cards[0] = card1Button;
                        card1Button = card1Button.InitializeCard("Bone Club", "Hits the enemy with a leg bone", CardType.skeleton);
                        player.Strength = 3;
                        card1Button.Visible = true;

                    }
                    else if (player.cards[1] == null)
                    {
                        player.cards[1] = card2Button;
                        card2Button = card2Button.InitializeCard("Sticky Slime", "Launches a sticky goo causing slowness", CardType.skeleton);
                        card2Button.Visible = true;
                    }
                    else if (player.cards[2] == null)
                    {
                        player.cards[2] = card3Button;
                        card3Button = card3Button.InitializeCard("Sticky Slime", "Launches a sticky goo causing slowness", CardType.skeleton);
                        card3Button.Visible = true;
                    }
                }
                break;
            case CardType.goblin: break;
            case CardType.troll: break;
            case CardType.vampire: break;
        }
    }

    public void OnDiscard()
    {
        randomCard.Visible = false;
        cardManager.InitializeValues();
        startNextBattle.Visible = true;
    }

    private void InitializePlayer(bool newPlayer)
    {
        if (newPlayer)
        {
            player = playerScene.Instantiate<Character>();
            player.Hit += OnCharacterHit;
            playerNode.AddChild(player);
            player.GlobalPosition = playerNode.GlobalPosition;
            player.StartingPosition = playerNode.GlobalPosition;
        }


        player.Health = player.GetMaxHealthAmount();
        player.Strength = 90;
        player.Defense = 2;
        player.Speed = 2;

        playerhealth.MaxValue = player.GetMaxHealthAmount();
        playerhealth.Value = player.GetHealthAmount();

    }

    public void OnCharacterHit()
    {
        GD.Print("character is hit");
        ChooseNextActiveCharacter();
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
                    player.IsDefending = true;
                    ChooseNextActiveCharacter();
                    break;
                case PlayerAction.UseCard1:
                    state = State.Busy;
                    player.UseCard(enemy);
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
            player.regularPlayer.Visible = false;
            player.deadPlayer.Visible = true;
            player.animPlayer.Play("dead");
            //show a card with the question mark. so player can click on it.
            //enable the next arrow for next fight
            return true;
        }

        if (enemy.IsDead())
        {
            enemy.QueueFree();
            cardManager.InitializeValues();
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

    private void OnStartNextBattle()
    {
        InitializePlayer(false);
        SpawnNewEnemy();

        state = State.WaitingForPlayer;
        startNextBattle.Visible = false;
        isPlayerTurn = false;
        //spawn a new enemy
        //spawn the player by sliding them in 
        //set state to waiting on player
        //disable the start next battler button
    }

    private void SpawnNewEnemy()
    {
        enemy = enemyScene.Instantiate<Character>();
        enemy.Hit += OnEnemyHit;
        enemy.InitializeSprites();
        //enemy.GlobalPosition = enemyNode.GlobalPosition;
        enemyNode.AddChild(enemy);
        enemy.StartingPosition = enemyNode.GlobalPosition;

        enemy.Health = enemy.GetMaxHealthAmount();
        enemy.Strength = 90;
        enemy.Defense = 2;
        enemy.Speed = 2;
        enemyhealth.MaxValue = enemy.GetMaxHealthAmount();
        enemyhealth.Value = enemy.GetHealthAmount();
        enemy.EnableEnemySprite(new Random().Next(0, 4));

    }

    private void OnEnemyHit()
    {
        ChooseNextActiveCharacter();
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
