using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject shovels, optionsPanel;

    private bool optionsOpen;
    private int index;
    void Start()
    {
        optionsPanel.SetActive(false);
        index = 0;
    }

    void Update()
    {
        if(!optionsOpen) MenuLogicPointers();
    }

    void MenuLogicPointers()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            shovels.transform.GetChild(index).gameObject.GetComponent<Animator>().SetTrigger("hasPressed");
            if (index == 0) Invoke("GoToGame", 2);
            else if (index == 1) Invoke("OpenOptions", 2);
            else if (index == 2) Invoke("ExitGame", 2);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            shovels.transform.GetChild(index).gameObject.SetActive(false);
            index++;
            if (index >= shovels.transform.childCount) index = 0;
            shovels.transform.GetChild(index).gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            shovels.transform.GetChild(index).gameObject.SetActive(false);
            index--;
            if (index < 0) index = shovels.transform.childCount - 1;
            shovels.transform.GetChild(index).gameObject.SetActive(true);
        }
    }
    void GoToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void OpenOptions()
    {
        optionsPanel.SetActive(true);
        optionsOpen = true;
    }

    void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void CloseOptions()
    {
        shovels.transform.GetChild(index).gameObject.GetComponent<Animator>().SetTrigger("return");
        optionsPanel.SetActive(false);
        optionsOpen = false;
    }
}
