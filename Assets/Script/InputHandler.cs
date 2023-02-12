using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] bool moveRight;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(moveRight) PlayerController.instance.moveRight();
        else PlayerController.instance.moveLEFT();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // PlayerController.instance.ResetPosition(
        //     (moveRight) ? Vector3.right : Vector3.left
        // );
    }

}
