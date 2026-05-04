using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    [SerializeField] Rigidbody _player;
    [SerializeField] float _playerMoveSpeed = 3;

    Vector3 _playerDirection;

    void Update()
    {
        _playerDirection = new Vector3(
            Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        _player.velocity = _playerDirection * _playerMoveSpeed;
    }
}