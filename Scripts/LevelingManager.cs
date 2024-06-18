using Godot;
using System;

public class LevelingManager
{
    public int CalculateXPForNextLevel(int level)
    {
        // Example formula: XP required for next level increases exponentially
        return 100 * level * level;
    }

    public void AddXP(int Level, float Experience, float ExpToNextLevel, int amount)
    {
        Experience += amount;
        while (Experience >= ExpToNextLevel)
        {
            LevelUp(Level, Experience, ExpToNextLevel);
        }
    }

    private void LevelUp(int Level, float Experience, float ExpToNextLevel)
    {
        Level++;
        Experience -= ExpToNextLevel;
        ExpToNextLevel = CalculateXPForNextLevel(Level);
        OnLevelUp(Level);
    }

    private void OnLevelUp(int Level)
    {
        // Handle logic that should happen when the character levels up
        // For example, increase stats
        Console.WriteLine($"Leveled up to {Level}!");
        //show the level up screen so user can select a stat to increase

        //show the skill tree so they can choose a skill
    }
}
