using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnPoint : MonoBehaviour
{

    public Transform baseSpawnPoint;
    public float maxXOffset = 0.5f;
    public float maxYOffset = 0.5f;
    public float speedMultiplier = 7f;

    private float minScaleY = 0.8f;
    private float minScaleX = 0.5f;
    private float maxScaleY = 1.3f;
    private float maxScaleX = 1.5f;

    public GameObject blockPrefab;
    public LevelManager levelManager;
    public float spawnRate = 0.1f;

    public void SpawnBlock()
    {
        if (!levelManager.IsGameOver() && (Random.value <= spawnRate))
        {
            float xOffset = Random.Range(-maxXOffset, maxXOffset);
            float yOffset = Random.Range(-maxYOffset - 0.8f, maxYOffset);
            float xScale = Random.Range(minScaleX, maxScaleX);
            float yScale = Random.Range(minScaleY, maxScaleY);

            Transform spawnPosition = transform;

            GameObject newBlock = Instantiate(blockPrefab, spawnPosition.position + new Vector3(xOffset, yOffset), spawnPosition.rotation);
            newBlock.gameObject.transform.localScale = new Vector3(xScale, yScale, blockPrefab.transform.localScale.z);

            Rigidbody[] blockRbs = newBlock.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in blockRbs)
            {
                rb.velocity += Vector3.back * speedMultiplier;
            }
        }
    }
}
