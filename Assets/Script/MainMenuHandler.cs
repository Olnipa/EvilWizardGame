using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private MainMenuHandler _mainMenu;

    private int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && _mainMenu.gameObject.activeSelf == false)
        {
            _mainMenu.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && _mainMenu.gameObject.activeSelf == true)
        {
            _mainMenu.gameObject.SetActive(false);
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ContinueButton()
    {
        _mainMenu.gameObject.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
