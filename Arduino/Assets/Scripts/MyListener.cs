using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListener : MonoBehaviour
{
    private int fing1 = 0;
    private int fing2 = 0;

    public int PlayerInput = 4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {        
        //Verificar Pedra
        if(fing1 <= 2 && fing2 > 0)
        {
            Debug.Log("PEDRA");
            PlayerInput = 0;
        }
        //Verificar Tesoura
        if(fing1 > 2 && fing2 > 0)
        {
            Debug.Log("TESOURA");
            PlayerInput = 1;
        }
        //Verificar Papel
        if(fing1 > 2 && fing2 == 0)
        {
            Debug.Log("PAPEL");
            PlayerInput = 2;
        }
    }

    void OnMessageArrived(string msg)
    {
        string[] elements = msg.Split(' ');
        fing1 = int.Parse(elements[0]);
        fing2 = int.Parse(elements[1]);

        //Debug.Log("Arrived: " + fing1);
    }

    void OnConnetionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnectd");
    }
}
