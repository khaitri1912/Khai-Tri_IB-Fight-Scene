using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player PlayerInstance;

    public CharSrciptableObject charsSO;


    public Rigidbody playerRigidbody;
    public Animator playerAnimator;

    [Header("Enemy")]
    public GameObject enemy;
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject[] enemyListTag;
    public float distanceToEnemies = 1.5f;

    [Header("Player Movement")]
    public FixedJoystick joystick;
    public Vector3 inputDirection;
    public float playerSpeed;
    public float playerRotationSpeed = 20f;

    [Header("Player States")]
    BaseStateMachine currentState;
    public PlayerIdleState playerIdleState = new PlayerIdleState();
    public PlayerWalkState playerWalkState = new PlayerWalkState();
    public PlayerAttackState playerAttackState = new PlayerAttackState();
    public PlayerVictoryState playerVictoryState = new PlayerVictoryState();

    [Header("PLayer Stats")]
    public PlayerStats playerStats = new PlayerStats();
    public Slider playerHealth_Bar;

    private void Awake()
    {
        if (PlayerInstance == null)
        {
            PlayerInstance = this;
        }

        playerSpeed = charsSO.CharactersData.charactersBaseSpeed;

        playerStats.health = charsSO.PlayerData.HP;
        playerStats.damage = charsSO.PlayerData.PlayerDamage;

        SwitchState(playerIdleState);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        Debug.Log(playerStats.health);
        playerHealth_Bar.maxValue = charsSO.PlayerData.HP;

        enemyListTag = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log("So luong enemy trong CheckClosestEnemy: " + enemyListTag.Length);

        for (int i = 0; i < enemyListTag.Length; i++)
        {
            enemyList.Add(enemyListTag[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotation();

        currentState.UpdateState(this);
        Debug.Log(currentState.ToString());

        playerHealth_Bar.value = playerStats.health;

        enemy = CheckClosestEnemy();

        foreach (GameObject e in enemyList)
        {
            if (e == null)
            {
                enemyList.Remove(e);
            }
        }

    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }


    #region Movement
    public void PlayerMovement()
    {
        playerRigidbody.velocity = new Vector3(joystick.Horizontal * playerSpeed, playerRigidbody.velocity.y, joystick.Vertical * playerSpeed);
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
            //  joystick
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

    public void PlayerTakeDamage(float damage)
    {
        if (playerStats.health <= 2)
        {
            playerAnimator.SetTrigger("defeat");
        }
        else
        {
            playerAnimator.SetTrigger("getDamage");
        }
        playerStats.TakeDamage(damage);
    }

    public GameObject CheckClosestEnemy()
    {
        if (enemyList.Count == 0 || enemyList == null)
        {
            return null;
        }

        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject e in enemyList)
        {
            if (e == null) continue;

            float distance = Vector3.Distance(e.transform.position, playerAnimator.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = e;
            }
        }
        return closestEnemy;
    }
}
