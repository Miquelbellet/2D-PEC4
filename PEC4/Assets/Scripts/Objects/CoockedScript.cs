using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoockedScript : MonoBehaviour
{
    [SerializeField] private Sprite coockedSprite;
    [SerializeField] private float platajumpForce;

    private SpriteRenderer coockedSpriteRend;
    private bool isOpen;
    void Start()
    {
        coockedSpriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackR" || collision.gameObject.tag == "AttackL")
        {
            coockedSpriteRend.sprite = coockedSprite;
            isOpen = true;
            GameObject plata1 = transform.GetChild(0).gameObject;
            plata1.SetActive(true);
            plata1.GetComponent<Rigidbody2D>().AddForce(new Vector2(platajumpForce/2, platajumpForce));
            GameObject plata2 = transform.GetChild(1).gameObject;
            plata1.SetActive(true);
            plata2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-platajumpForce / 2, platajumpForce));
            Destroy(plata1, 0.5f);
            Destroy(plata2, 0.5f);
        }

        if (collision.gameObject.tag == "Player" && isOpen)
        {
            collision.GetComponent<PlayerHealthScript>().SumarVida(2);
            Destroy(gameObject);
        }
    }
}
