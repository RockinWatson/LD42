using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainFall : MonoBehaviour {

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * 1.5f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "RainCollider")
        {
            gameObject.SetActive(false);
        }
    }
}
