using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBagScript : MonoBehaviour
{
    [HideInInspector] public int goldInBag;

    private GameObject gameController;
    private float time, timeFlying, velocityFlying;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        timeFlying = 1f;
        velocityFlying = 0.5f;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time <= timeFlying)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (velocityFlying * Time.deltaTime));
        }
        else if (time >= timeFlying && time <= timeFlying * 2)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (velocityFlying * Time.deltaTime));
        }
        else time = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameController.GetComponent<UIScript>().PlusGold(goldInBag);
            Destroy(gameObject);
        }
    }
}
