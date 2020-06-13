using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject checkpoints, goldBagPrefab;
    [SerializeField] private float gemJumpForce;
    [SerializeField] private GameObject[] goldTypes;

    private GameObject player;
    private int checkpointIndex, goldPlayerDead;
    private float posXPlayerDead, posYPlayerDead;

    bool followPlayer;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        checkpointIndex = PlayerPrefs.GetInt("checkpointIndex", 0);
        player.transform.position = checkpoints.transform.GetChild(checkpointIndex).transform.position;
        followPlayer = true;
        goldPlayerDead = PlayerPrefs.GetInt("goldPlayerDead", 0);
        posXPlayerDead = PlayerPrefs.GetFloat("posXPlayerDead", 0);
        posYPlayerDead = PlayerPrefs.GetFloat("posYPlayerDead", 0);
        if (goldPlayerDead > 0) SetGoldBags();
    }

    void Update()
    {
        if (followPlayer)
        {
            Camera.main.transform.position = new Vector3(player.transform.position.x, 0, -8.5f);
        }
    }

    void SetGoldBags()
    {
        GameObject bag1 = Instantiate(goldBagPrefab, new Vector2(posXPlayerDead-1f, posYPlayerDead+0.5f), Quaternion.Euler(0, 0, 0));
        bag1.GetComponent<GoldBagScript>().goldInBag = Mathf.CeilToInt(goldPlayerDead / 3);
        GameObject bag2 = Instantiate(goldBagPrefab, new Vector2(posXPlayerDead, posYPlayerDead+1f), Quaternion.Euler(0, 0, 0));
        bag2.GetComponent<GoldBagScript>().goldInBag = Mathf.CeilToInt(goldPlayerDead / 3);
        GameObject bag3 = Instantiate(goldBagPrefab, new Vector2(posXPlayerDead+1f, posYPlayerDead+0.5f), Quaternion.Euler(0, 0, 0));
        bag3.GetComponent<GoldBagScript>().goldInBag = Mathf.CeilToInt(goldPlayerDead / 3);
    }

    public void InvokeGoldType(int type, Vector2 pos)
    {
        GameObject gem = Instantiate(goldTypes[type], pos, Quaternion.Euler(0, 0, 0));
        gem.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, gemJumpForce));
    }

    public void SetCheckpoint(int index)
    {
        checkpointIndex = index;
        PlayerPrefs.SetInt("checkpointIndex", checkpointIndex);
    }
}
