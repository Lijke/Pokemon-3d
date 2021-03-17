using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Water,
    Fire,
    Normal,
    Grass

}
[CreateAssetMenu(fileName = " New Moves", menuName = "New Move")]
public class Moves : ScriptableObject
{
    public string nameMove;
    public int damage;
    public int currentPowerPoint;
    public int maxPowerPoint;
    public MoveType moveType;


  
}
