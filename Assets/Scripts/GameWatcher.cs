using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Следит за игрой, хранит её состояние и некоторые действия игрока.
/// </summary>
public class GameWatcher : MonoBehaviour
{
    const float ANIMATION_DELAY_OPEN = 0.33f;
    const float ANIMATION_DELAY_SHOWING = 0.5f;
    const float ANIMATION_COLLECT_ITEM = 0.5f;

    
    private const float ANIMATION_DELAY_FADE_OUT = 0.33f;
    public GameObject _tileBuffer1 = null;
    public GameObject _tileBuffer2 = null;
    private bool _pickingBlocked = false;
    public List<GameObject> levelTiles;
    [Header("Квест")]
    public QuestTemplate quest;
    [Header("Колода игрока")]
    public List<ObjectDescription> playerDeck;


    public Transform inventoryParent;

    public int currentLevel = 0;
    public bool levelInProgress = false;

    public static GameWatcher instance;

    private int _tilesRemain;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //LevelGenerator.instance.StartLevel(quest.levelLibrary[currentLevel]);
        Debug.Log("Starting game");
        NextLevel();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartLevel()
    {
        
    }


    /// <summary>
    /// Функция для запоминания, какой тайл был выбран
    /// </summary>
    /// <param name="_tile">Выбранный тайл</param>
    public void OpenTile(GameObject _tile)
    {
        if (_tileBuffer1 == null)
        {
            _tileBuffer1 = _tile;
            Debug.Log("Первым открыт тайл с именем: "+ _tileBuffer1.GetComponent<Tile>().GetObjectDescription().objectName[0]);
            //Debug.Log(_tileBuffer1.GetComponent<Tile>().GetTileType());
            if (_tileBuffer1.GetComponent<Tile>().GetObjectDescription().objectName[0] == "Upset Orc")
            {
                _tileBuffer1.GetComponent<Tile>().SetColorParticles();
            }
        }
        else if (_tileBuffer2 == null)
        {
            _tileBuffer2 = _tile;
            Debug.Log("Вторым открыт тайл с именем: " + _tileBuffer2.GetComponent<Tile>().GetObjectDescription().objectName[0]);
            //Debug.Log(_tileBuffer1.GetComponent<Tile>().GetTileType());
            if (_tileBuffer2.GetComponent<Tile>().GetObjectDescription().objectName[0] == "Upset Orc")
            {
                _tileBuffer2.GetComponent<Tile>().SetColorParticles();
            }

            CheckPair();
        }
    }

    /// <summary>
    /// Запускает корутину проверки пар
    /// </summary>
    public void CheckPair()
    {
        StartCoroutine(CheckPairHandler());
    }

    /// <summary>
    /// Корутина обработки нажатия на пары
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckPairHandler()
    {
        BlockPicking(true);
        Debug.Log("Проверка на пары");
        if (_tileBuffer1.GetComponent<Tile>().GetObjectDescription().objectName ==
            _tileBuffer2.GetComponent<Tile>().GetObjectDescription().objectName)
        {
            Debug.Log("Это пара!");
            yield return new WaitForSeconds(ANIMATION_DELAY_OPEN);
            if (_tileBuffer2.GetComponent<Tile>().GetObjectDescription().tileType ==
                ObjectDescription.TileType.Treasure)
            {

                //Анимация собирания
                StartCoroutine(CollectItemAnimation(_tileBuffer1.GetComponent<Tile>(), _tileBuffer2.GetComponent<Tile>()));
                _tileBuffer1.GetComponent<Tile>().mainCanvasObject.SetActive(false);
                _tileBuffer2.GetComponent<Tile>().mainCanvasObject.SetActive(false);
                yield return new WaitForSeconds(ANIMATION_COLLECT_ITEM);
            }
            else
            {
                Effector.instance.ApplyEffect(_tileBuffer1.GetComponent<Tile>().GetObjectDescription(), true);
                _tileBuffer1.GetComponent<Tile>().PlayAnim();
                _tileBuffer2.GetComponent<Tile>().PlayAnim();
                yield return new WaitForSeconds(0.3f);
                _tileBuffer1.GetComponent<Tile>().Particles.SetActive(false);
                _tileBuffer2.GetComponent<Tile>().Particles.SetActive(false);
                _tileBuffer1.GetComponent<Tile>().Particles1.SetActive(false);
                _tileBuffer2.GetComponent<Tile>().Particles1.SetActive(false);

                _tileBuffer1.GetComponent<Tile>().SetState(2);
                _tileBuffer2.GetComponent<Tile>().SetState(2);
                yield return new WaitForSeconds(ANIMATION_DELAY_FADE_OUT);

                _tileBuffer1 = null;
                _tileBuffer2 = null;


            }
            //yield return new WaitForSeconds(ANIMATION_DELAY_SHOWING);
            //_tileBuffer1.GetComponent<Tile>().SetState(2);
            //_tileBuffer2.GetComponent<Tile>().SetState(2);


            

            _tilesRemain -= 2;
        }
        else
        {
            Debug.Log("Это не пара");

            yield return new WaitForSeconds(ANIMATION_DELAY_OPEN);
            yield return new WaitForSeconds(ANIMATION_DELAY_SHOWING);
            if (_tileBuffer1.GetComponent<Tile>().GetObjectDescription().tileType==ObjectDescription.TileType.Monster)
            {
                Effector.instance.ApplyEffect(_tileBuffer1.GetComponent<Tile>().GetObjectDescription(), false);
            }

            _tileBuffer1.GetComponent<Tile>().Particles.SetActive(false);
            _tileBuffer2.GetComponent<Tile>().Particles.SetActive(false);
            _tileBuffer1.GetComponent<Tile>().Particles1.SetActive(false);
            _tileBuffer2.GetComponent<Tile>().Particles1.SetActive(false);
            //_tileBuffer1.GetComponent<Tile>().Particles2.SetActive(false);
            //_tileBuffer2.GetComponent<Tile>().Particles2.SetActive(false);
            Debug.Log("Прячем частицы здеся");

            _tileBuffer1.GetComponent<Tile>().SetState(0);
            _tileBuffer2.GetComponent<Tile>().SetState(0);

            yield return new WaitForSeconds(ANIMATION_DELAY_OPEN);
            _tileBuffer1 = null;
            _tileBuffer2 = null;
        }

        yield return null;
        BlockPicking(false);
        if (_tilesRemain == 0 && levelInProgress==true)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        levelInProgress = false;
        currentLevel++;
        Debug.Log("Level:"+currentLevel);

        LevelGenerator.instance.StartLevel(quest.levelLibrary[currentLevel-1]);
    }

    /// <summary>
    /// Функция для блока/разблока нажатия на тайлы
    /// </summary>
    /// <param name="_s">Заблокировать/разблокировать</param>
    public void BlockPicking(bool _s)
    {
        _pickingBlocked = _s;
    }

    /// <summary>
    /// Заблокировано ли нажатие на тайлы
    /// </summary>
    /// <returns></returns>
    public bool IsPickingBlocked()
    {
        return _pickingBlocked;
    }

    public void SetRemainTiles(int _t)
    {
        _tilesRemain = _t;
    }

    IEnumerator CollectItemAnimation(Tile _t1, Tile _t2)
    {
        GameObject t1 = Instantiate(LevelGenerator.instance.TileShirtPrefab, LevelGenerator.instance._tilesStartPoint);
        CanvasGroup t1cg =  t1.AddComponent<CanvasGroup>();
        t1.GetComponent<TileShirt>().TileShirtImage.sprite = _t1.TileObjectImage.sprite;
        t1.transform.position = _t1.gameObject.transform.position;

        GameObject t2 = Instantiate(LevelGenerator.instance.TileShirtPrefab, LevelGenerator.instance._tilesStartPoint);
        CanvasGroup t2cg =  t2.AddComponent<CanvasGroup>();
        t2.GetComponent<TileShirt>().TileShirtImage.sprite = _t2.TileObjectImage.sprite;
        t2.transform.position = _t2.gameObject.transform.position;

        AnimationCurve[,] curves = new AnimationCurve[2, 5];
        Vector3 endScale = new Vector3(0.3f, 0.3f, 1f);

        curves[0,0] = AnimationCurve.EaseInOut(0, t1.transform.position.x, ANIMATION_COLLECT_ITEM, inventoryParent.position.x);
        curves[0, 0].AddKey(ANIMATION_COLLECT_ITEM / 2, t1.transform.position.x - 100);
        curves[0,1] = AnimationCurve.EaseInOut(0, t1.transform.position.y, ANIMATION_COLLECT_ITEM, inventoryParent.position.y);

        curves[1, 0] = AnimationCurve.EaseInOut(0, t2.transform.position.x, ANIMATION_COLLECT_ITEM, inventoryParent.position.x);
        curves[1, 0].AddKey(ANIMATION_COLLECT_ITEM / 2, t2.transform.position.x - 100);
        curves[1, 1] = AnimationCurve.EaseInOut(0, t2.transform.position.y, ANIMATION_COLLECT_ITEM, inventoryParent.position.y);

        curves[0, 2] = AnimationCurve.EaseInOut(0, t1.transform.localScale.x, ANIMATION_COLLECT_ITEM, endScale.x);
        curves[0, 3] = AnimationCurve.EaseInOut(0, t1.transform.localScale.y, ANIMATION_COLLECT_ITEM, endScale.y);
        curves[0, 4] = AnimationCurve.EaseInOut(0, 1f, ANIMATION_COLLECT_ITEM, 0f);
        //curves[0, 4].keys[1].outTangent = -50f;

        float curTime = Time.time;
        while (Time.time-curTime< ANIMATION_COLLECT_ITEM)
        {
            t1.transform.position = new Vector3(curves[0, 0].Evaluate(Time.time - curTime), curves[0, 1].Evaluate(Time.time - curTime), t1.transform.position.z);
            t2.transform.position = new Vector3(curves[1, 0].Evaluate(Time.time - curTime), curves[1, 1].Evaluate(Time.time - curTime), t2.transform.position.z);
            t1.transform.localScale = new Vector3(curves[0, 2].Evaluate(Time.time - curTime), curves[0, 3].Evaluate(Time.time - curTime), 1f);
            t2.transform.localScale = new Vector3(curves[0, 2].Evaluate(Time.time - curTime), curves[0, 3].Evaluate(Time.time - curTime), 1f);

            t1cg.alpha = curves[0, 4].Evaluate(Time.time - curTime);
            t2cg.alpha = curves[0, 4].Evaluate(Time.time - curTime);
            yield return null;
        }
        t1.transform.position = new Vector3(curves[0, 0].Evaluate(ANIMATION_COLLECT_ITEM), curves[0, 1].Evaluate(ANIMATION_COLLECT_ITEM), t1.transform.position.z);
        t2.transform.position = new Vector3(curves[1, 0].Evaluate(ANIMATION_COLLECT_ITEM), curves[1, 1].Evaluate(ANIMATION_COLLECT_ITEM), t2.transform.position.z);
        Destroy(t1);
        Destroy(t2);
        yield return null;
        InventoryController.instance.AddItem(_tileBuffer2.GetComponent<Tile>().GetObjectDescription(), 1);
        _tileBuffer1 = null;
        _tileBuffer2 = null;
    }

}
