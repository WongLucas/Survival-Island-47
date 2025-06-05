using UnityEngine;

public class SunRotation : MonoBehaviour
{
    [Header("Velocidade de Rotação (graus por segundo)")]
    public float rotationSpeed = 10f;

    void Update()
    {
        // Rotaciona em torno do eixo X (ou ajuste para o eixo desejado)
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
