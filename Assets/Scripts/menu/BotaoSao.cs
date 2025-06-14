using UnityEngine;

public class BotaoSom : MonoBehaviour
{
    public AudioSource somDoClique;

    public void TocarSom()
    {
        if (somDoClique != null)
        {
            somDoClique.Play();
        }
    }
}
