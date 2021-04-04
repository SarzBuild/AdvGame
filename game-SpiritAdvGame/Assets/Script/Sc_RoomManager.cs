using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Sc_RoomManager : MonoBehaviour
{
    public GameObject virtualCamera;
    public GameObject player;
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        virtualCamera = this.gameObject.transform.GetChild(0).gameObject;
        player = GameObject.FindWithTag("Player");
        var vcam = this.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        vcam.Follow = player.transform;
        vcam.LookAt = player.transform;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamera.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamera.SetActive(false);
        }
    }
}
