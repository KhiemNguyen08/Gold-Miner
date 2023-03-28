using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public float speed;
    bool moveLeft;
    public float firstLocation;
    public float lastLocation;
    void Start()
    {
        moveLeft = true;
    }
    void Update()
    {
        flip();
    }
    // di chuyển con chuột chạy
    void flip()
    {
        if (moveLeft == true)
        {
            transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= firstLocation)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                moveLeft = false;
            }
        }
        if (moveLeft == false)
        {
            transform.position = transform.position + Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= lastLocation)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                moveLeft = true;
            }
        }
    }
}
