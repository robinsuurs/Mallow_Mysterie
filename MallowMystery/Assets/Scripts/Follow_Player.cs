using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 CameraOffset;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null) {
            transform.position = player.transform.position + CameraOffset;
        }
    }
}
