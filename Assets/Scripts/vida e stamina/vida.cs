using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vida : MonoBehaviour
{

    public Slider Vida;
    void Start()
    {
        Vida.value = 100f;
    }
    
    void Update()
    {
        if (Vida.value <= 0)

            Destroy(gameObject);       
    }
    public void receberDano(float dano)
    {
        Vida.value -= dano;

    }

}
