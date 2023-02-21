using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] MovementDirection movementDirection;

    private void Update() {
        // Debug.Log("anyKey : "+Input.anyKey);
        Debug.Log(Input.anyKey);
        KeyBoardInputHandler();
    }

    void KeyBoardInputHandler(){
        if(Input.GetKey(KeyCode.A)){
            PlayerController.instance.moveLEFT();
        }else if(Input.GetKey(KeyCode.D)){
            PlayerController.instance.moveRight();
        }

        if(!Input.anyKey){
            Debug.Log("Any ket down is called");
            ResetPosition();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(movementDirection == MovementDirection.Right) PlayerController.instance.moveRight();
        else PlayerController.instance.moveLEFT();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetPosition();
    }

    void ResetPosition(){
        PlayerController.instance.ResetPosition(
            (movementDirection == MovementDirection.Right) ? Vector3.right : Vector3.left
        );
    }

}

public enum MovementDirection{
    Right,
    Left
}