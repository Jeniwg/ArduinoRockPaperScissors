using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    private MyListener myListener;
    [SerializeField]
    private SceneManage sceneManager;
    [SerializeField]
    private Animator anim;
    private int compInput;
    private int playInput;

    //To Visualize your moves
    [SerializeField]
    private Image hand;
    [SerializeField]
    private Sprite scss;
    [SerializeField]
    private Sprite pp;
    [SerializeField]
    private Sprite rck;

    //Screens
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject loseScreen;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject tieScreen;

    //UI Score
    [SerializeField]
    private Image[] score;
    [SerializeField]
    private Sprite lose;
    [SerializeField]
    private Sprite win;
    [SerializeField]
    private Sprite tie;

    [SerializeField]
    private int rounds = 0;
    private int[] scoreSave = new int[5];
    private bool isInMenu = false;
    private int lastInput = 4;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lastInput = myListener.PlayerInput;
        StartCoroutine("timer");
    }

    // Update is called once per frame
    void Update()
    {
        if (myListener.PlayerInput == 0)
        {
            hand.sprite = rck;
        }

        if (myListener.PlayerInput == 1)
        {
            hand.sprite = scss;
        }

        if (myListener.PlayerInput == 2)
        {
            hand.sprite = pp;
        }

        if (Input.GetKey(KeyCode.Escape) && !isInMenu)
        {
            pauseScreen.SetActive(true);
            isInMenu = true;
        }
        else if (pauseScreen.activeSelf && !isInMenu)
        {
            isInMenu = true;
        }

        int currentInput = myListener.PlayerInput;

        if (isInMenu)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                isInMenu = false;
                pauseScreen.SetActive(false);
            }

            if (time >= 5f)
            {
                switch (lastInput)
                {
                    case 0:
                        sceneManager.OpenMainMenu();
                        break;
                    case 2:
                        if (pauseScreen.activeSelf)
                        {
                            isInMenu = false;
                            pauseScreen.SetActive(false);
                            time = 0;
                            StartCoroutine("timer");
                        }
                        else
                        {
                            isInMenu = false;
                            sceneManager.OpenGame();
                        }
                        break;
                }
            }
            else
            {

                if (currentInput == lastInput)
                {
                    time = time + Time.deltaTime;
                    
                }
                else
                {
                    lastInput = currentInput;
                    time = 0;
                }
            }
        }
    }

    // 0 => Pedra
    // 1 => Tesoura
    // 2 => Papel

    // 0>1>2>0

    IEnumerator timer()
    {
        if(isInMenu)
        {
            Debug.Log("bruh");
            yield break;
        }
        else
        {
          anim.SetTrigger("CountDown");  
        }
        

        yield return new WaitForSeconds(3f);
        compInput = Random.Range(0, 2);
        Debug.Log("compInput: " + compInput);
        playInput = myListener.PlayerInput;
        Debug.Log("PlayInput: " + playInput);

        Debug.Log(rounds);
        if (compInput == playInput)
        {
            Debug.Log("IMPATE");
            score[rounds].sprite = tie;
            scoreSave[rounds] = 0;
            rounds++;
            StartCoroutine("timer");
        }
        else if (compInput == 0)
        {
            switch (playInput)
            {
                case 1:
                    score[rounds].sprite = lose;
                    scoreSave[rounds] = -1;
                    rounds++;
                    StartCoroutine("timer");
                    break;
                case 2:
                    score[rounds].sprite = win;
                    scoreSave[rounds] = 1;
                    rounds++;
                    StartCoroutine("timer");
                    break;
            }
        }
        else if (compInput == 1)
        {
            switch (playInput)
            {
                case 0:
                    score[rounds].sprite = win;
                    scoreSave[rounds] = 1;
                    rounds++;
                    StartCoroutine("timer");
                    break;
                case 2:
                    score[rounds].sprite = lose;
                    scoreSave[rounds] = -1;
                    rounds++;
                    StartCoroutine("timer");
                    break;
            }
        }
        else if (compInput == 2)
        {
            switch (playInput)
            {
                case 0:
                    score[rounds].sprite = lose;
                    scoreSave[rounds] = -1;
                    rounds++;
                    StartCoroutine("timer");
                    break;
                case 1:
                    score[rounds].sprite = win;
                    scoreSave[rounds] = 1;
                    rounds++;
                    StartCoroutine("timer");
                    break;
            }
        }

        Debug.Log(rounds);

        if (rounds == 5)
        {
            Verify();
        }
    }

    private void Verify()
    {
        int zero = 0;
        int one = 0;
        int minusOne = 0;

        foreach (int score in scoreSave)
        {
            switch (score)
            {
                case -1:
                    minusOne++;
                    break;
                case 0:
                    zero++;
                    break;
                case 1:
                    one++;
                    break;
            }
        }

        string max = zero >= one && zero >= minusOne ? "zero" : one >= zero && one >= minusOne ? "one" : "minusOne";

        switch (max)
        {
            case "minusOne":
                CompWin();
                break;
            case "zero":
                Tie();
                break;
            case "one":
                PlayWin();
                break;
        }
    }

    private void CompWin()
    {
        Debug.Log("COMPUTER WIN");
        loseScreen.SetActive(true);
        isInMenu = true;
    }

    private void PlayWin()
    {
        Debug.Log("PLAYER WIN");
        winScreen.SetActive(true);
        isInMenu = true;
    }

    private void Tie()
    {
        Debug.Log("TIE");
        tieScreen.SetActive(true);
        isInMenu = true;
    }
}
