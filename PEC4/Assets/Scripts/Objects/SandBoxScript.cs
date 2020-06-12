using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBoxScript : MonoBehaviour
{
    private GameControllerScript gameController;
    private Animator boxAnimator;
    private Collider2D boxCollider;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
        boxAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackR" || collision.gameObject.tag == "AttackL" || collision.gameObject.tag == "AttackJump")
        {
            if(collision.gameObject.tag == "AttackJump") collision.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
            gameController.InvokeGoldType(Random.Range(0, 3), transform.position);
            boxCollider.isTrigger = true;
            boxAnimator.SetTrigger("DestroyBox");
            Destroy(gameObject, 0.2f);
        }
    }
}
