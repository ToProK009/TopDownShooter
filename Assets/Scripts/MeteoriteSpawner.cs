using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public GameObject meteoritePrefab;
    public float startMinInterval = 2f;
    public float startMaxInterval = 4f;
    public float startMinScale = 0.5f;
    public float startMaxScale = 2.5f;
    public float difficultyRate = 0.1f;

    private Camera cam;
    private float elapsed;

    void Start()
    {
        cam = Camera.main;
        difficultyRate = DifficultyManager.difficultyRate;
        Spawn();
    }

    void Spawn()
    {
        elapsed += 1;

        float t = elapsed * difficultyRate;

        float curMinInterval = Mathf.Max(0.15f, startMinInterval - t);
        float curMaxInterval = Mathf.Max(0.3f, startMaxInterval - t * 2f);

        if (curMaxInterval < curMinInterval)
            curMaxInterval = curMinInterval + 0.15f;

        float speedMult = 1f + t * 0.05f;
        float scale = Random.Range(startMinScale, startMaxScale);

        GameObject meteor = Instantiate(meteoritePrefab, Vector3.zero, Quaternion.identity);
        meteor.transform.localScale = Vector3.one * scale;
        meteor.transform.position = GetRandomEdgePosition(scale * 0.5f);
        meteor.GetComponent<Meteorite>().speedMultiplier = speedMult;

        float interval = Random.Range(curMinInterval, curMaxInterval);
        Invoke(nameof(Spawn), interval);
    }

    Vector3 GetRandomEdgePosition(float margin)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        int side = Random.Range(0, 4);
        float x = 0, y = 0;

        switch (side)
        {
            case 0: x = Random.Range(-camWidth, camWidth); y = camHeight + margin; break;
            case 1: x = Random.Range(-camWidth, camWidth); y = -camHeight - margin; break;
            case 2: x = -camWidth - margin; y = Random.Range(-camHeight, camHeight); break;
            case 3: x = camWidth + margin; y = Random.Range(-camHeight, camHeight); break;
        }

        return new Vector3(x, y, 0);
    }
}
