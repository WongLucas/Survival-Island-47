using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transicao : MonoBehaviour
{
    // Variável para guardar o nome da cena que será carregada.
    private string cenaParaCarregar;

    // A função pública que o seu botão vai chamar.
       public void LoadScene(string name)
    {
        // 1. Guardamos o nome da cena que recebemos como parâmetro.
        this.cenaParaCarregar = name;

        //Agendamos a função "CarregarCenaAposEspera" para ser chamada
        //    daqui a 0.5 segundos.
        Invoke("CarregarCenaAposEspera", 0.5f);
    }

    // Esta é a nova função, que só será chamada pelo Invoke após o tempo de espera.
    private void CarregarCenaAposEspera()
    {
      // 3. Aqui, usamos o SceneManager para carregar a cena que foi guardada. 
        SceneManager.LoadScene(this.cenaParaCarregar);
    }

    public void sairdojogo()
    {
        Application.Quit();
        Debug.Log("Saindo do jogo");
    }
}