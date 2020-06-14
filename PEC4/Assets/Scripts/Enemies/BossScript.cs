using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    [HideInInspector] public bool bossDead, bossAttacking;

    [SerializeField] private float bossRangeDetection;
    [SerializeField] private float attackRange;
    [SerializeField] private float bossWalkingVelocity;
    [SerializeField] private float timeBetweenAttacks;

    private enum BossStates { Idle, Walk, Attack, Protect };
    private BossStates currentState;

    private GameObject gameController;
    private GameObject player;
    private Animator bossAnimator;
    private SpriteRenderer bossSprite;
    private Rigidbody2D bossRigidBody;
    private BoxCollider2D bossCollider;
    private float distance, time;
    private int health;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        player = GameObject.FindWithTag("Player");
        bossAnimator = GetComponent<Animator>();
        bossSprite = GetComponent<SpriteRenderer>();
        bossCollider = GetComponent<BoxCollider2D>();
        bossRigidBody = GetComponent<Rigidbody2D>();
        health = 12;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if(!bossDead) BossStateMachineBehaviour();
        if (bossAttacking) transform.GetChild(0).gameObject.SetActive(true);
        else transform.GetChild(0).gameObject.SetActive(false);
    }

    private void BossStateMachineBehaviour()
    {
        switch (currentState)
        {
            case BossStates.Idle:
                WaitingForPlayer();
                break;
            case BossStates.Walk:
                Walking();
                break;
            case BossStates.Attack:
                AttackPlayer();
                break;
            case BossStates.Protect:
                Protect();
                break;
            default:
                currentState = BossStates.Idle;
                break;
        }
    }
    private void ChangeState(BossStates newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
    private void WaitingForPlayer()
    {
        if (distance <= bossRangeDetection)
        {
            gameController.GetComponent<UIScript>().UpdateHealthBoss(health);
            ChangeState(BossStates.Walk);
        }
    }
    private void Walking()
    {
        if (distance > bossRangeDetection) ChangeState(BossStates.Idle);

        bossAnimator.SetBool("Walk", true);
        if (transform.position.x > player.transform.position.x)
        {
            bossSprite.flipX = true;
            transform.position = new Vector2(transform.position.x - (bossWalkingVelocity * Time.deltaTime), transform.position.y);
        }
        else if (transform.position.x < player.transform.position.x)
        {
            bossSprite.flipX = false;
            transform.position = new Vector2(transform.position.x + (bossWalkingVelocity * Time.deltaTime), transform.position.y);
        }
        
        if (distance <= attackRange) ChangeState(BossStates.Attack);
    }
    private void AttackPlayer()
    {
        if (distance > bossRangeDetection) ChangeState(BossStates.Idle);
        else if (player.transform.position.y > transform.position.y + 1) ChangeState(BossStates.Protect);

        float distX = player.transform.position.x - transform.position.x;
        if (distX < 0) distX *= -1;
        if (distX > attackRange) ChangeState(BossStates.Walk);

        string clipName = "";
        try { clipName = bossAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name; }
        catch { clipName = ""; }

        bossAnimator.SetBool("Walk", false);
        time += Time.deltaTime;
        if (clipName != "Attack" && time >= timeBetweenAttacks)
        {
            bossAnimator.SetTrigger("Attack");
            bossAttacking = true;
            time = 0;
        }
        else if (time > 1)
        {
            bossAttacking = false;
        }

        if (transform.position.x > player.transform.position.x)
        {
            bossSprite.flipX = true;
        }
        else if (transform.position.x < player.transform.position.x)
        {
            bossSprite.flipX = false;
        }
    }
    private void Protect()
    {
        if (distance > bossRangeDetection) ChangeState(BossStates.Idle);

        bossAnimator.SetBool("ProtectUp", true);
        if (distance <= attackRange && player.transform.position.y <= transform.position.y + 1)
        {
            bossAnimator.SetBool("ProtectUp", false);
            ChangeState(BossStates.Attack);
        }
    }
    private void BossDead()
    {
        bossDead = true;
        bossAnimator.SetTrigger("Dead");
        bossRigidBody.bodyType = RigidbodyType2D.Kinematic;
        bossCollider.isTrigger = true;
        gameController.GetComponent<UIScript>().LevelPassed();
        player.GetComponent<Animator>().SetTrigger("Victory");
        Invoke("goToCredits", 5);
    }
    private void goToCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackR" || collision.gameObject.tag == "AttackL" || collision.gameObject.tag == "AttackJump")
        {
            if (player.transform.position.x < transform.position.x) bossRigidBody.AddForce(new Vector2(300, 0));
            else bossRigidBody.AddForce(new Vector2(-300, 0));
            health--;
            if (health <= 0) BossDead();
            gameController.GetComponent<UIScript>().UpdateHealthBoss(health);
        }
    }
}
