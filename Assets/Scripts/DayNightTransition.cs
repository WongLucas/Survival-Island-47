using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    [Header("Directional Light que irá rotacionar")]
    public Transform targetLight; // 🔥 Referência à luz que será rotacionada

    [Header("Configurações da rotação")]
    public float rotationSpeed = 90f; // Velocidade da rotação

    private bool isRotating = false;
    private float rotatedAngle = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isRotating)
        {
            isRotating = true;
            rotatedAngle = 0f;
        }

        if (isRotating && targetLight != null)
        {
            float step = rotationSpeed * Time.deltaTime;
            targetLight.Rotate(Vector3.right, step);
            rotatedAngle += step;

            if (rotatedAngle >= 180f)
            {
                float overshoot = rotatedAngle - 180f;
                targetLight.Rotate(Vector3.right, -overshoot);
                isRotating = false;
            }
        }
    }
}
