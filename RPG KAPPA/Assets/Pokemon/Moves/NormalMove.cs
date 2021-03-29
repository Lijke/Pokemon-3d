using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " New Normal Move", menuName = "Moves/Normal move")]
public class NormalMove : MovesObject
{
    public void Awake()
    {
        moveType = MoveType.Normal;
    }
}
