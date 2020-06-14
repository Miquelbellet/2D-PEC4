using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [HideInInspector] public int gold;

    [SerializeField] private TextMeshProUGUI goldTxt, itemsTxt;
    [SerializeField] private GameObject lifes, bossLifes, endBlackScreen;
    [SerializeField] private Sprite fullLife, midLife, fullLifeBoss, midLifeBoss, noLife;

    private GameObject player;
    private int initHeath, items;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        initHeath = player.GetComponent<PlayerHealthScript>().health;
        UpdateHealth(initHeath);
        //UpdateHealthBoss(12);
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

    public void UpdateHealthBoss(int health)
    {
        var countHealth = 2;
        for (int i = 0; i < bossLifes.transform.childCount; i++)
        {
            if (health >= countHealth)
            {
                bossLifes.transform.GetChild(i).GetComponent<Image>().sprite = fullLifeBoss;
            }
            else if (health >= countHealth - 1)
            {
                bossLifes.transform.GetChild(i).GetComponent<Image>().sprite = midLifeBoss;
            }
            else
            {
                bossLifes.transform.GetChild(i).GetComponent<Image>().sprite = noLife;
            }
            countHealth += 2;
        }
    }

    public void PlusGold(int plusGold)
    {
        gold += plusGold;
        goldTxt.text = gold.ToString();
        GetComponent<SoundEffectsScript>().CoinSound();
    }

    public void restItem(int restItem)
    {
        items -= restItem;
        itemsTxt.text = items.ToString();
    }
    public void LevelPassed()
    {
        endBlackScreen.SetActive(true);
    }
}
