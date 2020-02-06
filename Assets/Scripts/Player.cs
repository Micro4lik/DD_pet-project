using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Управление характеристиками игрока
/// </summary>
public class Player : MonoBehaviour
{
    public static Player instance;

    public Image characterSprite;

    [Header("Здоровье игрока")]
    [SerializeField] private int _hpMax;
    [SerializeField] private int _hp;
    [Header("Голод игрока")]
    [SerializeField] private int _hungerMax;
    [SerializeField] private int _hunger;



    void Awake()
    {
        instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        SetMaxHp(_hpMax);
        SetHp(_hp);
        SetMaxHunger(_hungerMax);
        SetHunger(_hunger);

        Debug.Log(MainMenu.characterCurrent);
        Debug.Log(MainMenu.avatarSprite);

        if (MainMenu.avatarSprite != null)
        {
            characterSprite.sprite = MainMenu.avatarSprite;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamageHp(int _dmg)
    {
        SetHp(_hp-_dmg);
    }
    public void TakeDamageHunger(int _dmg)
    {
        SetHunger(_hunger - _dmg);
    }

    public void SetHp(int _h)
    {
        if (_h <= 0)
        {
            _hp = 0;
            //GameWatcher.GameOver();
        }
        else if (_h > _hpMax)
        {
            _hp = _hpMax;
        }
        else
        {
            _hp = _h;
        }
        UIcontroller.instance.SetHp(_hp);
    }
    public void SetMaxHp(int _h)
    {
        _hpMax = _h;
        UIcontroller.instance.SetMaxHp(_hpMax);
    }

    public int GetMaxHp()
    {
        return _hpMax;
    }

    public int GetCurrentHp()
    {
        return _hp;
    }

    public void SetHunger(int _h)
    {
        if (_h <= 0)
        {
            _hunger = 0;
            //GameWatcher.GameOver();
        }
        else if (_h > _hungerMax)
        {
            _hunger = _hungerMax;
            //GameWatcher.GameOver();
        }
        else
        {
            _hunger = _h;
        }
        UIcontroller.instance.SetHunger(_hunger);
    }

    public void SetMaxHunger(int _h)
    {
        _hungerMax = _h;
        UIcontroller.instance.SetMaxHunger(_hungerMax);
    }

    public int GetMaxHunger()
    {
        return _hungerMax;
    }

    public int GetCurrentHunger()
    {
        return _hunger;
    }
}
