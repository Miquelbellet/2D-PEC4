using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    [SerializeField] private float logoTime;
    [SerializeField] private GameObject gameTitle;
    [SerializeField] private GameObject shovel;
    [SerializeField] private AudioClip startGameClip;

    private AudioSource titleAudioSource;
    private float time;
    void Start()
    {
        titleAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= logoTime) gameTitle.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            shovel.GetComponent<Animator>().SetTrigger("hasPressed");
            titleAudioSource.PlayOneShot(startGameClip);
            Invoke("GoToGame", 3f);
        }
    }

    void GoToGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
