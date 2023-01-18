using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{
    private int game = 1;
    private int mainMenu = 0;

    public void OpenGame()
    {
        SceneManager.LoadScene(game);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
