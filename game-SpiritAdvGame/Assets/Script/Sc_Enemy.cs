using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Enemy : MonoBehaviour
{
    Sc_SelectChildObject test;
    // Start is called before the first frame update
    void Start()
    {
        test = FindObjectOfType<Sc_SelectChildObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(test.isSpirit);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hitbox")
        {
            Destroy(gameObject);
        }
    }
}
