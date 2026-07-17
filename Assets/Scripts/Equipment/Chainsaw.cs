using UnityEngine;

public class Chainsaw : PickableObject
{
    public override Tool ToolType { get => toolType; }
    private Tool toolType = Tool.Chainsaw;
}
