using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool _isConeSpawned = false;
    [SerializeField] private GameObject[] _block;
    [SerializeField] private GameObject _cone, _boost, _boostPoint;
    [SerializeField] private Vector3 _poolPosition, _conePosition, _boostPosition;
    [SerializeField] private int _blocksAmount = 15, _coneAmount = 45;
    [SerializeField] private int _currentBlock = 0, _currentCone = 0;
    [SerializeField] private bool _isFirstTime;
    private float _randZ;
    private GameObject[] _blocks, _cones;

    private float[] _lines = new float[3]{-0.5f, 0, 0.5f};
    private int _randNum;

    private void Start()
    {
        _blocks = new GameObject[_blocksAmount];
        _cones = new GameObject[_coneAmount];

        CreateBoost();
        CreateBlocks();
        if (!_isFirstTime)
        {
            Time.timeScale = 1;
            CreateCones();
        }
    }

    private void CreateBlocks()
    {
        for (int i = 0; i < _blocksAmount; i++)
        {
            _randNum = Random.Range(0, 2);
            _poolPosition.z += 4.8f;
            _blocks[i] = Instantiate(_block[_randNum], _poolPosition, Quaternion.identity);
        }
    }

    public void CreateCones()
    {
        for (int i = 0; i < _coneAmount; i++)
        {
            _randNum = Random.Range(0, 3);
            _conePosition.x = _lines[_randNum];
            _conePosition.z += 3f;
            _cones[i] = Instantiate(_cone, _conePosition, Quaternion.identity);
        }
    }

    public void ChangeBlockPosition()
    {
        _poolPosition.z += 4.8f;
        _blocks[_currentBlock].transform.position = _poolPosition;
        _currentBlock++;
        if (_currentBlock >= _blocksAmount)
        {
            _currentBlock = 0;
        }
    }

    public void ChangeConePosition()
    {
        _conePosition.z += 3f;
        _randNum = Random.Range(0, 3);
        _conePosition.x = _lines[_randNum];
        _cones[_currentCone].transform.position = _conePosition;
        _currentCone++;
        if (_currentCone >= _coneAmount)
        {
            _currentCone = 0;
        }
    }

    private void CreateBoost()
    {
        _randNum = Random.Range(0, 2);
        _randZ = Random.Range(40, 50);
        _boostPosition = new Vector3(_lines[_randNum], 0.1f, _randZ);
        _boostPoint = Instantiate(_boost, _boostPosition, Quaternion.identity);
    }

    public void ChangeBoostPosition()
    {
        _boostPosition.x = _lines[Random.Range(0, 2)];
        _boostPosition.z += 50f;
        _boostPoint.transform.position = _boostPosition;
    }
}
