using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondBehavior : MonoBehaviour
{
    [Header ("Controllers")]
    public OVRInput.Controller leftController;
    public OVRInput.Controller rightController;

    [Header("Audio Clips")]
    public AudioClip validHitSFX;
    public AudioClip invalidHitSFX;

    [Header("Colors")]
    public Color leftColor;
    public Color rightColor;

    [Header("Broken Pieces")]
    public GameObject smDiamondPieces;
    public GameObject mdDiamondPieces;
    public GameObject lgDiamondPieces;

    [Header("Other Info")]
    public bool hitWithRightController = true;
    public string sceneToLoad;
    public LevelManager levelManager;

    private float explosionForce = 150f;
    private int pointWorth = 50;
    private float explosionRadius = 2f;

    private void Start()
    {
        if (levelManager.IsPlayingGame())
        {
            SetColor();
        }
    }

    private void SetColor()
    {
        if (hitWithRightController)
        {
            GetComponent<Renderer>().material.color = rightColor;
        }
        else
        {
            GetComponent<Renderer>().material.color = leftColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckHitWithController(other))
        {
            if (levelManager.IsPlayingGame())
            {
                HandleGameCollision(other);
            } else
            {
                HandleMenuCollision(other);
            }
        }
    }

    private void HandleMenuCollision(Collider other)
    {
        InstantiateDiamondPieces(smDiamondPieces, explosionForce);
        levelManager.LoadScene(sceneToLoad);
    }

    private void HandleGameCollision(Collider other)
    {
        if (CheckHitWithRightController(other))
        {
            AudioSource.PlayClipAtPoint(validHitSFX, transform.position);
            float velocityMagnitude = CalculateHitMagnitude();
            if (velocityMagnitude > 2)
            {
                levelManager.UpdateScoreBy(pointWorth + 20);
                InstantiateDiamondPieces(lgDiamondPieces, explosionForce * 2f);
            }
            else if (velocityMagnitude > 1)
            {
                levelManager.UpdateScoreBy(pointWorth + 10);
                InstantiateDiamondPieces(mdDiamondPieces, explosionForce * 1.5f);
            } else {
                levelManager.UpdateScoreBy(pointWorth);
                InstantiateDiamondPieces(smDiamondPieces, explosionForce);
            }
        } else
        {
            AudioSource.PlayClipAtPoint(invalidHitSFX, transform.position);
            levelManager.UpdateScoreBy(-pointWorth);
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, 2, 1) * explosionForce);
            Destroy(gameObject, 1);
        }
    }

    private void InstantiateDiamondPieces(GameObject pieces, float force)
    {
        // Get original properties
        Transform diamondTransform = transform;
        Color diamondColor = GetComponent<Renderer>().material.color;

        // Instantiate pieces
        GameObject diamondPieces = Instantiate(pieces, diamondTransform.position , diamondTransform.rotation);
        
        Destroy(gameObject);
        
        // Set color
        Renderer[] diamondRenderers = diamondPieces.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in diamondRenderers)
        {
            r.material.color = diamondColor;
        }
        
        // Apply Force
        Rigidbody[] diamondRbPieces = diamondPieces.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in diamondRbPieces)
        {
            rb.AddExplosionForce(force, diamondTransform.position, explosionRadius);
        }
        Destroy(diamondPieces, 1);
    }

    private bool CheckHitWithController(Collider other)
    {
        if (other.CompareTag("LeftController") && OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).magnitude > 0.5) {
            other.GetComponent<HapticFeedback>().ApplyHapticFeedback(leftController);
            return true;
        } else if (other.CompareTag("RightController") && OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).magnitude > 0.5)
        {
            other.GetComponent<HapticFeedback>().ApplyHapticFeedback(rightController);
            return true;
        } else
        {
            return false;
        }
    }

    private bool CheckHitWithRightController(Collider other)
    {
        if (hitWithRightController)
        {
            return other.CompareTag("RightController");
        } else
        {
            return other.CompareTag("LeftController");
        }
    }

    private float CalculateHitMagnitude()
    {
        if (hitWithRightController)
        {
            return OVRInput.GetLocalControllerVelocity(rightController).magnitude;
        }
        else
        {
            return OVRInput.GetLocalControllerVelocity(leftController).magnitude;
        }
    }
}
