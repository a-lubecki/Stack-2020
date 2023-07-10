using System.Collections;
using UnityEngine;
using Lean.Pool;
public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    [SerializeField] private LeanGameObjectPool poolBlocks;
    [SerializeField] private LeanGameObjectPool poolMissiles;
    public GameObject bombPrefab;
    [SerializeField] private Transform trBlocks;
    [SerializeField] private Transform trBlocks2;
    [Range(0f,1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;


    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
       StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.5f);

        while (enabled)
        {
            GameObject prefab = poolBlocks.Spawn(Vector3.zero, Quaternion.identity, trBlocks, false);

            Vector3 spawnPosition = trBlocks2.position;

            GameObject missileTemp = poolMissiles.Spawn(spawnPosition, Quaternion.identity, trBlocks2, false);

            missileTemp.GetComponent<MissileBehaviour>().target = trBlocks;





            if (Random.value < bombChance)
            {
                prefab = bombPrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));  

            GameObject fruit = Instantiate(prefab, position, rotation);
            // Destroy(fruit, maxLifetime);
            LeanPool.Despawn(prefab);
          
            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
    
}
