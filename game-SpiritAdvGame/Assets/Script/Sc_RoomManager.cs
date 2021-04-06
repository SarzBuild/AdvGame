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
        virtualCamera = gameObject.transform.parent.GetChild(0).gameObject;
        player = GameObject.FindWithTag("Player");
        var vcam = gameObject.transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
        vcam.Follow = player.transform;
        vcam.LookAt = player.transform;
        virtualCamera.SetActive(false);
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
