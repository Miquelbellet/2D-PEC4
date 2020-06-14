using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugScript : MonoBehaviour
{
    [HideInInspector] public bool dead;

    [SerializeField] private GameObject poofPrefab;
    [SerializeField] private float speedMovement;
    [SerializeField] private float forceJump;
    [SerializeField] private float timeRunning;

    private GameControllerScript GameController;
    private Rigidbody2D rbBug;
    private Collider2D colliderBug;
    private Animator animatorBug;
    private SpriteRenderer spriteBug;
    private float time;
    void Start()
    {
        GameController = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
        rbBug = GetComponent<Rigidbody2D>();
        colliderBug = GetComponent<Collider2D>();
        animatorBug = GetComponent<Animator>();
        spriteBug = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!dead) BugMovement();
    }

    void BugMovement()
    {
        time += Time.deltaTime;
        if (time < timeRunning)
        {
            transform.position = new Vector2(transform.position.x + (speedMovement * Time.deltaTime), transform.position.y);
            spriteBug.flipX = false;

        }
        else if (time > timeRunning && time < timeRunning * 2)
        {
            transform.position = new Vector2(transform.position.x - (speedMovement * Time.deltaTime), transform.position.y);
            spriteBug.flipX = true;
        }
        else time = 0;
    }

    void BugDead(bool hitRight)
    {
        colliderBug.isTrigger = true;
        if (hitRight) rbBug.AddForce(new Vector2(forceJump/2, forceJump));
        else rbBug.AddForce(new Vector2(-forceJump / 2, forceJump));
        animatorBug.SetBool("Dead", true);
        dead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && dead)
        {
            GameObject poof = Instantiate(poofPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(poof, 1);
            GameController.InvokeGoldType(Random.Range(0, 3), transform.position);
            GameController.GetComponent<SoundEffectsScript>().EnemieDieSound();
            Destroy(gameObject);
        }

        if ((collision.gameObject.tag == "AttackR" || collision.gameObject.tag == "AttackL" || collision.gameObject.tag == "AttackJump") && !dead)
        {
            var hitRight = true;
            if (transform.position.x < collision.transform.position.x) hitRight = false;
            BugDead(hitRight);
        }
    }
}
