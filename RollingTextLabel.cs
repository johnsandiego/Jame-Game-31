using Godot;
using System;

public partial class RollingTextLabel : Label
{
	public string fullText;
	private int currentCharIndex;
	private Timer timer;
	[Export]
	public float RevealSpeed = .1f;
	[Signal]
	public delegate void RollingTextOverEventHandler();
	[Export]
	public AudioStreamPlayer2D audio;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = new Timer();
		AddChild(timer);
		timer.Timeout += OnTimerTimeout;
		//audio.Play(0);
        //SetText("Hello, Godot!");
    }

	public void SetText(string text)
	{
		fullText = text;
		currentCharIndex = 0;
		Text = "";

		//start the timer
		timer.Start(RevealSpeed);

	}

	public void OnTimerTimeout()
	{
		if(currentCharIndex < fullText.Length)
		{
			Text += fullText[currentCharIndex];
			currentCharIndex++;
		}
		else
		{
			timer.Stop();
			audio.Stop();
            EmitSignal(SignalName.RollingTextOver);
        }

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
