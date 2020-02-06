using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

    public static InventoryController instance;

    [SerializeField] private List<ObjectDescription> itemsLibrary = new List<ObjectDescription>();

    [SerializeField] private GameObject inventoryGrid;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<GameObject> inventorySlot = new List<GameObject>();
    [SerializeField] public Dictionary<ObjectDescription, int> inventoryItems = new Dictionary<ObjectDescription, int>();

    //public int Step;
    //bool _lerp;
    //Vector3 _lerp_target;
    bool isMoving = false;
    public GameObject qwe;
    [SerializeField] private int dummySlots;

    private int index = 0;

    const int INVENTORY_UI_SIZE = 6;

    public void AddItem(ObjectDescription itemOD, int quantity)
    {
        if (inventoryItems.ContainsKey(itemOD))
        {
            Debug.Log("Уже есть! " + itemOD.objectName[0] + " Меням количество " + quantity);
            inventoryItems[itemOD] += quantity;
            if (inventoryItems[itemOD]<=0)
            {
                inventoryItems.Remove(itemOD);
                changeIndex(0);
            }
        }
        
        else
        {
            Debug.Log("Ещё нет, добавляю");
            inventoryItems.Add(itemOD, quantity);
        }
        RefreshInventory();
    }

    public string StealRandomItem()
    {
        int _rndTmp = Random.Range(0, inventoryItems.Count);
        string _str = inventoryItems.ElementAt(_rndTmp).Key.objectName[LocalizationManager.localeNum];
        AddItem(inventoryItems.ElementAt(_rndTmp).Key, -1);
        return _str;
    }

    public void ItemChangeAmount(ObjectDescription itemOD, int v)
    {
        inventoryItems[itemOD] += v;
    }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //dummySlots = 0;
        
        for (int i = 0; i < INVENTORY_UI_SIZE; i++)
        {
            GameObject _go = Instantiate(itemPrefab, inventoryGrid.transform);
            inventorySlot.Add(_go);
        }
        RefreshInventory();

        foreach (var it in itemsLibrary)
        {
            AddItem(it, 1);
        }
        /*
        for (int i = 0; i < 30; i++)
        {
            AddItem(itemsLibrary[Random.Range(0, itemsLibrary.Count)],1);
        }*/

    }

    public void AddDummySlots()
    {

    }


    public void RefreshInventory()
    {
        int endIndex = 0;
        if (inventoryItems.Count < INVENTORY_UI_SIZE)
        {
            endIndex = inventoryItems.Count;
            //AddDummySlots();
        }
        else
        {
            endIndex = INVENTORY_UI_SIZE;
        }

        for (int i = 0; i < endIndex; i++)
        {
            Item tmpItem = inventorySlot[i].GetComponent<Item>();
            //Debug.Log("Смотрим на элемент " + (i + index) + " из " + inventoryItems.Count);
            tmpItem.ItemDescription = inventoryItems.ElementAt(i+index).Key;

            
            tmpItem.Initialize(inventoryItems.ElementAt(i + index).Value);
            inventorySlot[i].SetActive(true);
        }
        for (int i = endIndex; i < INVENTORY_UI_SIZE; i++)
        {
            inventorySlot[i].SetActive(false);
        }
    }

    public void changeIndex(int _ch)
    {
        //Debug.Log("index changing to " + _ch);
        index += _ch;
        if (index > inventoryItems.Count - INVENTORY_UI_SIZE-1)
        {
            index = inventoryItems.Count - INVENTORY_UI_SIZE-1;
        }
        else if (index < 0)
        {
            index = 0;
        }
        else if (inventoryItems.Count < INVENTORY_UI_SIZE)
        {
            index = 0;
        }

        if (index < 0)
        {
            index = 0;
        }
        RefreshInventory();
    }

    public void NextScreen()
    {
        if (!isMoving)
        {
            isMoving = true;

            LeanTween.moveX(qwe, 3f, 0.5f).setEaseInCubic();

            //int CurrentItem = index + 1;
            //CurrentPage = _currentPage + Step;

            //LeanTween.moveLocalX(qwe, 3f, 2f).setEaseInOutCubic;

            //LeanTween.moveLocalY(followArrow.gameObject, Random.Range(-100f, 100f), 0f);
        }

    }
    
}
