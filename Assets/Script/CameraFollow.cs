using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Update() {

        transform.position = new Vector3(
            transform.position.x,
            player.position.y,
            transform.position.z
        );

    }
}
