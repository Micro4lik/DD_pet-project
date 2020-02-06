using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopupPause : MonoBehaviour
{
    private bool popupIsShowing = false;
    private bool settingsIsShowing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenPausePopup()
    {
        popupIsShowing = true;
    }

    public void ClosePausePopup()
    {
        popupIsShowing = false;
    }


}
