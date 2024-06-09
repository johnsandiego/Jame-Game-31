using Godot;
using System;

public partial class GameOverScreen : Panel
{
    [Export]
    public Label gameoverLabel;
    [Export]
    public Button continueButton;
    public void OnRestart()
    {
        // Get the current scene
        PackedScene currentScene = (PackedScene)ResourceLoader.Load(GetTree().CurrentScene.SceneFilePath);

        // Load the current scene again
        GetTree().ChangeSceneToPacked(currentScene);
    }

    public void OnQuit()
    {
        GetTree().Quit();
    }
}
