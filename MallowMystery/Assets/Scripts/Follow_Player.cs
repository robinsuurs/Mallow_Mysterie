using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 CameraOffset;
    [SerializeField] private bool followPlayer;
    
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null) {
            transform.position = player.transform.position + CameraOffset;
        }
    }

    public void setFollowPlayer() {
        if (followPlayer) {
            this.player = GameObject.FindWithTag("Player");
        }
    }
}
