using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Replica", menuName = "Dialog/create new Replica")]
public class Replica : ScriptableObject
{
    public Sprite characterSprite;
    public string[] charaterName;

    /// <summary>
    /// 0 - спрятан
    /// 1 - Затенён
    /// 2 - Показан
    /// </summary>
    public int firstCharStatus;
    /// <summary>
    /// 0 - спрятан
    /// 1 - Затенён
    /// 2 - Показан
    /// </summary>
    public int secondCharStatus;


    [TextArea(3,6)]
    public string[] replicaText;
}
