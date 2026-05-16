using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests
{
    public class InputScript : MonoBehaviour
    {
        [SerializeField] Rigidbody _player;
        [SerializeField] float _playerMoveSpeed = 3;

        Vector2 _playerDirection;

        void Update()
        {
            _playerDirection = new Vector3(
                Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameManager.PauseGame.IsPaused)
                    GameManager.PauseGame.Resume();
                else
                    GameManager.PauseGame.Pause();
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
                GameManager.RestartGame.Restart();
        }

        void FixedUpdate()
        {
            _player.velocity = new Vector3(
                _playerDirection.x * _playerMoveSpeed,
                _player.velocity.y,
                _playerDirection.y * _playerMoveSpeed);
        }
    }
}