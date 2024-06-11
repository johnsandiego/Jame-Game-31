using Godot;
using System;
using static CardManager;

public partial class TurnManager : Node
{
    [Export]
    public Node2D enemyNode;
    [Export]
    public Node2D playerNode;
    [Export]
    public RollingTextLabel label;
    //ui
    [Export]
    public ProgressBar playerhealth;
    [Export]
    public ProgressBar enemyhealth;
    [Export]
    public Panel randomCard;
    [Export]
    public CardHandler card1;
    [Export]
    public CardHandler card2;
    [Export]
    public CardHandler card3;
    [Export] public Button card1Button;
    [Export] public Button card2Button;
    [Export] public Button card3Button;
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

    public int turnCounter = 0;

    //gameover panel
    [Export]
    public Panel GameOverScreen;

    [Export]
    public AudioStreamPlayer2D audio;

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
        cardManager.Replace += OnReplace;
        player.GameOver += OnDeadFinished;
        audio.Finished += audioFinished;
        //audio.Play(0);
        label.SetText("You have the ability to collect a skill from a defeated enemy and turn it into cards. Use it wisely.     ");
        label.RollingTextOver += TextOver;
    }

    public void TextOver()
    {
        Timer timer = new Timer();
        timer.Timeout += Timeout;
        AddChild(timer);
        timer.Start(2);

    }

    public void Timeout()
    {
        label.Visible = false;
    }

    public void audioFinished()
    {
        audio.Play(0);
    }

    public void OnReplace(int index, CardType c)
    {
        player.cards[index] = null;
        OnEquip(c);
        
    }

    public void OnEquip(CardType cardType)
    {
        card1Button.Visible = false;
        card2Button.Visible = false;
        card3Button.Visible = false;
        Skill skill; 
        switch(cardType)
        {
            case CardType.slime:
                if (player.cards.Length < 4)
                {
                    skill = new SkillManager().GetSkill(CardType.slime);
                    if (player.cards[0] == null)
                    {
                        
                        card1 = card1.InitializeCard(skill.Title, skill.Description, CardType.slime);
                        card1.Visible = true;
                        player.cards[0] = card1;

                    }
                    else if(player.cards[1] == null)
                    {
                        player.cards[1] = card2;
                        card2 = card2.InitializeCard(skill.Title, skill.Description, CardType.slime);

                        card2.Visible = true;
                    }
                    else if (player.cards[2] == null)
                    {
                        player.cards[2] = card3;
                        card3 = card3.InitializeCard(skill.Title, skill.Description, CardType.slime);

                        card3.Visible = true;
                    }
                }

                break;
            case CardType.skeleton:
                if (player.cards.Length < 4)
                {
                    skill = new SkillManager().GetSkill(CardType.skeleton);

                    if (player.cards[0] == null)
                    {
                        player.cards[0] = card1;
                        card1 = card1.InitializeCard(skill.Title, skill.Description, CardType.skeleton);
                        card1.Visible = true;
                    }
                    else if (player.cards[1] == null)
                    {
                        player.cards[1] = card2;
                        card2 = card2.InitializeCard(skill.Title, skill.Description, CardType.skeleton);
                        card2.Visible = true;

                    }
                    else if (player.cards[2] == null)
                    {
                        player.cards[2] = card3;
                        card3 = card3.InitializeCard(skill.Title, skill.Description, CardType.skeleton);
                        card3.Visible = true;

                    }
                }
                break;
            case CardType.goblin:
                if (player.cards.Length < 4)
                {
                    skill = new SkillManager().GetSkill(CardType.goblin);

                    if (player.cards[0] == null)
                    {
                        player.cards[0] = card1;
                        card1 = card1.InitializeCard(skill.Title, skill.Description, CardType.goblin);
                        card1.Visible = true;

                    }
                    else if (player.cards[1] == null)
                    {
                        player.cards[1] = card2;
                        card2 = card2.InitializeCard(skill.Title, skill.Description, CardType.goblin);
                        card2.Visible = true;

                    }
                    else if (player.cards[2] == null)
                    {
                        player.cards[2] = card3;
                        card3 = card3.InitializeCard(skill.Title, skill.Description, CardType.goblin);
                        card3.Visible = true;

                    }
                }
                break;
            case CardType.troll:
                if (player.cards.Length < 4)
                {
                    skill = new SkillManager().GetSkill(CardType.troll);

                    if (player.cards[0] == null)
                    {
                        player.cards[0] = card1;
                        card1 = card1.InitializeCard(skill.Title, skill.Description, CardType.troll);
                        card1.Visible = true;

                    }
                    else if (player.cards[1] == null)
                    {
                        player.cards[1] = card2;
                        card2 = card2.InitializeCard(skill.Title, skill.Description, CardType.troll);
                        card2.Visible = true;

                    }
                    else if (player.cards[2] == null)
                    {
                        player.cards[2] = card3;
                        card3 = card3.InitializeCard(skill.Title, skill.Description, CardType.troll);
                        card3.Visible = true;

                    }
                }
                break;
            case CardType.vampire:
                if (player.cards.Length < 4)
                {
                    skill = new SkillManager().GetSkill(CardType.vampire);

                    if (player.cards[0] == null)
                    {
                        player.cards[0] = card1;
                        card1 = card1.InitializeCard(skill.Title, skill.Description, CardType.vampire);
                        card1.Visible = true;

                    }
                    else if (player.cards[1] == null)
                    {
                        player.cards[1] = card2;
                        card2 = card2.InitializeCard(skill.Title, skill.Description, CardType.vampire);
                        card2.Visible = true;

                    }
                    else if (player.cards[2] == null)
                    {
                        player.cards[2] = card3;
                        card3 = card3.InitializeCard(skill.Title, skill.Description, CardType.vampire);
                        card3.Visible = true;

                    }
                }
                break;
        }

        randomCard.Visible = false;
        startNextBattle.Visible = true;
        //later apply texture of card
        // apply title and description
    }

    public void OnDiscard()
    {
        randomCard.Visible = false;
        cardManager.InitializeValues(enemy.CardType);
        startNextBattle.Visible = true;
        card1Button.Visible = false;
        card2Button.Visible = false;
        card3Button.Visible = false;
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
            player.Health = player.GetMaxHealthAmount();
            player.Strength = 20;
            player.Defense = 2;
            player.Speed = 2;
            playerhealth.MaxValue = player.GetMaxHealthAmount();
            playerhealth.Value = player.GetHealthAmount();
        }

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
                    if (player.cards[0].CardType == CardType.troll)
                    {
                        var value = new SkillManager().GetSkill(CardType.troll);
                        if(value.Stats.TryGetValue("Heal", out int amount))
                            player.Heal(amount);

                        ChooseNextActiveCharacter();
                    }
                    else
                    {
                        player.UseCard(enemy, player.cards[0].CardType);
                    }
                    player.cards[0] = null;
                    break;
                case PlayerAction.UseCard2:
                    state = State.Busy;
                    if (player.cards[1].CardType == CardType.troll)
                    {
                        var value = new SkillManager().GetSkill(CardType.troll);
                        if (value.Stats.TryGetValue("Heal", out int amount))
                            player.Heal(amount);

                        ChooseNextActiveCharacter();
                    }
                    else
                    {
                        player.UseCard(enemy, player.cards[1].CardType);
                    }
                    player.cards[1] = null;
                    //player.UseCard(enemy, card, playerAction);
                    break;
                case PlayerAction.UseCard3:
                    state = State.Busy;
                    if (player.cards[2].CardType == CardType.troll)
                    {
                        var value = new SkillManager().GetSkill(CardType.troll);
                        if (value.Stats.TryGetValue("Heal", out int amount))
                            player.Heal(amount);

                        ChooseNextActiveCharacter();
                    }
                    else
                    {
                        player.UseCard(enemy, player.cards[2].CardType);
                    }
                    player.cards[2] = null;
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
            turnCounter++;
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
            cardManager.InitializeValues(enemy.CardType);
            if(enemy.CardType == CardType.vampire && turnCounter > 1)
            {
                // Get the current scene
                PackedScene currentScene = (PackedScene)ResourceLoader.Load("res://Scene/ending.tscn");

                // Load the current scene again
                GetTree().ChangeSceneToPacked(currentScene);
            }
            randomCard.Visible = true;
            GD.Print("enemy is dead");
            enemy.QueueFree();
            return true;
        }

        return false;
    }
    
    public void OnDeadFinished()
    {
        GameOverScreen.Visible = true;
        (GameOverScreen as GameOverScreen).gameoverLabel.Visible = true;
        (GameOverScreen as GameOverScreen).continueButton.Visible = false;
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
        card1.Visible = false;
        card1Button.Visible = false;
    }

    private void OnPlayerUseCard2()
    {
        playerAction = PlayerAction.UseCard2;
        isPlayerTurn = true;
        card2.Visible = false;
        card2Button.Visible = false;
    }

    private void OnPlayerUseCard3()
    {
        playerAction = PlayerAction.UseCard3;
        isPlayerTurn = true;
        card3.Visible = false;
        card3Button.Visible = false;
    }

    private void OnStartNextBattle()
    {
        InitializePlayer(false);
        SpawnNewEnemy();

        state = State.WaitingForPlayer;
        startNextBattle.Visible = false;
        isPlayerTurn = false;

        if (player.cards[0] != null)
        {
            card1Button.Visible = true;
        }

        if (player.cards[1] != null)
        {
            card2Button.Visible = true;
        }

        if (player.cards[2] != null)
        {
            card3Button.Visible = true;
        }
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
        enemy.Defense = 2;
        enemy.Speed = 2;
        enemyhealth.MaxValue = enemy.GetMaxHealthAmount();
        enemyhealth.Value = enemy.GetHealthAmount();

        var index = new Random().Next(0, 8);
        GD.Print("index: ", index);
        enemy.CardType = index == 6 || index == 7 || index == 8 ? enemy.EnableEnemySprite(0) : enemy.EnableEnemySprite(index);
        GD.Print("cardtype: ", enemy.CardType);

        new SkillManager().GetSkill(enemy.CardType).Stats.TryGetValue("Damage", out int value);
        GD.Print("damage", value);
        enemy.Strength = value <= 0 ? 20 : value;


    }

    private void OnEnemyHit()
    {
        ChooseNextActiveCharacter();
    }

    public void OnMenu()
    {
        GameOverScreen.Visible = true;
        (GameOverScreen as GameOverScreen).gameoverLabel.Visible = false;
        (GameOverScreen as GameOverScreen).continueButton.Visible = true;
    }

    public void OnContinue()
    {
        GameOverScreen.Visible = false;
    }
}
