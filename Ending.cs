using Godot;
using System;
public partial class Ending : Control
{
    [Export]
    public RollingTextLabel text;
    [Export]
    public RollingTextLabel label;
    [Export]
    public Panel panel;
    [Export]
    public AnimationPlayer anim;
    [Export]
    public Button newgame;
    [Export]
    public Button closegame;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        text.RollingTextOver += OnRollingTextOver;
        text.SetText("And so, your journey began. With each victory, you gained new cards. The fate of this new world rest in your hands.  ");
        anim.AnimationFinished += OnAnimationFinished;
    }

    public void OnRollingTextOver()
    {
        anim.Play("white");
        text.Visible = false;

    }

    public void OnAnimationFinished(StringName anim)
    {
        if (anim == "white")
        {
            text.Visible = false;

            panel.Visible = true;
            label.SetText("Thanks for playing! I'm planning to expand this further, so follow if interested.   ");
            newgame.Visible = true;
            closegame.Visible = true;
        }
    }

    public void OnNewGame()
    {
        // Get the current scene
        PackedScene currentScene = (PackedScene)ResourceLoader.Load("res://Scene/IntroScreen.tscn");

        // Load the current scene again
        GetTree().ChangeSceneToPacked(currentScene);
    }

    public void OnQuit()
    {
        GetTree().Quit();
    }
}
