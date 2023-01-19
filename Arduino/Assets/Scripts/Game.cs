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
    private UnityEngine.Video.VideoPlayer videoPlayer;
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
    private GameObject loseScreen;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject tieScreen;
    [SerializeField]
    private GameObject gameScreen;
    [SerializeField]
    private GameObject video;

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
        gameScreen.SetActive(false);
        StartCoroutine("startWait");
    }

    IEnumerator startWait()
    {
        yield return new WaitForSeconds(3.5f);
        gameScreen.SetActive(true);
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

        int currentInput = myListener.PlayerInput;

        if (isInMenu)
        {
            if (time >= 5f)
            {
                switch (lastInput)
                {
                    case 0:
                        sceneManager.OpenMainMenu();
                        break;
                    case 2:
                        isInMenu = false;
                        sceneManager.OpenGame();

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
        if (isInMenu)
        {
            Debug.Log("bruh");
            yield break;
        }
        else if (!isInMenu)
        {
            switch (rounds)
            {
                case 0:
                    videoPlayer.url = "Assets/Animations UI/Countdown 1.mp4";
                    videoPlayer.Play();
                    break;
                case 1:
                    videoPlayer.url = "Assets/Animations UI/Countdown 2.mp4";
                    videoPlayer.Play();
                    break;
                case 2:
                    videoPlayer.url = "Assets/Animations UI/Countdown 3.mp4";
                    videoPlayer.Play();
                    break;
                case 3:
                    videoPlayer.url = "Assets/Animations UI/Countdown 4.mp4";
                    videoPlayer.Play();
                    break;
                case 4:
                    videoPlayer.url = "Assets/Animations UI/Countdown 5.mp4";
                    videoPlayer.Play();
                    break;
            }
        }


        yield return new WaitForSeconds(7f);
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
            videoPlayer.url = "Assets/Animations UI/Tie round bg.mp4";
            videoPlayer.Play();
            StartCoroutine("roundEnd");
        }
        else if (compInput == 0)
        {
            switch (playInput)
            {
                case 1:
                    score[rounds].sprite = lose;
                    scoreSave[rounds] = -1;
                    rounds++;
                    videoPlayer.url = "Assets/Animations UI/PC wins round bg.mp4";
                    videoPlayer.Play();
                    StartCoroutine("roundEnd");
                    break;
                case 2:
                    score[rounds].sprite = win;
                    scoreSave[rounds] = 1;
                    rounds++;
                    videoPlayer.url = "Assets/Animations UI/Player wins rouns bg.mp4";
                    videoPlayer.Play();
                    StartCoroutine("roundEnd");
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
                    videoPlayer.url = "Assets/Animations UI/Player wins rouns bg.mp4";
                    videoPlayer.Play();
                    StartCoroutine("roundEnd");
                    break;
                case 2:
                    score[rounds].sprite = lose;
                    scoreSave[rounds] = -1;
                    rounds++;
                    videoPlayer.url = "Assets/Animations UI/PC wins round bg.mp4";
                    videoPlayer.Play();
                    StartCoroutine("roundEnd");
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
                    videoPlayer.url = "Assets/Animations UI/PC wins round bg.mp4";
                    videoPlayer.Play();
                    StartCoroutine("roundEnd");
                    break;
                case 1:
                    score[rounds].sprite = win;
                    scoreSave[rounds] = 1;
                    rounds++;
                    videoPlayer.url = "Assets/Animations UI/Player wins rouns bg.mp4";
                    videoPlayer.Play();
                    StartCoroutine("roundEnd");
                    break;
            }
        }

        Debug.Log(rounds);

        if (rounds == 5)
        {
            yield return new WaitForSeconds(2f);
            Verify();
        }
    }

    IEnumerator roundEnd()
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("timer");
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
        gameScreen.SetActive(false);
        videoPlayer.url = "Assets/Animations UI/PC wins battle.mp4";
        videoPlayer.Play();
        loseScreen.SetActive(true);
        isInMenu = true;
    }

    private void PlayWin()
    {
        Debug.Log("PLAYER WIN");
        gameScreen.SetActive(false);
        videoPlayer.url = "Assets/Animations UI/Player wins battle.mp4";
        videoPlayer.Play();
        winScreen.SetActive(true);
        isInMenu = true;
    }

    private void Tie()
    {
        Debug.Log("TIE");
        gameScreen.SetActive(false);
        videoPlayer.url = "Assets/Animations UI/Nobody wins battle.mp4";
        videoPlayer.Play();
        tieScreen.SetActive(true);
        isInMenu = true;
    }
}
