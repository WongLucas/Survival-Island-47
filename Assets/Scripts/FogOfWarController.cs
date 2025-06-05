using UnityEngine;

public class FogOfWarController : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;                // Referência ao player
    public Camera mapCamera;                // Câmera que gera o mapa
    public RenderTexture fogTexture;        // Textura da névoa (fog mask)
    public Material fogMaterial;            // Material que usa o shader da névoa

    [Header("Configurações do Mapa")]
    public float worldSize = 200f;          // Tamanho total do mundo no plano XZ
    public float revealRadius = 5f;         // Raio de revelação em unidades do mundo

    // Internos
    private Texture2D fogTex;
    private Color32[] clearColors;
    private Rect textureRect;

    private Vector3 mapCenter;
    private float halfWorldSize;

    void Start()
    {
        // Inicializa o centro e tamanho
        mapCenter = mapCamera.transform.position;
        halfWorldSize = worldSize / 2f;

        // Cria a textura da máscara da névoa
        fogTex = new Texture2D(fogTexture.width, fogTexture.height, TextureFormat.ARGB32, false);
        clearColors = new Color32[fogTex.width * fogTex.height];

        for (int i = 0; i < clearColors.Length; i++)
            clearColors[i] = new Color32(0, 0, 0, 255); // Preto = coberto

        fogTex.SetPixels32(clearColors);
        fogTex.Apply();

        textureRect = new Rect(0, 0, fogTex.width, fogTex.height);

        // Inicializa o RenderTexture com tudo coberto
        Graphics.Blit(fogTex, fogTexture);
    }

    void Update()
    {
        if (player != null)
        {
            Reveal(player.position);
        }
    }

    void Reveal(Vector3 worldPos)
    {
        // Converte posição do mundo para UV (0 a 1)
        float uvX = Mathf.InverseLerp(mapCenter.x - halfWorldSize, mapCenter.x + halfWorldSize, worldPos.x);
        float uvY = Mathf.InverseLerp(mapCenter.z - halfWorldSize, mapCenter.z + halfWorldSize, worldPos.z);

        // Converte UV para coordenadas da textura
        float relativeX = uvX * fogTex.width;
        float relativeY = uvY * fogTex.height;

        // Raio em pixels
        int radius = Mathf.RoundToInt((revealRadius / worldSize) * fogTex.width);

        // Percorre e revela os pixels
        for (int y = -radius; y <= radius; y++)
        {
            int ny = (int)relativeY + y;
            if (ny < 0 || ny >= fogTex.height) continue;

            for (int x = -radius; x <= radius; x++)
            {
                int nx = (int)relativeX + x;
                if (nx < 0 || nx >= fogTex.width) continue;

                if (x * x + y * y <= radius * radius)
                {
                    fogTex.SetPixel(nx, ny, Color.white); // Branco = revelado
                }
            }
        }

        fogTex.Apply();
        Graphics.Blit(fogTex, fogTexture);
    }
}
