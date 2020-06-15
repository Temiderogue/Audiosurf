using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _offset = -2.25f;

    private void LateUpdate()
    {
        Vector3 _newPos = transform.position;
        _newPos.z = _player.transform.position.z + _offset;
        transform.position = _newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cone")
        {
            _spawner.ChangeConePosition();
        }
        else if (other.tag == "Boost")
        {
            _spawner.ChangeBoostPosition();
        }
    }
}
