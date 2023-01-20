using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private MyListener myListener;
    [SerializeField]
    private SceneManage sceneManager;
    [SerializeField]
    private GameObject howToPlayScreen;
    [SerializeField]
    private GameObject mainScreen;

    private int lastInput = 4;
    private float timer = 0;
    private bool isInHTP = false;

    // Start is called before the first frame update
    void Start()
    {
        lastInput = myListener.PlayerInput;
    }

    // Update is called once per frame
    void Update()
    {
        int currentInput = myListener.PlayerInput;

        if (!isInHTP)
        {
            if (timer >= 5f)
            {
                switch (lastInput)
                {
                    case 0:
                        UnityEditor.EditorApplication.isPlaying = false;
                        //Application.Quit(); <- for build
                        break;
                    case 1:
                        howToPlayScreen.SetActive(true);
                        mainScreen.SetActive(false);
                        isInHTP = true;
                        timer = 0;
                        break;
                    case 2:
                        sceneManager.OpenGame();
                        break;
                }
            }
            else
            {
                if (currentInput == lastInput)
                {
                    timer = timer + Time.deltaTime;
                }
                else
                {
                    lastInput = currentInput;
                    timer = 0;
                }
            }
        }
        else
        {
            if (timer >= 5f)
            {
                howToPlayScreen.SetActive(false);
                mainScreen.SetActive(true);
                isInHTP = false;
                timer = 0;
            }
            else
            {
                if (currentInput == 0)
                {
                    timer = timer + Time.deltaTime;
                }
                else
                {
                    timer = 0;
                }
            }

        }


    }
}
