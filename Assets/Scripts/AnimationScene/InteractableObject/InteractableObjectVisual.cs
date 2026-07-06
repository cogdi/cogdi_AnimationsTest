using Unity.VisualScripting;
using UnityEngine;

public class InteractableObjectVisual : MonoBehaviour
{
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

    public void Highlight(bool isObjectHolded)
    {
        meshRenderer.GetPropertyBlock(block);
        block.SetFloat(OutlineWidthID, outlineWidth);

        if (isObjectHolded)
            block.SetColor(OutlineColorID, holdedItemOutlineColor);
        else
            block.SetColor(OutlineColorID, pickableItemOutlineColor);

        meshRenderer.SetPropertyBlock(block);
    }
}
