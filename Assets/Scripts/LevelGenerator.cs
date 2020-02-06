using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;

    [SerializeField] private GameObject TilePrefab;
    [SerializeField] public GameObject TileShirtPrefab;
    [SerializeField] private GameObject GridParent;
    [SerializeField] public Transform _tilesStartPoint;
    [SerializeField] private Transform _bgTransformParent;


    private List<ObjectDescription> _playerDeck;

    private List<GameObject> levelTiles = new List<GameObject>();
    private int _levelSize;
    


    private const float ANIMATION_TIME_INTRO_SHUFFLE = 1f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //StartLevel();
    }

    public void StartLevel(LevelTemplate _levelTemplate)
    {
        SetupDeck();
        StartCoroutine(GenerateLevel(_levelTemplate));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
        }

    }

    private void SetupDeck()
    {
        _playerDeck = new List<ObjectDescription>(GameWatcher.instance.playerDeck);
    }

    IEnumerator GenerateLevel(LevelTemplate _template)
    {
        Debug.Log("Generating");
        //Удаляем фоны прошлого уровня
        for (int i = _bgTransformParent.childCount - 1; i >= 0; i--)
        {
            Transform _got = _bgTransformParent.GetChild(i);
            _got.parent = null;
            Destroy(_got.gameObject);
        }
        //Создаём фоны
        foreach (var bg in _template.background)
        {
            Instantiate(bg, _bgTransformParent);
        }
       //Удаляем карты прошлого уровня
        for (int i = GridParent.transform.childCount - 1; i >= 0; i--)
        {
            Transform _got = GridParent.transform.GetChild(i);
            _got.parent = null;
            Destroy(_got.gameObject);
        }
        for (int i = _tilesStartPoint.transform.childCount - 1; i >= 0; i--)
        {
            Transform _got = _tilesStartPoint.transform.GetChild(i);
            _got.parent = null;
            Destroy(_got.gameObject);
        }
        //Debug.Log("Ost:" + GridParent.transform.childCount);
        levelTiles.Clear();
        GridParent.GetComponent<GridLayoutGroup>().constraintCount = _template.levelColumns;
        GameObject tmpObj;
        _levelSize = _template.levelSize;
        int rndTile;
        int tmpRnd;
        if (_template.introDialog != null)
        {
            DialogRunner.instance.StartDialog(_template.introDialog);
            while (DialogRunner.instance.isDialogInProgress)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        //Меняем рубашку у тайлов
        if (_template.shirt != null)
        {
            TilePrefab.GetComponent<Tile>().TileShirtImage.sprite = _template.shirt;
            TileShirtPrefab.GetComponent<TileShirt>().TileShirtImage.sprite = _template.shirt;
        }
        //Создаём новые тайлы

        for (int i = 0; i < _levelSize; i++)
        {
            tmpObj = Instantiate(TilePrefab, GridParent.transform);
            //Debug.Log("p"+tmpObj.transform.position);
            levelTiles.Add(tmpObj);
        }
        SetActiveAllTiles(false);
        List<GameObject> tilesToPopulate = new List<GameObject>(levelTiles);
        

        //Нумеруем тайлы по порядку из расположения в Grid
        int fe = 0;
        foreach (Transform child in GridParent.transform)
        {
            child.gameObject.GetComponent<Tile>().index = fe;
            //Debug.Log("i_set: " + fe);
            fe++;
        }
        
        //Прячем пустые тайлы
        for (int i = 0; i < _template.emptyCellsArray.Length; i++)
        {
            /*
            foreach (var tile in tilesToPopulate)
            {
                Debug.Log("i: "+tile.GetComponent<Tile>().index);
            }*/

            tmpObj = tilesToPopulate.Find(t => t.GetComponent<Tile>().index == _template.emptyCellsArray[i]);
            tmpObj.GetComponent<Tile>().SetObjectDescription(0);
            tilesToPopulate.Remove(tmpObj);
        }

        //GameWatcher.instance.levelTiles = new List<GameObject>(tilesToPopulate);
        StartCoroutine(IntroShuffleAnimation(new List<GameObject>(tilesToPopulate)));
        GameWatcher.instance.SetRemainTiles(_levelSize- _template.emptyCellsArray.Length);

        //Заселение обязательными объектами из уровня
        foreach (var obj in _template.necessaryObjects)
        {
            for (int j = 0; j < 2; j++)
            {
                rndTile = Random.Range(0, tilesToPopulate.Count);
                tilesToPopulate[rndTile].GetComponent<Tile>().SetObjectDescription(obj);
                tilesToPopulate.Remove(tilesToPopulate[rndTile]);
            }
        }

        //Заселение оставшегося места объектами из колоды
        int restTilesCount = tilesToPopulate.Count;
        for (int i = 0; i < restTilesCount / 2; i++)
        {
            tmpRnd = Random.Range(0, _playerDeck.Count);
            for (int j = 0; j < 2; j++)
            {
                rndTile = Random.Range(0, tilesToPopulate.Count);
                tilesToPopulate[rndTile].GetComponent<Tile>().SetObjectDescription(_playerDeck[tmpRnd]);
                tilesToPopulate.Remove(tilesToPopulate[rndTile]);
            }

            if (_playerDeck.Count > 0)
            {
                _playerDeck.Remove(_playerDeck[tmpRnd]);
            }
            else
            {
                _playerDeck = new List<ObjectDescription>(GameWatcher.instance.playerDeck);
            }

            
        }

        yield return null;
    }


    IEnumerator IntroShuffleAnimation(List<GameObject> _tList)
    {
        _tilesStartPoint.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        GameObject tmpObj;
        List<GameObject> tilesForAnimation = new List<GameObject>();
        List<Vector3> targetCoordsList = new List<Vector3>();
        AnimationCurve[,] curves = new AnimationCurve[_tList.Count, 3];
        for (int i = 0; i < _tList.Count; i++)
        {
            tmpObj = Instantiate(TileShirtPrefab, _tilesStartPoint);
            tmpObj.transform.localPosition = new Vector3(tmpObj.transform.position.x, tmpObj.transform.position.y,0);
            // tmpObj.transform.position = _tilesStartPoint.position;
            curves[i, 0] = AnimationCurve.EaseInOut(0, _tilesStartPoint.position.x, 1, _tList[i].GetComponent<RectTransform>().position.x);
            curves[i, 0].AddKey(ANIMATION_TIME_INTRO_SHUFFLE/2, _tilesStartPoint.position.x+100 );

            curves[i, 1] = AnimationCurve.EaseInOut(0, _tilesStartPoint.position.y, 1, _tList[i].GetComponent<RectTransform>().position.y);
            curves[i, 1].AddKey(ANIMATION_TIME_INTRO_SHUFFLE/2, _tilesStartPoint.position.y + 300);

            curves[i, 2] = AnimationCurve.EaseInOut(0, _tilesStartPoint.position.z, 1, _tList[i].GetComponent<RectTransform>().position.z);
            curves[i, 2].AddKey(ANIMATION_TIME_INTRO_SHUFFLE / 3, _tilesStartPoint.position.z - 350f);
            //curves[i, 2].AddKey(ANIMATION_TIME_INTRO_SHUFFLE *2 / 3, _tilesStartPoint.position.z + 150f);

            //curves[i, 1].SmoothTangents(2,0.1f);
            tilesForAnimation.Add(tmpObj);
            //Debug.Log("0:" + tmpObj.transform.localPosition.z);
            StartCoroutine(IntroShuffleAnimationTile(tmpObj, curves[i,0], curves[i, 1], curves[i, 2]));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(ANIMATION_TIME_INTRO_SHUFFLE);
        SetActiveAllTiles(true);
        //_tilesStartPoint.gameObject.SetActive(false);
        for (int i = _tilesStartPoint.childCount - 1; i >= 0; i--)
        {
            _tilesStartPoint.GetChild(i).gameObject.SetActive(false);

        }
        //GameWatcher.instance.StartLevel();
        StartCoroutine(RevealCoroutine());
        GameWatcher.instance.levelInProgress = true;
    }

    IEnumerator IntroShuffleAnimationTile(GameObject _tile, AnimationCurve _curveX, AnimationCurve _curveY, AnimationCurve _curveZ)
    {

        float curTime = Time.time;
        while (Time.time - curTime < ANIMATION_TIME_INTRO_SHUFFLE)
        {
            //Debug.Log("1:" + _tile.transform.localPosition.z);
            _tile.transform.position = new Vector3(_curveX.Evaluate(Time.time - curTime), _curveY.Evaluate(Time.time - curTime), _curveZ.Evaluate(Time.time - curTime));
            _tile.transform.localPosition = new Vector3(_tile.transform.localPosition.x, _tile.transform.localPosition.y, _tile.transform.localPosition.z);
            //yield return null;
            //Debug.Log(tilesForAnimation[0].transform.position);
            yield return null;
        }
        _tile.transform.position = new Vector3(_curveX.Evaluate(ANIMATION_TIME_INTRO_SHUFFLE), _curveY.Evaluate(ANIMATION_TIME_INTRO_SHUFFLE), 0);
        _tile.transform.localPosition = new Vector3(_tile.transform.localPosition.x, _tile.transform.localPosition.y, 0);

    }

    private void SetActiveAllTiles(bool _active)
    {
        foreach (var tile in levelTiles)
        {
            tile.GetComponent<Tile>().mainCanvasObject.SetActive(_active);
        }
    }

    IEnumerator RevealCoroutine()
    {
        foreach (var tile in levelTiles)
        {
            if (tile.GetComponent<Tile>().GetObjectDescription() != null)
            {
                tile.GetComponent<Tile>().SetTileGraphics(3);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
