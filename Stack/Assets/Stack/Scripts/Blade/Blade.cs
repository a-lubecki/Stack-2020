using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;
    private bool slicing;

    public Vector3 direction { get; private set;}
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickInUpperScreen())
            {
                StartSlicing();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (IsClickInUpperScreen())
            {
                StopSlicing();
            }
        }
        else if (slicing)
        {
            if (IsClickInUpperScreen())
            {
                ContinueSlicing();
            }
        }
    }

    bool IsClickInUpperScreen()
    {
        // Obtener la posición del clic del mouse
        Vector2 clickPosition = Input.mousePosition;

        // Verificar si la posición del clic está en la parte superior de la pantalla
        return clickPosition.y >= (Screen.height / 2);
    }


    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;   

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;

    }
}
