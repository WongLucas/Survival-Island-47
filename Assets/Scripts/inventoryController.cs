using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [Header("Inventory Settings")]
    public Objects[] slots;            // Slots de itens
    public Image[] slotImage;          // Imagens dos slots
    public int[] slotAmount;           // Quantidade de cada item

    [Header("Camera Settings")]
    public Camera playerCamera;        // Referência da câmera

    void Start()
    {
        // Se a câmera não estiver atribuída no Inspector, pega a MainCamera
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        // Faz um raycast do centro da tela
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, 5f))
        {
            // Se colidir com algo que tenha a tag "Object"
            if (hit.collider.CompareTag("Object"))
            {
                // Aperta E para pegar
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Pega o objeto do script objectype que está no objeto hit
                    var obj = hit.transform.GetComponent<objectype>();

                    if (obj != null)
                    {
                        // Loop nos slots
                        for (int i = 0; i < slots.Length; i++)
                        {
                            // Se slot está vazio ou tem o mesmo item (acumular)
                            if (slots[i] == null || slots[i].name == obj.ObjecType.name)
                            {
                                slots[i] = obj.ObjecType;
                                slotAmount[i]++;
                                slotImage[i].sprite = slots[i].itemSprite;
                                slotImage[i].enabled = true;

                                // Destroi o objeto da cena
                                Destroy(hit.transform.gameObject);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
