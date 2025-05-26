using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{


    private Rigidbody _rigidbody;
    private FixedJoystick _fixedJoystick;
    private Animator _animator;

    private float _playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = Player.PlayerInstance.playerRigidbody;
        _animator = Player.PlayerInstance.playerAnimator;
        _fixedJoystick = Player.PlayerInstance.joystick;

        _playerSpeed = Player.PlayerInstance.playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * _playerSpeed, _rigidbody.velocity.y, _fixedJoystick.Vertical * _playerSpeed);
    }
}
