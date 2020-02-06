using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIcontroller : MonoBehaviour
{

    public static UIcontroller instance;

    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _hungerSlider;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _hungerText;

    private const float ANIMATION_DELAY_SLIDERS = 0.3f;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHp(int _hp)
    {
        _hpText.text = _hp + "/" + Player.instance.GetMaxHp();

        if (_hp > _hpSlider.maxValue)
        {
            StartCoroutine(SliderAnimation(_hpSlider.maxValue, _hpSlider));
        }
        else
        {
            StartCoroutine(SliderAnimation(_hp, _hpSlider));
        }
    }
    public void SetMaxHp(int _hp)
    {
        _hpText.text = Player.instance.GetCurrentHp() + "/" + _hp;
        _hpSlider.maxValue = _hp;
    }
    public void SetHunger(int _h)
    {
        _hungerText.text = _h + "/" + Player.instance.GetMaxHunger();

        if (_h > _hungerSlider.maxValue)
        {
            StartCoroutine(SliderAnimation(_hungerSlider.maxValue, _hungerSlider));
        }
        else
        {
            StartCoroutine(SliderAnimation(_h, _hungerSlider));
        }

    }
    public void SetMaxHunger(int _h)
    {
        _hungerText.text = Player.instance.GetCurrentHunger() + "/" + _h;
        _hungerSlider.maxValue = _h;
    }

    IEnumerator SliderAnimation(float _target, Slider _slider)
    {
        float _start = _slider.value;
        float curTime = Time.time;
        while (Time.time - curTime < ANIMATION_DELAY_SLIDERS)
        {
            _slider.value= Mathf.Lerp(_start, _target, (Time.time - curTime) / ANIMATION_DELAY_SLIDERS);
            yield return null;
        }
        _slider.value = _target;
    }
}
