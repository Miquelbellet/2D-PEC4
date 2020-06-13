using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] private int checkpointNumber;

    private GameControllerScript gameController;
    private Animator checkpointAnimator;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
        checkpointAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            checkpointAnimator.SetTrigger("ActivateCheckpoint");
            gameController.SetCheckpoint(checkpointNumber);
        }
    }
}
