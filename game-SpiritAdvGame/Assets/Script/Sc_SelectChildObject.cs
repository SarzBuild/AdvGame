using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SelectChildObject : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    public bool isSpirit = true;
    public bool hasLegs = false;
    public bool hasArms = false;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Legs")
        {
            isSpirit = false;
            hasLegs = true;
            mySpriteRenderer.enabled = false;
            Destroy(collision.gameObject);
            transform.Find("PlayerB").gameObject.SetActive(false);
            transform.Find("PlayerA").gameObject.SetActive(true);
            
        }
        if (collision.name == "Body" && transform.Find("PlayerA").gameObject.activeSelf)
        {
            hasArms = true;
            transform.Find("PlayerA").gameObject.SetActive(false);
            transform.Find("PlayerB").gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}

