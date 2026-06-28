using UnityEngine;

public class InteractableObjectVisual : MonoBehaviour
{
    [SerializeField] private Material material;
    private static readonly int OutlineWidthID = Shader.PropertyToID("_OutlineWidth");
    private static readonly int OutlineColorID = Shader.PropertyToID("_OutlineColor");
    private float outlineWidth = 1.05f;
    private Color holdedItemOutlineColor = Color.orange;
    private Color pickableItemOutlineColor = Color.green;

    private Renderer meshRenderer;
    private MaterialPropertyBlock block;
    

    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
    }

    public void RemoveHighlight()
    {
        meshRenderer.GetPropertyBlock(block);
        block.SetFloat(OutlineWidthID, 0f);
        meshRenderer.SetPropertyBlock(block);
    }

    public void HighlightPickableObject()
    {
        meshRenderer.GetPropertyBlock(block);
        
        block.SetFloat(OutlineWidthID, outlineWidth);
        block.SetColor(OutlineColorID, pickableItemOutlineColor);

        meshRenderer.SetPropertyBlock(block);
    }

    public void HighlightObjectHolded()
    {
        meshRenderer.GetPropertyBlock(block);

        block.SetFloat(OutlineWidthID, outlineWidth);
        block.SetColor(OutlineColorID, holdedItemOutlineColor);

        meshRenderer.SetPropertyBlock(block);
    }
}
