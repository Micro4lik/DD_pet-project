using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseEffect : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    public float zOffset = -2f;
    public Vector2 tolerance = Vector2.one;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    private bool _over;

    void Update()
    {
        if (_over) this.OnPointerOver();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        _over = true;
        originalRotation = transform.rotation;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        _over = false;
        transform.rotation = originalRotation;
    }

    private void OnPointerOver()
    {
        //Debug.Log("OPA");
        Vector3 localOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // Multiplying by a adjustable tolerance, this is just a matter of preference if you want more rotation on the xAxis/yAxis or not.
        localOffset.x *= tolerance.x;
        localOffset.y *= tolerance.y;
        Vector3 worldOffset = transform.position + localOffset;

        // Setting a zOffset it will be really weird to adjust the rotation to look at something on the same Z as it.
        worldOffset.z = transform.position.z + zOffset;

        // Transform.LookAt for simplicity sake.
        transform.LookAt(worldOffset);
    }

}
