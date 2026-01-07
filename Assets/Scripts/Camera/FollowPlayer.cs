using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0, -15);
    private float dampTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        TrackPlayer();
        
    }

    void TrackPlayer()
    {
        Vector3 targetPos = player.position + offset; //add offset on z postion
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, dampTime);
    }
}
