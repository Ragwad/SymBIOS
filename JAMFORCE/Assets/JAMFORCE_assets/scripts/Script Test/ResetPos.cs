using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{
    Vector3 defaultPos;

    private void Awake()
    {
        defaultPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.MovePosition(defaultPos);
            rb.velocity = default;
        }
    }
}
