using Godot;
using System;
using System.Collections.Generic;
using static CardManager;

public partial class SkillManager
{
    private Dictionary<CardType, Skill> skills = new Dictionary<CardType, Skill>()
    {
        {CardType.slime, new Skill("Sticky Slime", "Launches a sticky goo.", new Dictionary<string, int> { { "Damage", 3 } }, CardType.slime)},
        {CardType.skeleton, new Skill("Leg Bone", "A Leg bone that can tossed.", new Dictionary<string, int> { { "Damage", 4 } }, CardType.skeleton) },
        {CardType.troll, new Skill("Heal", "Restores health.", new Dictionary<string, int> { { "Damage", 3 }, { "Heal", 3 } }, CardType.troll)},
        {CardType.goblin, new Skill("Acid", "A weak acid that causes some burn.", new Dictionary<string, int> { { "Damage", 3 } }, CardType.goblin)},
        {CardType.vampire, new Skill("Blood Spike", "Spawns a bloody pool which shoots spikes", new Dictionary<string, int> { { "Damage", 6 } }, CardType.vampire)},
        {CardType.truckkun, new Skill("Truck-kun", "Launches a truck that sends you to another world", new Dictionary<string, int> { { "Damage", 1000 } }, CardType.truckkun)},
        {CardType.nothing, new Skill("Nothing", "Better luck next time", new Dictionary<string, int> { { "Damage", 0 } }, CardType.nothing)},
        {CardType.nothing2, new Skill("Nothing", "Better luck next time", new Dictionary<string, int> { { "Damage", 0 } }, CardType.nothing2)},
        {CardType.nothing3, new Skill("Nothing", "Better luck next time", new Dictionary<string, int> { { "Damage", 0 } }, CardType.nothing3)},
    };


    public Dictionary<CardType, Skill> GetSkills()
    {
        skills = new Dictionary<CardType, Skill>();

        // Example skills
        skills.Add(CardType.slime, new Skill("Sticky Slime", "Launches a sticky goo.", new Dictionary<string, int> { { "Damage", 10 } }, CardType.slime));
        skills.Add(CardType.slime, new Skill("Leg Bone", "A Leg bone that can tossed.", new Dictionary<string, int> { { "Damage", 20 } }, CardType.skeleton));
        skills.Add(CardType.slime, new Skill("Heal", "Restores health.", new Dictionary<string, int> { { "Heal", 20 } }, CardType.troll));
        skills.Add(CardType.slime, new Skill("Acid", "A weak acid that causes some burn.", new Dictionary<string, int> { { "Damage", 25 } }, CardType.goblin));
        skills.Add(CardType.slime, new Skill("Blood Spike", "Spawns a bloody pool which shoots spikes", new Dictionary<string, int> { { "Damage", 50 } }, CardType.vampire));
        return skills;
    }

    public Skill GetSkill(CardType type)
    {
        if (skills.TryGetValue(type, out Skill value)) return value;
        return null;
    }
}