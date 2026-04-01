using UnityEngine;

public class SimpleWater : MonoBehaviour
{
    [Header("Settings")]
    public float waterWidth = 5f;
    public float waterLength = 5f;
    public Color waterColor = new Color(0, 0.4f, 0.7f, 0.6f);
    public Vector2 flowSpeed = new Vector2(0.05f, 0.05f);

    private Renderer waterRenderer;

    void Start()
    {
        // Creates a plane mesh automatically
        GameObject waterPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        waterPlane.name = "WaterSurface";
        waterPlane.transform.SetParent(this.transform);
        waterPlane.transform.localPosition = Vector3.zero;
        
        // Scale it to fit your gap (Planes are 10x10 units by default, so we divide by 10)
        waterPlane.transform.localScale = new Vector3(waterWidth / 10f, 1, waterLength / 10f);

        // Assign a basic transparent material
        waterRenderer = waterPlane.GetComponent<Renderer>();
        waterRenderer.material = new Material(Shader.Find("Standard"));
        
        // Setup transparency for the Standard Shader
        waterRenderer.material.SetFloat("_Mode", 3); 
        waterRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        waterRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        waterRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        waterRenderer.material.renderQueue = 3000;
        
        waterRenderer.material.color = waterColor;

        // Remove collider so we don't walk ON the water
        Destroy(waterPlane.GetComponent<MeshCollider>());
    }

    void Update()
    {
        // Animates the "texture" (even if it's just a color, it helps for later)
        if (waterRenderer != null)
        {
            Vector2 offset = Time.time * flowSpeed;
            waterRenderer.material.SetTextureOffset("_MainTex", offset);
        }
    }
}