using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Шаблон для уровня
/// </summary>
[CreateAssetMenu(fileName = "new LevelTemplate", menuName = "Create new LevelTemplate")]
public class LevelTemplate : ScriptableObject
{
    [Header("Диалог в начале уровня")]
    public Dialog introDialog;

    [Header("Имя уровня")]
    public string templateName;

    [Header("Оформление уровня")]
    public List<GameObject> background;
    public Sprite shirt;



    [Header("Размер уровня")]
    public int levelSize;

    [Header("Обязательные предметы на уровне")]
    /// <summary>
    /// Список обязательных объектов на уровне
    /// </summary>
    public List<ObjectDescription> necessaryObjects = new List<ObjectDescription>();
    [Header("Количество столбцов сетки уровня")]
    public int levelColumns;

    [Header("Номера пропускаемых ячеек")] public int[] emptyCellsArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
