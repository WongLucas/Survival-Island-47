using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class musicatransicao : MonoBehaviour
{
    public static musicatransicao instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PararMusica()
    {
        GetComponent<AudioSource>().Stop();
    }

}
