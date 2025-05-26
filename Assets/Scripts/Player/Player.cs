using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    public static Player PlayerInstance;

    public CharSrciptableObject charsSO;

    public Rigidbody playerRigidbody;
    public Animator playerAnimator;
    public FixedJoystick joystick;

    public Vector3 inputDirection;
    public float playerSpeed;
    public float playerRotationSpeed = 20f;

    BaseStateMachine currentState;
    public PlayerIdleState playerIdleState = new PlayerIdleState();
    public PlayerWalkState playerWalkState = new PlayerWalkState();
    public PlayerAttackState playerAttackState = new PlayerAttackState();

    private void Awake()
    {
        if (PlayerInstance == null)
        {
            PlayerInstance = this;
        }


        playerSpeed = charsSO.CharactersData.charactersBaseSpeed;

        SwitchState(playerIdleState);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotation();

        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }


    #region Movement
    public void PlayerMovement()
    {
        playerRigidbody.velocity = new Vector3(joystick.Horizontal * playerSpeed, playerRigidbody.velocity.y, joystick.Vertical * playerSpeed);

        /*if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            *//*transform.rotation = Quaternion.LookRotation(playerRigidbody.velocity);*//*
            playerAnimator.SetBool("isWalking", true);
        }
        else
            playerAnimator.SetBool("isWalking", false);*/  
    }

    public void PlayerRotation()
    {
        inputDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        float magnitude = inputDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);
        inputDirection.Normalize();


        transform.Translate(inputDirection * magnitude * playerSpeed * Time.deltaTime, Space.World);

        if (inputDirection.magnitude > 0.1f)
        {
            // Xoay theo hướng joystick
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, playerRotationSpeed * Time.deltaTime);
        }
    }
    #endregion

    public void SwitchState(BaseStateMachine state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
