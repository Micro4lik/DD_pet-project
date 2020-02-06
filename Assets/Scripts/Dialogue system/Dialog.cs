using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dialog", menuName = "Dialog/create new Dialog")]
public class Dialog : ScriptableObject
{
    public int dialogId;
    public string dialogName;
    public List<Replica> replicaList = new List<Replica>();

}
