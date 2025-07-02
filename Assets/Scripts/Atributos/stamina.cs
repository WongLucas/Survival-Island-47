using UnityEngine;
using UnityEngine.UI;

public class stamina : MonoBehaviour
{
    public Slider StaminaSlider;
    public float MaxStamina = 100f;
    public float CurrentStamina;
    public float StaminaRegenRate = 3f;   // quanto recupera por segundo
    public float StaminaDrainRate = 20f;   // quanto consome por segundo

    void Start()
    {
        CurrentStamina = MaxStamina;
        StaminaSlider.maxValue = MaxStamina;
        StaminaSlider.value = MaxStamina;
    }

    void Update()
    {
        // Atualiza visualmente a barra
        StaminaSlider.value = CurrentStamina;
    }

    public void DrainStamina(float amount)
    {
        CurrentStamina = Mathf.Max(CurrentStamina - amount * Time.deltaTime, 0f);
    }

   public void RegenerateStamina(float amount)
{
    float regenAmount = CurrentStamina <= 0f ? amount / 2f : amount;
    CurrentStamina = Mathf.Min(CurrentStamina + regenAmount * Time.deltaTime, MaxStamina);
}


    public bool HasStamina()
    {
        return CurrentStamina > 0f;
    }
}
