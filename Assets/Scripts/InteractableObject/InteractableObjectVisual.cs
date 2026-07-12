using Unity.VisualScripting;
using UnityEngine;

public class InteractableObjectVisual : MonoBehaviour
{
    private static readonly int OutlineWidthID = Shader.PropertyToID("_OutlineWidth");
    private static readonly int OutlineColorID = Shader.PropertyToID("_OutlineColor");
    private float outlineWidth = 1.02f;
    private Color holdedItemOutlineColor = Color.orange;
    private Color pickableItemOutlineColor = Color.white;

    [SerializeField] private Renderer meshRenderer;
    private MaterialPropertyBlock block;
    
    private void Awake()
    {
        if (TryGetComponent(out Renderer renderer))
            meshRenderer = renderer;
        else
            Debug.LogError("There's no mesh renderer on this GameObject: " + name + ". Please, insert a reference manually!");

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
