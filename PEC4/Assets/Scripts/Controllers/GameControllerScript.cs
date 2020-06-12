using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private float gemJumpForce;
    [SerializeField] private GameObject[] goldTypes;

    private GameObject player;

    bool followPlayer;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        followPlayer = true;
    }

    void Update()
    {
        if (followPlayer)
        {
            Camera.main.transform.position = new Vector3(player.transform.position.x, 0, -10);
        }
    }

    public void InvokeGoldType(int type, Vector2 pos)
    {
        GameObject gem = Instantiate(goldTypes[type], pos, Quaternion.Euler(0, 0, 0));
        gem.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, gemJumpForce));
    }
}
