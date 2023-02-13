using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] bool moveRight;

    private void Update() {
        if(Input.GetKey(KeyCode.A)){
            PlayerController.instance.moveLEFT();
        }else if(Input.GetKey(KeyCode.D)){
            PlayerController.instance.moveRight();
        }else{
            ResetPosition();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(moveRight) PlayerController.instance.moveRight();
        else PlayerController.instance.moveLEFT();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetPosition();
    }

    void ResetPosition(){
        PlayerController.instance.ResetPosition(
            (moveRight) ? Vector3.right : Vector3.left
        );
    }

}
