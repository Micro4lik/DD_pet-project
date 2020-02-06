using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogRunner : MonoBehaviour
{
    public static DialogRunner instance;

     private Dialog _dialog;
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private TextMeshProUGUI _replicaText;
    [SerializeField] private TextMeshProUGUI _firstPersonName;
    [SerializeField] private TextMeshProUGUI _secondPersonName;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private CanvasGroup _textAlpha;
    [SerializeField] private CanvasGroup _playerAlpha;
    [SerializeField] private CanvasGroup _secondCharacterAlpha;

    [SerializeField] private Image _firstPersonImage;
    [SerializeField] private Image _secondPersonImage;
    [SerializeField] private GameObject _blockingPanel;

    private Animator _animator;
    private int _replicaCount = 0;
    const float ANIM_WAIT_BETWEEN_CHARS = 0.01f;
    private bool isPlayerAvatarShowed = false;
    private bool isSecondAvatarShowed = false;
    private int _prevFirstCharStatus = 0;
    private int _prevSecondCharStatus = 0;

    public bool isDialogInProgress = false;


    //Корутина вывода текста по буквам
    IEnumerator PrintText(string _text)
    {

        int charCount = 0;
        string _tmpText = "";
        while (charCount < _text.Length)
        {
            _tmpText += _text[charCount];
            charCount++;
            _replicaText.text = _tmpText;
            yield return new WaitForSeconds(ANIM_WAIT_BETWEEN_CHARS);
        }
    }

    void Awake()
    {
        instance = this;
        _replicaText.text = "";
        _animator = GetComponent<Animator>();
        _blockingPanel.SetActive(false);
        _mainCanvas.GetComponent<CanvasGroup>().alpha = 0f;

    }

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isDialogInProgress )
            {
                NextReplica();
            }
        }
    }
    

    public void StartDialog(Dialog _d)
    {
        _dialog = _d;
        StartCoroutine(BeginDialog());
    }

    IEnumerator BeginDialog()
    {

        _blockingPanel.SetActive(true);
        isDialogInProgress = true;
        _textAlpha.alpha = 0;
        _playerAlpha.alpha = 0;
        _secondCharacterAlpha.alpha = 0;
        yield return new WaitForSeconds(0.5f);
        _mainCanvas.GetComponent<CanvasGroup>().alpha = 1f;
        _animator.SetTrigger("FirstAppear");
        yield return new WaitForSeconds(1f);
        

        NextReplica();
    }


    public void NextReplica()
    {

        if (_replicaCount < _dialog.replicaList.Count)
        {
            StartCoroutine(PrintText(_dialog.replicaList[_replicaCount].replicaText[LocalizationManager.localeNum]));

            if (_prevFirstCharStatus != _dialog.replicaList[_replicaCount].firstCharStatus)
            {
                if (_prevFirstCharStatus == 0)
                {
                    if (_dialog.replicaList[_replicaCount].firstCharStatus ==2)
                    {
                        _firstPersonName.text = _dialog.replicaList[_replicaCount].charaterName[LocalizationManager.localeNum];
                        _firstPersonImage.sprite = _dialog.replicaList[_replicaCount].characterSprite;
                        _animator.SetTrigger("ShowPlayerAvatar");
                    }
                }
                else if (_prevFirstCharStatus == 1)
                {
                    if (_dialog.replicaList[_replicaCount].firstCharStatus == 2)
                    {
                        _animator.SetTrigger("FadeInPlayerAvatar");
                    }
                }
                else if (_prevFirstCharStatus == 2)
                {
                    if (_dialog.replicaList[_replicaCount].firstCharStatus == 1)
                    {
                        _animator.SetTrigger("FadeOutPlayerAvatar");
                    }
                }
                _prevFirstCharStatus = _dialog.replicaList[_replicaCount].firstCharStatus;
            }

            if (_prevSecondCharStatus != _dialog.replicaList[_replicaCount].secondCharStatus)
            {
                if (_prevSecondCharStatus == 0)
                {
                    if (_dialog.replicaList[_replicaCount].secondCharStatus == 2)
                    {
                        _secondPersonName.text = _dialog.replicaList[_replicaCount].charaterName[LocalizationManager.localeNum];
                        _secondPersonImage.sprite = _dialog.replicaList[_replicaCount].characterSprite;
                        _animator.SetTrigger("ShowSecondAvatar");
                    }
                }
                else if (_prevSecondCharStatus == 1)
                {
                    if (_dialog.replicaList[_replicaCount].secondCharStatus == 2)
                    {
                        _animator.SetTrigger("FadeInSecondAvatar");
                    }
                }
                else if (_prevSecondCharStatus == 2)
                {
                    if (_dialog.replicaList[_replicaCount].secondCharStatus == 1)
                    {
                        _animator.SetTrigger("FadeOutSecondAvatar");
                    }
                }
                _prevSecondCharStatus = _dialog.replicaList[_replicaCount].secondCharStatus;
            }
            _replicaCount++;
        }
        else if (_replicaCount >= _dialog.replicaList.Count)
        {
            StartCoroutine(EndDialog());
        }

    }

    IEnumerator EndDialog()
    {
        _animator.SetTrigger("HideDialog");
        yield return new WaitForSeconds(0.5f);
        _replicaText.text = "";
        _firstPersonName.text = "";
        _firstPersonImage.sprite = null;
        _secondPersonName.text = "";
        _secondPersonImage.sprite = null;
        _replicaCount = 0;

        _textAlpha.alpha = 0;
        _playerAlpha.alpha = 0;
        _secondCharacterAlpha.alpha = 0;
        _mainCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        isDialogInProgress = false;
    }
    
}
