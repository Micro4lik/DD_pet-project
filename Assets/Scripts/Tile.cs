﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using Coffee.UIExtensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Управляет всем, что связано с тайлом
/// </summary>
public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public Image TileShirtImage;
    [SerializeField] private Image TileBackgroundImage;
    [SerializeField] public Image TileObjectImage;
    [SerializeField] private Image TileBorderImage;
    public GameObject  mainCanvasObject;

    [SerializeField] private GameObject TileShirt;
    [SerializeField] private GameObject TileBackground;
    [SerializeField] private GameObject TileObject;

    [SerializeField] private TextMeshProUGUI _speechBubbleText;

    [SerializeField] private ObjectDescription _objectDescription;

    public GameObject Particles;
    public GameObject Particles1;
    public GameObject Particles2;

    public int index;

    private bool _tileWasClicked=false;

    private Animator _animator;
    /// <summary>
    /// _state - Статус тайла.
    /// 0 - закрыт
    /// 1 - открыт
    /// 2 - спрятан
    /// </summary>
    private int _state = 0;

    /// <summary>
    /// Тип тайла
    /// 0-пусто
    /// 1-монстр
    /// 2-предмет
    /// </summary>
    private int _type = 0;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ClickHandler());

        //StartPos = transform.position;
        //StartSize = transform.localScale;
    }

    //private Vector3 mousePosition;
    //public float moveSpeed = 2f;

    // Update is called once per frame

    Coroutine lastRoutine = null;

    public void OnPointerEnter(PointerEventData data)
    {
        //lastRoutine = StartCoroutine(Test());
    }

        IEnumerator Test()
    {
        while (true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = objectPos.x - mousePos.x;
            mousePos.y = objectPos.y - mousePos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(angle, angle, 0));

        yield return null;
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        //StopCoroutine(lastRoutine);
    }

    /*public Vector3 pz;
    public Vector3 StartPos;
    public Vector3 StartSize;

    public float moveModifier;

    // Update is called once per frame
    void Update()
    {
        Vector3 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        pz.z = 0;
        gameObject.transform.position = pz;
        //Debug.Log("Mouse Position: " + pz);

        transform.position = new Vector3(StartPos.x + (pz.x * moveModifier), StartPos.y + (pz.y * moveModifier), StartPos.z);
*/

    void Update()
    {
        //Debug.Log("...");
        //gameObject.transform.RotateAround(new Vector3(0, Input.GetAxis("Mouse X") * (Screen.width - Camera.main.ScreenToWorldPoint(Input.mousePosition).y) / 150, 0), 0.03f);

        //rotation
        //if (_tileWasClicked)
        //{
        /*Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));*/
        //}

        //Vector3 temp = Input.mousePosition;
        //temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.
        //transform.position = Camera.main.ScreenToWorldPoint(temp);

        //Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //transform.position = mousePosition;

        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);

        // Тот самый поворот
        // вычисляем разницу между текущим положением и положением мыши
        //Vector3 difference = mousePosition - transform.position;
        //difference.Normalize();
        // вычисляемый необходимый угол поворота
        //float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // Применяем поворот вокруг оси Z
        //transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

    }

    /// <summary>
    /// Тест анимации исчезновения
    /// </summary>
    public void PlayAnim()
    {
        //TileObjectImage.GetComponent<UIDissolve>().Play();
        //TileShirtImage.GetComponent<UIDissolve>().Play();
        //TileBackgroundImage.GetComponent<UIDissolve>().Play();
        //TileBorderImage.GetComponent<UIDissolve>().Play();
        TileShirtImage.gameObject.SetActive(false);
        TileBackgroundImage.gameObject.SetActive(false);

        //увеличение ширины цвета сгорания со временем
        //StartCoroutine(DissWidth());

        //офаем частицы, шобы красиво
        //Particles.SetActive(false);
        //Particles1.SetActive(false);
    }

    IEnumerator DissWidth()
    {
        while (true)
        {
            TileObjectImage.GetComponent<UIDissolve>().width -= 0.05f;
            TileShirtImage.GetComponent<UIDissolve>().width -= 0.05f;
            TileBackgroundImage.GetComponent<UIDissolve>().width -= 0.05f;
            TileBorderImage.GetComponent<UIDissolve>().width -= 0.05f;

            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
        /// Тест установки цвета частиц
        /// </summary>
        public void SetColorParticles()
    {
        //Particles.GetComponent<ParticleSystem>().main.startColor = new Color(0, 0, 0, 1);
        ParticleSystem.MainModule main = Particles.GetComponent<ParticleSystem>().main;
        main.startColor = Color.red;
        ParticleSystem.MainModule main1 = Particles1.GetComponent<ParticleSystem>().main;
        main1.startColor = Color.red;
        
    }


    /// <summary>
    /// Взводит флажок клика по тайлу
    /// </summary>
    public void ClickOnTile()
    {
        if (!GameWatcher.instance.IsPickingBlocked())
        {
            _tileWasClicked = true;
            Player.instance.TakeDamageHunger(1);
            //Particles.SetActive(true);
        }
    }

    /// <summary>
    /// Обновляет отображение тайла согласно установленному ObjectDescription
    /// </summary>
    public void InitializeTileGraphics()
    {
        TileObjectImage.sprite = _objectDescription.objectSprite;
        _speechBubbleText.text = _objectDescription.objectSpeech[0];
        //SetTileGraphics(0);
    }
    
    /// <summary>
    /// Управляет анимацией тайла
    /// </summary>
    /// <param name="_s">0 - закрыт, 1 - открыт, 2 - спрятан, 3 - засвет тайла, 4 - нажатие на тайл</param>
    public void SetTileGraphics(int _s)
    {
        switch (_s)
        {
            case 0:
                _animator.SetTrigger("HideTile");
                if (GameWatcher.instance._tileBuffer1 == gameObject)
                {
                    //_animator.SetTrigger("HideSpeechBubble");
                }
                break;
            case 1:
                _animator.SetTrigger("ShowTile");
                /*if (GameWatcher.instance._tileBuffer1 == gameObject)
                {
                    _animator.SetTrigger("ShowSpeechBubble");
                }*/
                break;
            case 2:
                _animator.SetTrigger("FadeOut");
                
                /*if (GameWatcher.instance._tileBuffer1 == gameObject)
                {
                    _animator.SetTrigger("HideSpeechBubble");
                }*/

                break;
            case 3:
                _animator.SetTrigger("RevealTiles");
                break;
            case 4:
                _animator.SetTrigger("Press");
                break;


        }
    }

    /// <summary>
    /// Корутина-обработчик нажатия на тайл. Работает всегда.
    /// </summary>
    /// <returns></returns>
    IEnumerator ClickHandler()
    {
        //_animator.Play();
        while (true)
        {
            if (_tileWasClicked)
            {
                if (GetState() == 0)
                {
                    GameWatcher.instance.OpenTile(gameObject);
                    SetState(1);
                    Particles.SetActive(true);
                    Particles1.SetActive(true);
                    //Particles2.SetActive(true);
                }
                else
                {
                    Particles.SetActive(false);
                    Particles1.SetActive(false);
                    //Particles2.SetActive(false);
                    //SetState(0);
                }


                _tileWasClicked = false;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Устанавливает статус тайла в _s 
    /// 0 - закрыт
    /// 1 - открыт
    /// </summary>
    /// <param name="_s">0 - закрыт, 1 - открыт, 2 - спрятан</param>
    public void SetState(int _s, bool playAnimation = true)
    {
        _state = _s;
        if (playAnimation)
        {
            SetTileGraphics(_state);
        }
        else
        {
            if (_state == 2)
            {
                TileObject.SetActive(false);
                TileShirt.SetActive(false);
                TileBackground.SetActive(false);
                _objectDescription = null;
            }
        }
    }

    /// <summary>
    /// Возвращает текущий статус тайла
    /// </summary>
    /// <returns></returns>
    public int GetState()
    {
        return _state;
    }
    /*
    /// <summary>
    /// Возвращает тип тайла
    /// 0 - пусто, 1 - монстр, 2 - предмет
    /// </summary>
    /// <returns></returns>
    public ObjectDescription.TileType GetTileType()
    {
        return _objectDescription.tileType;
    }


    /// <summary>
    /// Устанавливает тип тайла
    /// </summary>
    /// <param name="_t">0 - пусто, 1 - монстр, 2 - предмет</param>
    public void SetTileType(int _t)
    {
        _type=_t;
    }
    */

    /// <summary>
    /// Устанавливает ObjectDescription и запускает обновление графики тайла
    /// </summary>
    /// <param name="_od"></param>
    public void SetObjectDescription(ObjectDescription _od)
    {
        _objectDescription = _od;
        //Debug.Log(_objectDescription.tileType);
        InitializeTileGraphics();
    }

    //Перегрузка для конкретных состояни тайла, например "пустой"
    public void SetObjectDescription(int _s)
    {
        SetState(2, false);
    }

    public ObjectDescription GetObjectDescription()
    {
        return _objectDescription;
    }

    

}
