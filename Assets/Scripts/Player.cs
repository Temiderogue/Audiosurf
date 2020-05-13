using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    [SerializeField] private float  _slideSpeed;
    [SerializeField] private Rigidbody _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameCycle _gameCycle;

    [SerializeField] private float _distance = 1;
    [SerializeField] private int _targetRow = 0;
    [SerializeField] private float _firstLanePos;
    public int[] _rows = new int[] { -1, 0, 1 };

    /*
    private Vector3 fp;
    private Vector3 lp;

    private Vector3 _prevPos, _offset;
    private float dragDistance;
    */
    private void Update()
    {
        /*
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;
                _prevPos = transform.position;
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {
                        if ((lp.x > fp.x) && _targetRow < 2)
                        {   //Right swipe
                            _targetRow++;
                        }
                        else if ((lp.x < fp.x) && _targetRow > 0)
                        {   //Left swipe
                            _targetRow--;
                        }
                    }
                    else if (Mathf.Abs(lp.x - fp.x) < Mathf.Abs(lp.y - fp.y))
                    {
                        if (lp.y > fp.y)
                        {
                            //Up Swipe
                        }
                        else if (lp.y < fp.y)
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
            }
        }
        */

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x > Screen.width*2/3 && _targetRow < 2)
            {
                _targetRow++;
            }
            else if(Input.mousePosition.x < Screen.width / 3 && _targetRow > 0)
            {
                _targetRow--;
            }
        }

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, _firstLanePos + (_targetRow * _distance), Time.deltaTime * _slideSpeed);
        newPos.z += Speed * Time.deltaTime;
        transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Cone":
                _gameCycle.Death();
                break;
            case "Boost":
                _spawner.ChangeBoostPosition();
                Speed += 2f;
                break;
            case "TriggerZone":
                _spawner.ChangeBlockPosition();
                break;
        }
    }
}
