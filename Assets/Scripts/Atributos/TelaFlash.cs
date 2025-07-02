using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TelaFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.2f;

    private Coroutine flashRoutine;

    private void Awake()
    {
        flashImage.enabled = false; // começa invisível
    }

    public void Flash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        flashImage.enabled = true; // mostra o flash
        yield return new WaitForSeconds(flashDuration);
        flashImage.enabled = false; // esconde o flash
    }
}
