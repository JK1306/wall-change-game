using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float verticalMovementSpeed;
    [SerializeField] float thresholdEndPoint;
    [SerializeField] Vector3 movingDirection;
    [SerializeField] ParticleSystem playerDeathBurst;
    public delegate void MOVERIGHT();
    public delegate void MOVELEFT();
    public MOVERIGHT moveRight;
    public MOVELEFT moveLEFT;
    public static PlayerController instance;
    public bool playerIsAlive;

    void Start()
    {
        instance = this;
        thresholdEndPoint = 1.5f;
        moveRight  = MoveRight;
        moveLEFT = MoveLeft;
        movingDirection = Vector3.up;
        playerIsAlive = true;
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement(){
        transform.Translate(Vector3.up * Time.deltaTime * movementSpeed);
        if((transform.position.x < thresholdEndPoint && movingDirection == Vector3.right) || (transform.position.x > -thresholdEndPoint && movingDirection == Vector3.left)){
        // if(transform.position.x <= thresholdEndPoint && transform.position.x >= -thresholdEndPoint && movingDirection != Vector3.up){
            transform.Translate(movingDirection * Time.deltaTime * movementSpeed);
            // ResetPosition(movingDirection);
        }
    }

    void MoveRight(){
        movingDirection = Vector3.right;
        // Debug.Log("Direction : "+movingDirection);
    }

    void MoveLeft(){
        movingDirection = Vector3.left;
        // Debug.Log("Direction : "+movingDirection);
    }

    public void ResetPosition(Vector3 movementDirection){
        if(movingDirection == movementDirection){
            movingDirection = Vector3.up;
        }
    }

    public void IncreseMovementSpeed(float speedIncreaseRate){
        movementSpeed += speedIncreaseRate;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy"){
            playerIsAlive = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            movementSpeed = 0f;
            GameController.instance.PlayDeathSFX();
            playerDeathBurst.Play();
            StartCoroutine(DestroyDeathParticle());
            GameController.instance.GameOver();
        }
    }

    IEnumerator DestroyDeathParticle(){
        yield return new WaitForSeconds(5);
        Destroy(playerDeathBurst.gameObject);
    }

}
