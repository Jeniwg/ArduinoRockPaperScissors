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

    //Tempo do video vs
    //Ativa ecrã de jogo e começa o count down;
    IEnumerator startWait()
    {
        yield return new WaitForSeconds(3.5f);
        gameScreen.SetActive(true);
        StartCoroutine("timer");
    }

    // Update is called once per frame
    
    void Update()
    {
        //muda o preview dependendo do nosso input
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
        
        //Caso tiver nalgum tipo de ecrã (win lose tie) para ler o input de 5 sec
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
        //Se tiver no menu não entrar em countdown
        if (isInMenu)
        {
            yield break;
        }
        else if (!isInMenu) // dependendo da round mostra videos diferentes
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

        //Dependendo do output do pc a mão dele muda
        compInput = Random.Range(0, 2);

        switch (compInput)
        {
            case 0:
            //rck anti
            break;
            case 1: 
            //scss anti
            break;
            case 2:
            //pp anti
            break;
        }

        //player anti 

        //tempo de espera para countdown/video passar
        yield return new WaitForSeconds(7f);

        //INPUT FINAL -> mostrar as mãos do input
        playInput = myListener.PlayerInput;

        switch (playInput)
        {
            case 0:
            //show rock
            break;
            case 1:
            //show scss
            break;
            case 2:
            //show pp
            break;
        }

        switch (compInput)
        {
            case 0:
            //show rock
            break;
            case 1:
            //show scss
            break;
            case 2:
            //show pp
            break;
        }

        //Verificar resultados
        //Marcar resultado e mostrar video 
        //Coroutine para dar tempo do video acabar antes da próxima round
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

        //Quando as Rounds acabar 
        if (rounds == 5)
        {
            //tempo para ver ultima play
            yield return new WaitForSeconds(2f);
            Verify();
        }
    }

    //Coroutine para dar tempo do video acabar e começar próxima round
    IEnumerator roundEnd()
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("timer");
    }

    //verificar resultado
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

        //O maior dos 3
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

    //ecra lose
    private void CompWin()
    {
        Debug.Log("COMPUTER WIN");
        gameScreen.SetActive(false);
        videoPlayer.url = "Assets/Animations UI/PC wins battle.mp4";
        videoPlayer.Play();
        loseScreen.SetActive(true);
        isInMenu = true;
    }

    //ecra win
    private void PlayWin()
    {
        Debug.Log("PLAYER WIN");
        gameScreen.SetActive(false);
        videoPlayer.url = "Assets/Animations UI/Player wins battle.mp4";
        videoPlayer.Play();
        winScreen.SetActive(true);
        isInMenu = true;
    }

    //ecra tie
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
