using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    [SerializeField] private float logoTime;
    [SerializeField] private GameObject gameTitle;
    [SerializeField] private GameObject shovel;

    private float time;
    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= logoTime) gameTitle.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            shovel.GetComponent<Animator>().SetTrigger("hasPressed");
            Invoke("GoToGame", 3f);
        }
    }

    void GoToGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
