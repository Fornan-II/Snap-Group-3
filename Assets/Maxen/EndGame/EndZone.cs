using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EndZone : MonoBehaviour
{
    public Transform PlayerTransform;

    private Collider _col;
    private bool _gameRunning = true;

    private void Start()
    {
        _col = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if(_col.bounds.Contains(PlayerTransform.position) && _gameRunning)
        {
            GameManager.Instance.EndGameRound();
            _gameRunning = false;
        }
    }
}
