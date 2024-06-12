using Godot;
using System;
using System.Collections.Generic;
using static CardManager;

public class EnemyBase
{
    public CardType CardType;
    public string Name;
    public string Description;
    public const int MaxHealth = 100;
    public int Health;
    public int Strength;
    public int Defense;
    public int Speed;
    public List<CardType> cardDrops = new List<CardType>();
    public double SpawnChance;

    public EnemyBase(CardType cardType, string name, string description, int health, int strength, int defense, int speed, double spawnChance, List<CardType> cardTypes)
    {
        CardType = cardType;
        Name = name;
        Description = description;
        Health = health;
        Strength = strength;
        Defense = defense;
        Speed = speed;
        SpawnChance = spawnChance;
        cardDrops = cardTypes;
    }

}
