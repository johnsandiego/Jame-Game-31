
using System.Collections.Generic;
using static CardManager;

public class Skill
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Dictionary<string, int> Stats { get; set; }
    public CardType EnemyType { get; set; }

    public Skill(string title, string description, Dictionary<string, int> stats, CardType enemyType)
    {
        Title = title;
        Description = description;
        Stats = stats;
        EnemyType = enemyType;
    }
}