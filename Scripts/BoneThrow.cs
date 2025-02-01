using Godot;
using System;

public partial class BoneThrow : CharacterBody2D
{
    [Export]
    public float Speed = 5000f;
    public Vector2 Direction { get; set; }
    private Timer _lifetimeTimer;
    public AudioStreamPlayer2D audio;
    [Export]
    public Vector2 destination;
    [Export]
    public AnimationPlayer anim;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        //audio.Play(0);
        //Rotation = Direction.Angle();
        _lifetimeTimer = new Timer();
        _lifetimeTimer.WaitTime = 1.4f; // 2 seconds
        _lifetimeTimer.OneShot = true; // Timer should only run once
        _lifetimeTimer.Timeout += onTimeFinished;
        AddChild(_lifetimeTimer);
        _lifetimeTimer.Start();
        anim.Play("spin");
    }

    public void onTimeFinished()
    {
        GD.Print("projectile time is over");
        QueueFree();
    }

    public void OnBodyEntered(Node2D body)
    {
        GD.Print("projectile hit something");
        if (body != null && body is Character)
        {
            //QueueFree();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Position += Direction * Speed * (float)delta;

    }
}
