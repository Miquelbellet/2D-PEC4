using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [HideInInspector] public int health;
    private int initHealth;
    private GameObject gameController;
    private UIScript uiScript;
    void Start()
    {
        health = initHealth = 10;
        gameController = GameObject.FindWithTag("GameController");
        uiScript = gameController.GetComponent<UIScript>();
    }

    void Update()
    {
        
    }

    public void RestarVida(int vidaResta)
    {
        health -= vidaResta;
        if (health <= 0) PlayerDead();
        uiScript.UpdateHealth(health);
    }

    public void SumarVida(int vidaSuma)
    {
        health += vidaSuma;
        if (health > initHealth) health = initHealth;
        uiScript.UpdateHealth(health);
    }

    void PlayerDead()
    {
        GetComponent<PlayerControllerScript>().PlayerDead();
        Debug.Log("Player Dead");
    }
}
