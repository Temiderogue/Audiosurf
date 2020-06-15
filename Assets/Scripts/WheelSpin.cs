using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameCycle _gameCycle;
    private void FixedUpdate()
    {
        if (_gameCycle.isGameStarted)
        {
            transform.Rotate(0f, 0f, _speed);
        }
    }
}
