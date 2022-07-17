using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawnPoint : MonoBehaviour
{
    public Transform baseSpawnPoint;
    public float maxXOffset = 0.5f;
    public float maxYOffset = 0.5f;
    public float speedMultiplier = 7f;

    public GameObject leftDiamond;
    public GameObject rightDiamond;

    public LevelManager levelManager;

    public void SpawnDiamond()
    {
        if (!levelManager.IsGameOver())
        {
            bool spawnHitWithRight = (Random.value > 0.5f);
            Transform spawnPosition = baseSpawnPoint;

            float xOffset = Random.Range(-maxXOffset, maxXOffset);
            float yOffset = Random.Range(-maxYOffset, maxYOffset + 0.4f);


            if (spawnHitWithRight)
            {
                GameObject newDiamond = Instantiate(rightDiamond, spawnPosition.position + new Vector3(xOffset, yOffset), spawnPosition.rotation);
                newDiamond.SetActive(true);
                newDiamond.GetComponent<Rigidbody>().velocity += Vector3.back * speedMultiplier;
            }
            else
            {
                GameObject newDiamond = Instantiate(leftDiamond, spawnPosition.position + new Vector3(xOffset, yOffset), spawnPosition.rotation);
                newDiamond.SetActive(true);
                newDiamond.GetComponent<Rigidbody>().velocity += Vector3.back * speedMultiplier;
            }
        }
    }
}
