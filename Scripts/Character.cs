using Godot;
using System;
using System.Collections.Generic;
using static Godot.WebSocketPeer;

public partial class Character : CharacterBody2D
{
    [Export]
    public AnimationPlayer animPlayer;
    [Export]
    public Sprite2D slimeSprite;
    [Export]
    public Sprite2D slimeSprite2;
    [Export]
    public Sprite2D slimeSprite3;
    [Export]
    public Sprite2D skeletonSprite;
    [Export]
    public Sprite2D vampireSprite;
    [Export] 
    public Projectile projectile;
    [Export]
    public Sprite2D deadPlayer;
    [Export]
    public Sprite2D regularPlayer;
    [Signal]
    public delegate void HitEventHandler();

    public PackedScene projectileScene = ResourceLoader.Load<PackedScene>("res://Scene/Projectile.tscn");
    public Dictionary<int, Sprite2D> slimeSprites = new Dictionary<int, Sprite2D>();
    public const int MaxHealth = 100;
	public int Health;
	public int Strength;
	public int Defense;
	public int Speed;
    public bool IsDefending;
    public int TurnMeter { get; set; }
    private State CurrentState;

    public bool AttackFinished;
    public Vector2 StartingPosition;
    public bool isPlayerTeam;

    public Vector2 sliderTargetPosition;
    public Character targetCharacter;

    public Action onSlideComplete;
    public Action onAttackComplete;
    public Action onHit;
    //attack
    public Vector2 attackDirection;

    //skills
    public List<CardHandler> cards = new List<CardHandler>();

    private enum State
    {
        Idle,
        Attacking,
        Busy
    }

    // Method to perform an action (e.g., attack, defend)
    public virtual void PerformAction()
    {
        GD.Print($"{Name} performs an action.");
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        animPlayer.AnimationFinished += OnAnimationFinished;
        //slimeSprite.Texture = new CompressedTexture2D();
    }

    public void InitializeSprites()
    {
        slimeSprites = new Dictionary<int, Sprite2D>
        {
            { 0, slimeSprite },
            { 1, slimeSprite2 },
            { 2, slimeSprite3 },
            { 3, skeletonSprite },
            { 4, vampireSprite }
        };
    }

    public void EnableEnemySprite(int index)
    {
        slimeSprites.TryGetValue(index, out Sprite2D sprite);
        sprite.Visible = true;
    }

    public void Setup(bool isPlayerTeam)
    {
        this.isPlayerTeam = isPlayerTeam;
        if(isPlayerTeam)
        {
            //set the texture

        }
        else
        {
            //set the enemy texture
        }
        playAnimationIdle();

    }

    private void playAnimationIdle()
    {
        if (isPlayerTeam)
        {
            animPlayer.Play("idle");
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
        switch (CurrentState)
        {
            case State.Idle:

                break;
            case State.Busy:

                break;
            case State.Attacking:
                float slideSpeed = 5f;
                GlobalPosition += (sliderTargetPosition - GlobalPosition) * slideSpeed * (float)delta;
                float reachedDistance = 100f;
                
                if (GlobalPosition.DistanceTo(sliderTargetPosition) < reachedDistance)
                {
                    //arrived at position
                    onSlideComplete();
                }
                break;

        }
	}

    public void Attack(Character character, Action onAttackComplete)
    {
        var sliderTargetPosition = character.GlobalPosition + (GlobalPosition - character.GlobalPosition).Normalized() * 100f;
        targetCharacter = character;
        this.onAttackComplete = onAttackComplete;
        SlideToDestination(sliderTargetPosition, () =>
        {
            CurrentState = State.Busy;
            Vector2 attackDirection = (character.GlobalPosition - GlobalPosition).Normalized();
            PlayAttackAnimation(attackDirection);

        });
    }

    public void OnAnimationFinished(StringName animName)
    {
        if(animName == "attack")
        {
            AttackFinished = true;
            int damageAmount = new Random().Next(Strength-10, Strength);
            damageAmount = targetCharacter.IsDefending ? damageAmount / 2 : damageAmount;
            targetCharacter.TakeDamage(damageAmount, () =>
            {
                GD.Print("took damage");
                SlideToDestination(StartingPosition, () =>
                {
                    CurrentState = State.Idle;
                    playAnimationIdle();
                    onAttackComplete();
                });
            });

        }
    }

    public void PlayAttackAnimation(Vector2 attackDirection)
    {
        this.attackDirection = attackDirection;
        animPlayer.Play("attack");
    }

    public void Defend()
    {

    }

    public void UseCard(Character character)
    {
        projectile = projectileScene.Instantiate<Projectile>();
        this.targetCharacter = character;
        var direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(-Rotation)).Normalized();
        projectile.Direction = direction;
        projectile.GlobalPosition = GlobalPosition;
        AddChild(projectile);
        CurrentState = State.Busy;
        //Vector2 attackDirection = (character.GlobalPosition - GlobalPosition).Normalized();
        //PlayAttackAnimation(attackDirection);

    }

    public void SlideToDestination(Vector2 destination, Action onSlideComplete)
    {
        this.sliderTargetPosition = destination;
        this.onSlideComplete = onSlideComplete;
        CurrentState = State.Attacking;
        if(sliderTargetPosition.X > 0)
        {
            //play animation slide right
        }
        else
        {
            //play animation slide left
        }
    }

    public void FireCardAttack()
    {

    }

    public void TakeDamage(int damage, Action onHit)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
        if (onHit != null)
        {
            animPlayer.Play("hitflash");
            onHit();
        }
    }

    public void OnHurtBoxEntered(Node2D body)
    {
        if (body != null)
        {
            //animPlayer.Play("hitflash");
        }
    }

    public void OnBodyEntered(Node2D body)
    {
        GD.Print(body);
        if (body != null)
        {
            
        }
    }

    public void OnEnemyBodyEntered(Node2D body)
    {
        GD.Print("is projectile: ", body is Projectile);

        if (body != null)
        {
            if(body is Projectile)
            {
                AttackFinished = true;
                int damageAmount = new Random().Next(Strength - 10, Strength);
                TakeDamage(damageAmount, () =>
                {
                    EmitSignal(SignalName.Hit);

                });
            }
            //animPlayer.Play("hitflash");
        }
    }

    public void OnEnemyHurtBoxEntered(Node2D body)
    {
        GD.Print("is projectile: ", body is Projectile);
        if (body != null)
        {
            if (body is Projectile)
            {
                AttackFinished = true;
                int damageAmount = new Random().Next(Strength - 10, Strength);
                TakeDamage(damageAmount, () =>
                {
                    EmitSignal(SignalName.Hit);

                });
            }
            //animPlayer.Play("hitflash");
        }
    }

    public bool IsDead()
    {
        return Health <= 0;
    }

    public int GetHealthAmount()
    {
        return Health;
    }

    public int GetMaxHealthAmount()
    {
        return MaxHealth;
    }

    public void SetHealthAmount(int health)
    {
        this.Health = health;
    }

    public void Heal(int amount)
    {
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }
}

