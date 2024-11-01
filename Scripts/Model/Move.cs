using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool isMoving = false;
    public float speed = 200;
    public Vector3 destination;
    public Vector3 mid_destination;
    // Update is called once per frame
    void Update()
    {

        if (transform.localPosition == destination)
        {
            isMoving = false;
            return;
        }
        isMoving = true;
        // mid指的是到船上空了要下降了
        if (transform.localPosition.x != destination.x && transform.localPosition.y != destination.y)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, mid_destination, speed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);
        }
    }
}