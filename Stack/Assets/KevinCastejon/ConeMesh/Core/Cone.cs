using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KevinCastejon.ConeMesh
{
    public enum ConeOrientation
    {
        X,
        Y,
        Z
    }

    /// <summary>
    /// Generate a cone mesh, renderer and collider, on the fly.
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class Cone : MonoBehaviour
    {
        [SerializeField]
        private bool _pivotAtTop = true;
        [SerializeField]
        private ConeOrientation _orientation = ConeOrientation.Z;
        [SerializeField]
        private bool _invertDirection;
        [SerializeField]
        private bool _isTrigger;
        [SerializeField]
        private Material _material;
        [Min(3)]
        [SerializeField]
        private int _coneSides = 25;
        [SerializeField]
        private bool _proportionalRadius;
        [Min(float.Epsilon)]
        [SerializeField]
        private float _coneRadius = 0.5f;
        [SerializeField]
        private float _coneHeight = 1f;

        private Mesh _coneMesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private MeshCollider _meshCollider;

        public bool PivotAtTop { get => _pivotAtTop; set { _pivotAtTop = value; GenerateCone(); } }
        public Material Material { get => _material; set { _material = value; _meshRenderer = _meshRenderer ? _meshRenderer : gameObject.GetComponent<MeshRenderer>(); _meshRenderer.material = _material; } }
        public int ConeSubdivisions { get => _coneSides; set { _coneSides = value; GenerateCone(); } }
        public float ConeRadius { get => _coneRadius; set { _coneRadius = value; GenerateCone(); } }
        public float ConeHeight { get => _coneHeight; set { _coneHeight = value; GenerateCone(); } }
        public ConeOrientation Orientation { get => _orientation; set { _orientation = value; GenerateCone(); } }
        public bool IsConeGenerated { get => _coneMesh != null; }
        public bool IsTrigger
        {
            get => _isTrigger; set
            {
                _isTrigger = value; _meshCollider = _meshCollider ? _meshCollider : gameObject.GetComponent<MeshCollider>();
                if (_isTrigger)
                {
                    _meshCollider.convex = true;
                }
                _meshCollider.isTrigger = value;
            }
        }
        public bool ProportionalRadius { get => _proportionalRadius; set { _proportionalRadius = value; GenerateCone(); } }

        internal void GenerateCone()
        {
            _coneMesh = CreateConeMesh(_coneSides + 1, _coneRadius, _coneHeight, _pivotAtTop, _orientation, _invertDirection, _proportionalRadius);
            _meshFilter = _meshFilter ? _meshFilter : gameObject.GetComponent<MeshFilter>();
            _meshRenderer = _meshRenderer ? _meshRenderer : gameObject.GetComponent<MeshRenderer>();
            _meshCollider = _meshCollider ? _meshCollider : gameObject.GetComponent<MeshCollider>();

            _meshFilter.sharedMesh = _coneMesh;

            _meshRenderer.additionalVertexStreams = _coneMesh;
            _meshRenderer.material = _material;
            _meshCollider.sharedMesh = _coneMesh;
            _meshCollider.convex = true;
            _meshCollider.isTrigger = _isTrigger;
        }

        private static Mesh CreateConeMesh(int subdivisions, float radius, float height, bool pivotAtTop, ConeOrientation orientation, bool invertDirection, bool proportionalRadius)
        {
            if (proportionalRadius)
            {
                radius *= height;
            }
            if (invertDirection)
            {
                height = -height;
            }
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[subdivisions + 2];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[(subdivisions * 2) * 3];
            if (orientation == ConeOrientation.X)
            {
                vertices[0] = pivotAtTop ? Vector3.right * height : Vector3.zero;
            }
            else if (orientation == ConeOrientation.Y)
            {
                vertices[0] = pivotAtTop ? Vector3.up * height : Vector3.zero;
            }
            else
            {
                vertices[0] = pivotAtTop ? Vector3.forward * height : Vector3.zero;
            }
            uv[0] = new Vector2(0.5f, 0f);
            for (int i = 0, n = subdivisions - 1; i < subdivisions; i++)
            {
                float ratio = (float)i / n;
                float r = ratio * (Mathf.PI * 2f);
                float x = Mathf.Cos(r) * radius;
                float z = Mathf.Sin(r) * radius;
                if (orientation == ConeOrientation.X)
                {
                    vertices[i + 1] = new Vector3(pivotAtTop ? height : 0f, x, z);
                }
                else if (orientation == ConeOrientation.Y)
                {
                    vertices[i + 1] = new Vector3(x, pivotAtTop ? height : 0f, z);
                }
                else
                {
                    vertices[i + 1] = new Vector3(x, z, pivotAtTop ? height : 0f);
                }
                uv[i + 1] = new Vector2(ratio, 0f);
            }
            if (orientation == ConeOrientation.X)
            {
                vertices[subdivisions + 1] = !pivotAtTop ? Vector3.right * height : Vector3.zero;
            }
            else if (orientation == ConeOrientation.Y)
            {
                vertices[subdivisions + 1] = !pivotAtTop ? Vector3.up * height : Vector3.zero;
            }
            else
            {
                vertices[subdivisions + 1] = !pivotAtTop ? Vector3.forward * height : Vector3.zero;
            }
            uv[subdivisions + 1] = new Vector2(0.5f, 1f);

            // base

            for (int i = 0, n = subdivisions - 1; i < n; i++)
            {
                int offset = i * 3;
                triangles[offset] = 0;
                triangles[offset + 1] = i + 1;
                triangles[offset + 2] = i + 2;
            }

            // sides

            int bottomOffset = subdivisions * 3;
            for (int i = 0, n = subdivisions - 1; i < n; i++)
            {
                int offset = i * 3 + bottomOffset;
                triangles[offset] = i + 1;
                triangles[offset + 1] = subdivisions + 1;
                triangles[offset + 2] = i + 2;
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}
