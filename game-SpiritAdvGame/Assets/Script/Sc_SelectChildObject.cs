using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SelectChildObject : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Sc_PlayerControler Sc_PlayerControler;
    public Sc_PlayerState playerState;
    public bool isSpirit = true;
    public bool hasLegs = false;
    public bool hasArms = false;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        playerState = FindObjectOfType<Sc_PlayerState>();
        Sc_PlayerControler = FindObjectOfType<Sc_PlayerControler>();
        isSpirit = true;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Legs")
        {
            isSpirit = false;
            hasLegs = true;
            playerState.playerState = 1;
            mySpriteRenderer.enabled = false;
            Destroy(collision.gameObject);
            transform.Find("PlayerB").gameObject.SetActive(false);
            transform.Find("PlayerA").gameObject.SetActive(true);
        }
        if (collision.name == "Body" && transform.Find("PlayerA").gameObject.activeSelf)
        {
            hasArms = true;
            playerState.playerState = 2;
            transform.Find("PlayerA").gameObject.SetActive(false);
            transform.Find("PlayerB").gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}

