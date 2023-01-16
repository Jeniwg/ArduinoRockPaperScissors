using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private MyListener myListener;
    private int compInput;
    private int playInput;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("timer");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 0 => Pedra
    // 1 => Tesoura
    // 2 => Papel

    // 0>1>2>0

    IEnumerator timer()
    {
        yield return new WaitForSeconds(3f);
        compInput = Random.Range(0, 2);
        Debug.Log("compInput: " + compInput);
        playInput = myListener.PlayerInput;
        Debug.Log("PlayInput: " + playInput);

        if (compInput == playInput)
        {
            //Impate
            Debug.Log("IMPATE");
            StartCoroutine("timer");
        }
        else if (compInput == 0)
        {
            switch (playInput)
            {
                case 1:
                //comp win
                CompWin();
                    break;
                case 2:
                //play win
                PlayWin();
                    break;
            }
        }
        else if (compInput == 1)
        {
            switch (playInput)
            {
                case 0:
                //play win
                PlayWin();
                    break;
                case 2:
                //comp win
                CompWin();
                    break;
            }
        }else if (compInput == 2)
        {
            switch (playInput)
            {
                case 0:
                //comp win
                CompWin();
                    break;
                case 1:
                //play win
                PlayWin();
                    break;
            }
        }
    }

    private void CompWin()
    {
        Debug.Log("COMPUTER WIN");
        StartCoroutine("timer");
    }

    private void PlayWin()
    {
        Debug.Log("PLAYER WIN");
        StartCoroutine("timer");
    }
}
