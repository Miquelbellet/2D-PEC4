using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCarbonScript : MonoBehaviour
{
    [SerializeField] private GameObject particle1Prefab, particle2Prefab;
    [SerializeField] private float particlesJumpForce;
    [SerializeField] private Sprite[] carbonStates;

    private GameControllerScript gameController;
    private SpriteRenderer carbonSprite;
    private int state;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
        carbonSprite = GetComponent<SpriteRenderer>();
        state = 4;
    }

    void Update()
    {
        
    }

    void downgradeState()
    {
        state--;
        if (state < 0) Destroy(gameObject, 0.1f);
        else carbonSprite.sprite = carbonStates[state];

        //Particles
        GameObject part1 = Instantiate(particle1Prefab, transform.position, Quaternion.Euler(0, 0, 0));
        float randomForce1 = Random.Range(particlesJumpForce/2, particlesJumpForce*2);
        part1.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomForce1 / 2, randomForce1));

        float randomForce2 = Random.Range(particlesJumpForce / 2, particlesJumpForce * 2);
        GameObject part2 = Instantiate(particle1Prefab, transform.position, Quaternion.Euler(0, 0, 0));
        part2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-randomForce2 / 2, randomForce2));

        //Gems
        int numGems = Random.Range(1, 3);
        for (int i = 0; i < numGems; i++)
        {
            gameController.InvokeGoldType(Random.Range(0, 5), new Vector2(Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f), Random.Range(transform.position.y, transform.position.y + 1)));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackR" || collision.gameObject.tag == "AttackL" || collision.gameObject.tag == "AttackJump")
        {
            downgradeState();
        }
    }
}
