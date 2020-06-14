using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] private GameObject shovels;
    [SerializeField] private AudioClip moveCursorClip, confirmClip;

    private AudioSource menuAudoiSource;
    private int index;
    void Start()
    {
        menuAudoiSource = GetComponent<AudioSource>();
        index = 0;
    }

    void Update()
    {
        MenuLogicPointers();
    }

    void MenuLogicPointers()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            shovels.transform.GetChild(index).gameObject.GetComponent<Animator>().SetTrigger("hasPressed");
            menuAudoiSource.PlayOneShot(confirmClip);
            if (index == 0) Invoke("GoToGame", 2);
            else if (index == 1) Invoke("ExitGame", 2);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            shovels.transform.GetChild(index).gameObject.SetActive(false);
            index++;
            if (index >= shovels.transform.childCount) index = 0;
            shovels.transform.GetChild(index).gameObject.SetActive(true);
            menuAudoiSource.PlayOneShot(moveCursorClip);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            shovels.transform.GetChild(index).gameObject.SetActive(false);
            index--;
            if (index < 0) index = shovels.transform.childCount - 1;
            shovels.transform.GetChild(index).gameObject.SetActive(true);
            menuAudoiSource.PlayOneShot(moveCursorClip);
        }
    }

    private void GoToGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("GameScene");
    }

    void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
