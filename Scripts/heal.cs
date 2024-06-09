using Godot;
using System;

public partial class heal : Node2D
{
	[Export]
	public AnimationPlayer anim;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim.AnimationFinished += finished;
		anim.Play("heal");
	}

	public void finished(StringName animname)
	{
		if(animname == "heal")
		{
			QueueFree();
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
