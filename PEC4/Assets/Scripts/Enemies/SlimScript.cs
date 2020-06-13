using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimScript : MonoBehaviour
{
    [HideInInspector] public bool dead;

    [SerializeField] private float jumpTimer;
    [SerializeField] private float playerFoundDistance;
    [SerializeField] private float slimJumpForce;

    private GameObject player, gameController;
    private Animator slimAnimator;
    private BoxCollider2D slimCollider;
    private Rigidbody2D slimRigidBody;
    private float time;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        player = GameObject.FindWithTag("Player");
        slimRigidBody = GetComponent<Rigidbody2D>();
        slimCollider = GetComponent<BoxCollider2D>();
        slimAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < playerFoundDistance)
        {
            time += Time.deltaTime;
            if (time >= jumpTimer)
            {
                time = 0;
                slimAnimator.SetTrigger("SlimJump");
                if (transform.position.x > player.transform.position.x)
                {
                    slimRigidBody.AddForce(new Vector2(-slimJumpForce/2, slimJumpForce));
                }
                else
                {
                    slimRigidBody.AddForce(new Vector2(slimJumpForce / 2, slimJumpForce));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackR" || collision.gameObject.tag == "AttackL" || collision.gameObject.tag == "AttackJump")
        {
            dead = true;
            slimAnimator.SetTrigger("Dead");
            slimRigidBody.bodyType = RigidbodyType2D.Kinematic;
            slimCollider.isTrigger = true;
            gameController.GetComponent<GameControllerScript>().InvokeGoldType(Random.Range(0, 3), transform.position);
            Destroy(gameObject, 0.2f);
        }
    }
}
