using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{

    public Color triggerEnteredColor;
    public Color triggerExitedColor;
    public Renderer doorRenderer;
    public HapticFeedback hapticFeedback;
    public LevelManager levelManager;
    private int decrementScore = 100;

    private void Start()
    {
        if (levelManager == null)
        {
            levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            hapticFeedback.ApplyHapticFeedback(OVRInput.Controller.LHand);
            hapticFeedback.ApplyHapticFeedback(OVRInput.Controller.RHand);
            doorRenderer.material.color = triggerEnteredColor;
            //levelManager.UpdateScoreBy(-100);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {

            doorRenderer.material.color = triggerExitedColor;
            levelManager.UpdateScoreBy(-100);
            hapticFeedback.ApplyHapticFeedback(OVRInput.Controller.LHand);
            hapticFeedback.ApplyHapticFeedback(OVRInput.Controller.RHand);

        }
    }
}
