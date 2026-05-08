using Strawhenge.GameManagement.Unity;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManagement.PauseGame.IsPaused)
                GameManagement.PauseGame.Resume();
            else
                GameManagement.PauseGame.Pause();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
            GameManagement.RestartGame.Restart();
    }

    void FixedUpdate()
    {
        _player.velocity = _playerDirection * _playerMoveSpeed;
    }
}