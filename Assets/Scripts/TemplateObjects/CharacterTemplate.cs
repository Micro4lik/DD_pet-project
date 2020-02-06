using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Character", menuName = "Create new CharacterTemplate")]
public class CharacterTemplate : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    [TextArea(6, 6)]
    public string characterBiography;


    public Sprite characterAbilityIcon;

    public QuestTemplate[] Quests;


}
