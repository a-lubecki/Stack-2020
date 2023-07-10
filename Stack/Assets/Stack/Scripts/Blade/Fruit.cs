using UnityEngine;
using Lean.Pool;
public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;
  
    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;
    public GameObject explosion;
    
    public int points = 1;

    private void Awake()
    {
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

        foreach(Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("missile"))
        {
            GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
            LeanPool.Despawn(other);
            Destroy(explosionInstance, 1f);
        }

        else
        {
            GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
            LeanPool.Despawn(other);
            Destroy(explosionInstance, 1f);
        }

    }
}
