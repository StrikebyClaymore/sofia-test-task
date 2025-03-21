using UnityEngine;

namespace SofiaTestTask.Utility
{
    public class MeshBaker : MonoBehaviour
    {
        [SerializeField] private MeshFilter[] _meshes;
        [SerializeField] private bool _addMeshCollider;
        
        [ContextMenu("Bake")]
        public void Bake()
        {
            if(_meshes.Length == 0)
                _meshes = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[_meshes.Length];
            Matrix4x4 parentTransform = transform.worldToLocalMatrix;

            for (int i = 0; i < _meshes.Length; i++)
            {
                combine[i].mesh = _meshes[i].sharedMesh;
                combine[i].transform = parentTransform * _meshes[i].transform.localToWorldMatrix;
                _meshes[i].gameObject.SetActive(false);
            }
            Mesh combinedMesh = new Mesh();
            combinedMesh.CombineMeshes(combine, true, true);
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            if (meshFilter == null) meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.mesh = combinedMesh;
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer == null) meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = _meshes[0].GetComponent<MeshRenderer>().sharedMaterial;
            gameObject.SetActive(true);
            if (_addMeshCollider)
            {
                gameObject.AddComponent<MeshCollider>();
            }
        }
    }
}