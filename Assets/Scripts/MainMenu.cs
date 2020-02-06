using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public Animator animatorCharactersPopup;

    public ScrollRect ScrollRectQuests;
    public bool isScroll = false;

    private float startPosition;


    [Header ("Character options")]
    public GameObject heroesGO;
    public GameObject hero1GO;
    public GameObject hero2GO;

    public Button buttonPreviousCharacter;
    public Button buttonNextCharacter;

    public static Sprite avatarSprite;

    [Header("First character")]
    public TextMeshProUGUI character1Name;
    public Image character1Sprite;

    [Header("Second character")]
    public TextMeshProUGUI character2Name;
    public Image character2Sprite;

    public TextMeshProUGUI characterBiography;
    public Image characterAbilityIcon;

    [Header("Character templates")]
    [SerializeField] private List<CharacterTemplate> charactersLibrary = new List<CharacterTemplate>();
    private int characterCount;
    public static int characterCurrent = 0;

    [Header("Quest options")]
    public bool questIsSelected;
    public TextMeshProUGUI questDescription;

    public GameObject questsGrid;
    public GameObject questPrefab;

    // Start is called before the first frame update
    void Start()
    {
        characterCount = charactersLibrary.Count;

        character1Name.text = charactersLibrary[0].characterName;
        character1Sprite.sprite = charactersLibrary[0].characterSprite;
        characterBiography.text = charactersLibrary[0].characterBiography;
        characterAbilityIcon.sprite = charactersLibrary[0].characterAbilityIcon;

        ShowQuestList();
    }

    IEnumerator ScrollUp1()
    {


        AnimationCurve curve = new AnimationCurve();
        while (ScrollRectQuests.verticalNormalizedPosition > (startPosition - 0.5f))
        {
            Debug.Log(ScrollRectQuests.verticalNormalizedPosition);
       
        curve = AnimationCurve.EaseInOut(0, ScrollRectQuests.transform.localPosition.y, 1, startPosition - 0.5f);
        

        yield return null;
        }

    }

    IEnumerator ScrollUp()
    {


        while (ScrollRectQuests.verticalNormalizedPosition < (startPosition + 0.5f))
        {
            ScrollRectQuests.verticalNormalizedPosition += 0.84f * Time.deltaTime;
            if (ScrollRectQuests.verticalNormalizedPosition >= 0.99f)
            {
                ScrollRectQuests.verticalNormalizedPosition = 1f;
                isScroll = false;
                //StopCoroutine("ScrollUp");
                StopAllCoroutines();
            }
            yield return null;

        }

        isScroll = false;

    }

    IEnumerator ScrollDown()
    {

        while (ScrollRectQuests.verticalNormalizedPosition > (startPosition - 0.5f))
        {

            Debug.Log(ScrollRectQuests.verticalNormalizedPosition + " - current");
            Debug.Log((startPosition - 0.5f) + " - startPos - 0.5f");
            ScrollRectQuests.verticalNormalizedPosition -= 0.84f * Time.deltaTime;
            if (ScrollRectQuests.verticalNormalizedPosition <= 0.01f)
            {
                Debug.Log("ВЫРОВНЯЛ");
                ScrollRectQuests.verticalNormalizedPosition = 0f;
                isScroll = false;
                //StopCoroutine("ScrollDown");
                StopAllCoroutines();
            }
            yield return null;

        }

        isScroll = false;

    }

    public void ScrollUpQuests()
    {
        if (!isScroll && ScrollRectQuests.verticalNormalizedPosition != 1)
        {
            startPosition = ScrollRectQuests.verticalNormalizedPosition;
            isScroll = true;
            StartCoroutine(ScrollUp());
        }
    }

    public void ScrollDownQuests()
    {
        if (!isScroll && ScrollRectQuests.verticalNormalizedPosition != 0)
        {
            startPosition = ScrollRectQuests.verticalNormalizedPosition;
            isScroll = true;
            StartCoroutine(ScrollDown());
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowQuestList()
    {
        GameObject tmpObj;
        
        for (int i = 0; i < charactersLibrary[characterCurrent].Quests.Length; i++)
        {
            tmpObj = Instantiate(questPrefab, questsGrid.transform);
        }
    }

    public void PreviousCharacter()
    {
        if (characterCurrent != 0)
        {
            characterCurrent -= 1;
        }
        else
        {
            characterCurrent = characterCount-1;
        }

        character1Name.text = charactersLibrary[characterCurrent].characterName;
        character1Sprite.sprite = charactersLibrary[characterCurrent].characterSprite;
        characterBiography.text = charactersLibrary[characterCurrent].characterBiography;
        characterAbilityIcon.sprite = charactersLibrary[characterCurrent].characterAbilityIcon;

        avatarSprite = character1Sprite.sprite;
    }

    public void NextCharacter()
    {
        if (characterCurrent != characterCount-1 && LeanTween.isTweening(heroesGO) == false)
        {
            characterCurrent += 1;

            character2Name.text = charactersLibrary[characterCurrent].characterName;
            character2Sprite.sprite = charactersLibrary[characterCurrent].characterSprite;

            LeanTween.moveLocalY(heroesGO, +498f, 0.5f).setEaseInCubic().setOnComplete(() =>
            {
                heroesGO.transform.localPosition = new Vector3(0f, 0f, transform.position.z);

                character1Name.text = charactersLibrary[characterCurrent].characterName;
                character1Sprite.sprite = charactersLibrary[characterCurrent].characterSprite;
                characterBiography.text = charactersLibrary[characterCurrent].characterBiography;
                characterAbilityIcon.sprite = charactersLibrary[characterCurrent].characterAbilityIcon;

                if (characterCurrent == characterCount - 1)
                {
                    characterCurrent = -1;
                    //buttonNextCharacter.interactable = false;
                }

            });

        }
        else
        {
            //buttonNextCharacter.interactable = false;
            //characterCurrent = 0;
        }
        
        avatarSprite = character1Sprite.sprite;
        
    }

    public void RandomHero()
    {
        //Hero.sprite = Characters1[Random.Range(0, 108)];
        //Hero.sprite = Characters1[Random.Range(0, Characters1.Length)];
    }

    public void SwitchScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void ShowCharactersAndQuests()
    {
        animatorCharactersPopup.SetTrigger("show");
    }
    public void HideCharactersAndQuests()
    {
        animatorCharactersPopup.SetTrigger("hide");
    }

    public void ShowCharacters()
    {
        animatorCharactersPopup.SetTrigger("showCharacters");
    }
    public void HideCharacters()
    {
        animatorCharactersPopup.SetTrigger("hideCharacters");
    }

    public void ShowQuests()
    {
        animatorCharactersPopup.SetTrigger("showQuests");
    }
    public void HideQuests()
    {
        animatorCharactersPopup.SetTrigger("hideQuests");
    }

    public void ShowQuestsDescription()
    {
        if (!questIsSelected)
        {
            questIsSelected = true;
            Debug.Log("квест выбран");
            animatorCharactersPopup.SetTrigger("showQuestsDescription");
        }
        else
        {
            questDescription.text = "gav gav";
        }
        
    }

}
