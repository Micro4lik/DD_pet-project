using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
    //, IPointerEnterHandler, IPointerExitHandler
{
    //[SerializeField] private List<ObjectDescription> itemsLibrary = new List<ObjectDescription>();

    public TextMeshProUGUI itemCountText;
    public Image itemImage;

    //OD описание предмета
    public ObjectDescription ItemDescription;
    public int quantity;

    //public List<int> itemCount = new  List<int>();
    
    public GameObject itemDescriptionGO;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemQuantityText;
    [SerializeField] private Button _button;


    private void Start()
    {
    }

    public void PressOnItem()
    {
        Effector.instance.ApplyEffect(ItemDescription, false);
        InventoryController.instance.AddItem(ItemDescription, -1);
    }
    public void Initialize(int _q)
    {
        quantity = _q;
        itemImage.sprite = ItemDescription.objectSprite;
        itemQuantityText.text = _q.ToString();
        itemDescriptionText.text = "<b>"+ItemDescription.objectName[LocalizationManager.localeNum] +  "</b><br>" + ItemDescription.objectSpeech[LocalizationManager.localeNum];
        _button.interactable = ItemDescription.isUsable;
        
    }



}
