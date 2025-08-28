using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;  // Prefab containing Top + Bottom Pipe
    public float spawnRate = 2f;
    public float pipeGap = 2.5f; // Distance between bottom and top
    public float minY = -1f; // Lowest possible bottom pipe position
    public float maxY = 2f;  // Highest possible bottom pipe position

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnPipe();
            timer = 0;
        }
    }

    void SpawnPipe()
    {
        // Choose a random height for the bottom pipe
        float y = Random.Range(minY, maxY);

        // Spawn Pipe Pair
        GameObject newPipe = Instantiate(pipePrefab, new Vector3(5, y, 0), Quaternion.identity);

        // Fix pipe gap manually (if prefab is just one pipe)
        Transform topPipe = newPipe.transform.Find("TopPipe");
        Transform bottomPipe = newPipe.transform.Find("BottomPipe");

        if (topPipe != null && bottomPipe != null)
        {
            // Place bottom pipe at chosen y
            bottomPipe.localPosition = new Vector3(0, 0, 0);

            // Place top pipe above with gap
            topPipe.localPosition = new Vector3(0, pipeGap, 0);
        }
    }
}
