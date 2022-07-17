using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPunchable : MonoBehaviour
{

    [Header("Controller Configs")]
    public OVRInput.Controller leftController;
    public OVRInput.Controller rightController;
    public float controllerVelocityThreshold = 0.5f;

    [Header("Explosion Configs")]
    public GameObject brokenPiecesPrefab;
    public float explosionForce = 150f;
    public float explosionRadius = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (CheckHitWithController(other))
        {
            // TO-DO
        }
    }

    private bool CheckHitWithController(Collider other)
    {
        if (other.CompareTag("LeftController") && OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).magnitude > controllerVelocityThreshold)
        {
            other.GetComponent<HapticFeedback>().ApplyHapticFeedback(leftController);
            return true;
        }
        else if (other.CompareTag("RightController") && OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).magnitude > controllerVelocityThreshold)
        {
            other.GetComponent<HapticFeedback>().ApplyHapticFeedback(rightController);
            return true;
        }
        else
        {
            return false;
        }
    }
}
