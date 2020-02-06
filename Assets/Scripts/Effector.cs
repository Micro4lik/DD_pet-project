using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс применяет различные эффекты от карт способностей и тп
/// </summary>
public class Effector : MonoBehaviour
{
    public static Effector instance;
    public List<Dialog> dialogLibrary;

    void Awake()
    {
        instance = this;
    }

    public void ApplyEffect(ObjectDescription _od, bool _wasItPair )
    {
        string _effectName = _od.objectName[0];

        if (_od.tileType == ObjectDescription.TileType.Treasure)
        {
            switch (_effectName)
            {
                case "Food":
                    Debug.Log("Applying effect: " + _effectName);
                    Player.instance.TakeDamageHunger((int)(Player.instance.GetMaxHunger() * -0.8f));
                    HeroReplica.instance.ShowReplica("Tasty!");
                    break;
                case "Health potion":
                    Debug.Log("Applying effect: " + _effectName);
                    Player.instance.TakeDamageHp((int)(Player.instance.GetMaxHp() * -0.5f));
                    HeroReplica.instance.ShowReplica("Much better...");
                    break;
                case "Orc Shaman":
                    DialogRunner.instance.StartDialog(dialogLibrary.Find(x => x.dialogName == "Orc Shaman"));
                    break;
                case "Strawberry":
                    Debug.Log("Applying effect: " + _effectName);
                    Player.instance.TakeDamageHp((int)(Player.instance.GetMaxHp() * -0.2f));
                    break;
                case "Red berry":
                    Debug.Log("Applying effect: " + _effectName);
                    Player.instance.TakeDamageHp((int)(Player.instance.GetMaxHp() * -0.2f));
                    Player.instance.TakeDamageHunger((int)(Player.instance.GetMaxHunger() * -0.2f));
                    break;
            }
        }

        if (_wasItPair == false)
        {
            if (_od.tileType == ObjectDescription.TileType.Monster)
            {
                switch (_effectName)
                {
                    case "Raven":
                        Debug.Log("Applying effect: " + _effectName);

                        if (InventoryController.instance.inventoryItems.Count > 0)
                        {
                            string _itemName = InventoryController.instance.StealRandomItem();
                            HeroReplica.instance.ShowReplica("That thing stole<br>my "+ _itemName.ToLower());
                        }
                        else
                        {
                            Player.instance.TakeDamageHp(_od.attack);
                        }
                        break;
                    default:
                        Player.instance.TakeDamageHp(_od.attack);
                        break;
                }
            }
        }
    }
}
