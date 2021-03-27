using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SelectChildObject : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Legs")
        {
            mySpriteRenderer.enabled = false;
            Destroy(collision.gameObject);
            transform.Find("PlayerC").gameObject.SetActive(false);
            transform.Find("PlayerB").gameObject.SetActive(false);
            transform.Find("PlayerA").gameObject.SetActive(true);
            
        }
        if (collision.name == "Body" && transform.Find("PlayerA").gameObject.activeSelf)
        {
            transform.Find("PlayerA").gameObject.SetActive(false);
            transform.Find("PlayerC").gameObject.SetActive(false);
            transform.Find("PlayerB").gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.name == "Helmet" && transform.Find("PlayerB").gameObject.activeSelf)
        {
            transform.Find("PlayerA").gameObject.SetActive(false);
            transform.Find("PlayerB").gameObject.SetActive(false);
            transform.Find("PlayerC").gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}

