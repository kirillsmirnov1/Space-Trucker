using System.Linq;
using UnityEngine;

namespace Trucker.View.Util
{
    [RequireComponent(typeof(MeshCollider))]
    public class AddInvertedMeshCollider : MonoBehaviour
    {
        public bool runOnAwake = false;
        private Mesh _mesh;

        private void Awake()
        {
            if(runOnAwake) CreateInvertedMeshCollider();
        }
    
        public void CreateInvertedMeshCollider()
        {
            InvertMesh();
            SetCollider();
        }

        private void InvertMesh()
        {
            var meshFilter = GetComponent<MeshFilter>();
            var defaultMesh = meshFilter.mesh;
            _mesh = new Mesh
            {
                name = "inverted sphere mesh",
                vertices = defaultMesh.vertices,
                normals = defaultMesh.normals,
                triangles = defaultMesh.triangles.Reverse().ToArray()
            };
            meshFilter.mesh = _mesh;
        }

        private void SetCollider()
        {
            GetComponent<MeshCollider>().sharedMesh = _mesh;
        }
    }
}