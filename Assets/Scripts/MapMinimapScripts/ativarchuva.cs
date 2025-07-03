using UnityEngine;
using System.Collections;

public class ativarchuva : MonoBehaviour
{
    public GameObject chuvaParticulas;
    public float duracaoChuva = 60f;   // Tempo que a chuva fica ativa (em segundos)
    public float duracaoSeca = 60f;    // Tempo que a chuva fica desativada (em segundos)

    void Start()
    {
        if (chuvaParticulas != null)
        {
            chuvaParticulas.SetActive(false); // começa desativada
            StartCoroutine(ControlarChuva());
        }
        else
        {
            Debug.LogError("Chuva Partículas não atribuída no Inspector!");
        }
    }

    IEnumerator ControlarChuva()
    {
        while (true)
        {
            chuvaParticulas.SetActive(true);
            yield return new WaitForSeconds(duracaoChuva);

            chuvaParticulas.SetActive(false);
            yield return new WaitForSeconds(duracaoSeca);
        }
    }
}
