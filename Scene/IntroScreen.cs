using Godot;
using System;

public partial class IntroScreen : Control
{
	[Export]
	public RollingTextLabel rollingText;
	[Export]
	public TextureRect truck;
	[Export]
	public AudioStreamPlayer2D audio;
	[Export]
	public Panel panel;
	[Export]
	public AnimationPlayer anim;
	public PackedScene mainScene = ResourceLoader.Load<PackedScene>("res://Scene/CombatScene.tscn");
    private Timer timer;
	public bool firstSceneOver = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		rollingText.RollingTextOver += OnRollingTextOver;

        rollingText.SetText("Another day, another dollar. When will something exciting happen in my life?    ");
        timer = new Timer();
		timer.Timeout += OnTimeout;
        AddChild(timer);
		anim.AnimationFinished += OnAnimationFinished;

    }

    public void OnRollingTextOver()
	{
		if (!firstSceneOver)
		{
			rollingText.Visible = false;
			truck.Visible = true;
			timer.Start(1);
			audio.Play(0);
		}
	}

	public void OnAnimationFinished(StringName animName)
	{
		if(animName == "Flash")
		{
            rollingText.Visible = true;
            rollingText.SetText("Truck-kun at it again.");
            firstSceneOver = true;
        }

	}


    public void OnTimeout()
	{
        anim.Play("Flash");
        truck.Visible = false;

        timer.Stop();


    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		var input = Input.IsKeyPressed(Key.Space) || Input.IsKeyPressed(Key.Enter) || Input.IsKeyPressed(Key.Escape) ||
			Input.IsMouseButtonPressed(MouseButton.Left) || Input.IsMouseButtonPressed(MouseButton.Right);

        if (input && firstSceneOver)
		{
            GetTree().ChangeSceneToPacked(mainScene);
        }
	}
}
