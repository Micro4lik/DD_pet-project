using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Quest", menuName = "Create new QuestTemplate")]
public class QuestTemplate : ScriptableObject
{
    public string QuestName;
    [TextArea(6, 6)]
    public string questDescription;
    
    public Sprite[] questIcons;

    [Header("Набор уровней")]
    public List<LevelTemplate> levelLibrary;

}
