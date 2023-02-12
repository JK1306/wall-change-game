using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offsetDistance;

    private void Update() {

        transform.position = new Vector3(
            (transform.position.x + offsetDistance.x),
            (player.position.y + offsetDistance.y),
            (transform.position.z + offsetDistance.z)
        );

    }
}
