using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public PlayerScript player;
    public float spawnRate = 5;
    private float spawnTimer = 0;
    public float spawnRadius = 10f;
    public int maxEnemySpawn = 2;
    public int maxTotalEnemy = 0;

    // Update is called once per frame
    void Update()
    {
        if (!player.isDead && maxTotalEnemy < 20)
        {
            spawnEnemies();
        }
    }
    private Vector3 spawnInRadius(float radius)
    {
        Vector3 randomPos = Random.insideUnitSphere * radius;
        randomPos += transform.position;
        randomPos.y = 0f;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
        randomPos.y = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.y;
        randomPos.z = transform.position.z;
        return randomPos;
    }
    private void spawnEnemies()
    {
        int current = 0;
        if (spawnTimer < spawnRate)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            while (current < maxEnemySpawn && maxTotalEnemy <= 20)
            {
                Instantiate(enemy, spawnInRadius(spawnRadius), transform.rotation);
                spawnTimer = 0;
                current++;
                maxTotalEnemy++;
            }
            current = 0;
        }
    }
}
