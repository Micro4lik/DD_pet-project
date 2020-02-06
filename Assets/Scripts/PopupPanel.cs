using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour
{
    private const float minScale = 0.0001f;
    private const float minAlpha = 0.0001f;

    public enum TweenType
    {
        None = 0,
        Move,
        Scale,
        Fade
    };

    public enum MoveDirections
    {
        Left = 0,
        Right,
        Top,
        Bottom,
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }

    [Header("Fade")]
    public Button fade;
    public bool closeOnFadeClick = true;
    [Header("Close Button")]
    public Button closeButton;
    [Header("Transition")]
    public TweenType tweenType = TweenType.Scale;
    //public LeanTweenType easeType = LeanTweenType.linear;
    public float tweenTime = 0.5f;
    public MoveDirections direction;

    private CanvasGroup mainGroup, fadeGroup;

    void Init()
    {
        /*fadeGroup = fade.gameObject.GetComponent<CanvasGroup>();

        if (tweenType == TweenType.Fade)
        {
            fadeGroup.alpha = 1f;
            fadeGroup.ignoreParentGroups = false;
        }
        else
            fadeGroup.alpha = 0f;*/

        mainGroup = GetComponent<CanvasGroup>();

        //if (mainGroup == null) mainGroup = gameObject.AddComponent<CanvasGroup>();
        mainGroup.alpha = 0f;

    }

    /*public virtual void OpenPanel(bool withoutTween = false)
    {
        //if (isShowing) return;
        //if (isOpening) return;

        /*if (withoutTween)
        {
            resumeTweenTime = tweenTime;
            tweenTime = 0.01f;
            onPanelOpened.AddListener(() => { tweenTime = resumeTweenTime; });
        }*/

        //isOpening = true;
        //Init();
        //gameObject.SetActive(true);

        //if (tweenType != TweenType.Fade && tweenType != TweenType.None && fade && fade.gameObject.activeSelf)
            //TweenFadeIn();

        //onOpenPanel.Invoke();

        /*switch (tweenType)
        {
            case TweenType.None:
                OnOpened();
                break;
            case TweenType.Move:
                TweenMovePanelIn();
                break;
            case TweenType.Scale:
                TweenScalePanelIn();
                break;
            case TweenType.Fade:
                TweenFadePanelIn();
                break;
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
