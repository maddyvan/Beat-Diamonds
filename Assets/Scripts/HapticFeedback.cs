using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticFeedback : MonoBehaviour
{

    public AudioClip hapticSFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LeftConroller") || other.CompareTag("RightController"))
        {
            ApplyHapticFeedback(OVRInput.Controller.LTouch);
            ApplyHapticFeedback(OVRInput.Controller.RTouch);
        }
    }

    public void ApplyHapticFeedback(OVRInput.Controller controller)
    {
        OVRHapticsClip hapticsClip = new OVRHapticsClip(hapticSFX);

        if(controller == OVRInput.Controller.LTouch)
        {
            OVRHaptics.LeftChannel.Preempt(hapticsClip);
        } else
        {
            OVRHaptics.RightChannel.Preempt(hapticsClip);
        }
    }

}
