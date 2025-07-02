using UnityEngine;
using UnityEngine.UI;        // Necessário para usar Slider (barra de vida)
using System.Collections;    // Necessário caso use Coroutine em algum momento

public class vida : MonoBehaviour
{
    public Slider Vida;
    private TelaFlash telaFlash;

    void Start()
    {
        Vida.value = 100f;
        telaFlash = FindObjectOfType<TelaFlash>();
    }

    void Update()
    {
        if (Vida.value <= 0)
            Destroy(gameObject);
    }

    public void receberDano(float dano)
    {
        Vida.value -= dano;

        if (telaFlash != null)
        {
            telaFlash.Flash();
        }
    }
}
