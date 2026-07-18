using UnityEngine;

public class Figure : PickableObject
{
    public override Tool ToolType { get => toolType; }
    private Tool toolType = Tool.None;
}