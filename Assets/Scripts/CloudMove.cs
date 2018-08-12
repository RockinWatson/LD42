using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {

    [SerializeField]
    private float CloudSpeed;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * CloudSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CloudCollider")
        {
            gameObject.SetActive(false);
        }
    }
}
