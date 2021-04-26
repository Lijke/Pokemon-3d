using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTest : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;

    void OnMouseDown()
    {
        Die();
    }
    void Die()
    {
        gameEvent?.Invoke();
    }
}
