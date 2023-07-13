using UnityEngine;
using Lean.Pool;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;
    [SerializeField] private LeanGameObjectPool poolEnemy;
    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;
    public GameObject explosion;
    [SerializeField] private Transform trBlocks;
    [SerializeField] private Transform enemyInit;
   
    private GameObject newFruit;
    public bool awake;
    private void Awake()
    {
        awake=false;
        //enemyInit.position= new Vector3(0f, 0f, 0f);
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();

    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        //FindObjectOfType<GameManager>().IncreaseScore(points);

        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;
        juiceParticleEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Blocks"))
        {
           
            GameObject explosionInstance2 = Instantiate(explosion, transform.position, Quaternion.identity);
            awake=true;
            Destroy(explosionInstance2, 1f);
        }
        else
        {
            GameObject explosionInstance3 = Instantiate(explosion, transform.position, Quaternion.identity);
            //LeanPool.Despawn(other);
            Destroy(explosionInstance3, 1f);
        }

    }


}
