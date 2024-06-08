using Godot;
using System;
using static Godot.WebSocketPeer;

public partial class Character : CharacterBody2D
{
    public const int MaxHealth = 100;
	public int Health;
	public int Strength;
	public int Defense;
	public int Speed;
    public int TurnMeter { get; set; }
    private State CurrentState;
    [Export]
    public AnimationPlayer animPlayer;
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
                float slideSpeed = 10f;
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

    public void UseCard()
    {

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
        GD.Print(body);
        if (body != null)
        {
            //animPlayer.Play("hitflash");
        }
    }

    public void OnEnemyHurtBoxEntered(Node2D body)
    {
        GD.Print(body);
        if (body != null)
        {
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

