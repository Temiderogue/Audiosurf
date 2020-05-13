using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 15, 0)* _rotationSpeed);
    }
}
