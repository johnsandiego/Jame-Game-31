using Godot;
using System;

public partial class OptionsManager : Panel
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnStatOpen()
    {
        //smit signal to close the stat screen
    }

    public void OnStatClose()
    {
        //emit signal to close the stat screen
    }
}
