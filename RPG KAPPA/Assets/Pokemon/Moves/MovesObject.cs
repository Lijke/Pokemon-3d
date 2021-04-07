using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MoveType
{
    Water,
    Normal,
    Fire,
    Grass
}
public abstract class MovesObject : ScriptableObject
{
    public string moveName;
    public int currentPowerPoint;
    public int maxPowerPoint;
    public int baseDamage;
    public MoveType moveType;
}
