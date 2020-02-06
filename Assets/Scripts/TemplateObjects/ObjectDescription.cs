using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new ObjectDecription",menuName = "Create new ObjectDecription")]
public class ObjectDescription : ScriptableObject
{
    public Sprite objectSprite;
    public string[] objectName;
    public string[] objectSpeech;
    public bool isUsable;

    public enum TileType
    {
       Monster,
       Treasure,
       NPC
    }

    public TileType tileType;
    public int attack;
    public int level;


}
