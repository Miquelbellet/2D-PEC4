using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [HideInInspector] public int gold;

    [SerializeField] private TextMeshProUGUI goldTxt, itemsTxt;
    [SerializeField] private GameObject lifes;
    [SerializeField] private Sprite fullLife, midLife, noLife;

    private GameObject player;
    private int initHeath, items;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        initHeath = player.GetComponent<PlayerHealthScript>().health;
        UpdateHealth(initHeath);
        gold = 0;
        goldTxt.text = gold.ToString();
    }

    void Update()
    {
        
    }

    public void UpdateHealth(int health)
    {
        var countHealth = 2;
        for (int i = 0; i < lifes.transform.childCount; i++)
        {
            if (health >= countHealth)
            {
                lifes.transform.GetChild(i).GetComponent<Image>().sprite = fullLife;
            }else if (health >= countHealth-1)
            {
                lifes.transform.GetChild(i).GetComponent<Image>().sprite = midLife;
            }else
            {
                lifes.transform.GetChild(i).GetComponent<Image>().sprite = noLife;
            }
            countHealth += 2;
        }
    }

    public void PlusGold(int plusGold)
    {
        gold += plusGold;
        goldTxt.text = gold.ToString();
    }

    public void restItem(int restItem)
    {
        items -= restItem;
        itemsTxt.text = items.ToString();
    }
}
