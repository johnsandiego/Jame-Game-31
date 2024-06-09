using Godot;
using System;

public partial class Projectile : CharacterBody2D
{
    [Export]
    public float Speed = 5000f;
    public Vector2 Direction { get; set; }
    private Timer _lifetimeTimer;
    public AudioStreamPlayer2D audio;
    [Export]
    public Vector2 destination;
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
    }

    public void onTimeFinished()
    {
        GD.Print("projectile time is over");
        QueueFree();
    }

    public void OnBodyEntered()
    {
        GD.Print("projectile hit something");
        //QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Position += Direction * Speed * (float)delta;

    }
}
