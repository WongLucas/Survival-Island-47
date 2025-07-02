using UnityEngine;

public class dano : MonoBehaviour
{
    public int Dano = 10;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se colidiu com o jogador
        vida vidaJogador = collision.gameObject.GetComponent<vida>();
        if (vidaJogador != null)
        {
            vidaJogador.receberDano(Dano);
            Debug.Log("Dano aplicado ao jogador!");
        }
    }
}
