using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform LookAt;

    public GameObject topDownCam;

    public CameraStyle curremtStyle;

    public enum CameraStyle
    {
        Topdown
    }

    private void Start()
    {


    }

    private void Update()
    {

        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // rotate player object
        if (curremtStyle == CameraStyle.Topdown)
        {
            Vector3 dirToLookAt = LookAt.position - new Vector3(transform.position.x, LookAt.position.y, transform.position.z);
            orientation.forward = dirToLookAt.normalized;

            playerObj.forward = dirToLookAt.normalized;

        }

    }

}
