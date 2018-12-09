using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TriangleExplosion : MonoBehaviour
{
    public List<GameObject> meshesToExplode;
    public Vector3 explosionPosition;
    public float minExplosionForce;
    public float maxExplosionForce;
    public string trianglesLayer = "Particle";

    private void Start()
    {
        if (meshesToExplode.Count == 0)
            meshesToExplode.Add(gameObject);

        foreach (GameObject mesh in meshesToExplode)
        {
            if (mesh.GetComponent<MeshFilter>() == null && mesh.GetComponent<SkinnedMeshRenderer>() == null)
            {
                Debug.LogError("NO MESH FOUND");
            }
        }
    }


    public void SetExplosion(Vector3 contactPosition, Vector3 contactImpulse, bool destroy)
    {
        explosionPosition = contactPosition;
        minExplosionForce = contactImpulse.magnitude * 10f;
        maxExplosionForce = contactImpulse.magnitude * 200f;
        foreach (GameObject mesh in meshesToExplode)
            StartCoroutine(ExplodeMesh(mesh, destroy));
    }

    public IEnumerator ExplodeMesh(GameObject target, bool destroy)
    {
        if (target.GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = false;
        }

        Mesh M = new Mesh();
        if (target.GetComponent<MeshFilter>())
        {
            M = target.GetComponent<MeshFilter>().mesh;
        }
        else if (target.GetComponent<SkinnedMeshRenderer>())
        {
            M = target.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }

        Material[] materials = new Material[0];
        if (target.GetComponent<MeshRenderer>())
        {
            materials = target.GetComponent<MeshRenderer>().materials;
        }
        else if (target.GetComponent<SkinnedMeshRenderer>())
        {
            materials = target.GetComponent<SkinnedMeshRenderer>().materials;
        }

        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;
        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {
            int[] indices = M.GetTriangles(submesh);

            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];
                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }

                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle " + (i / 3));
                GO.layer = LayerMask.NameToLayer(trianglesLayer);
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.transform.localScale = transform.localScale;
                GO.AddComponent<MeshRenderer>().material = materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.AddComponent<BoxCollider>();
                GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(minExplosionForce, maxExplosionForce), explosionPosition, 1);
                GO.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.right * 100f);
                //GO.GetComponent<Rigidbody>().useGravity = false;
                Destroy(GO, 5 + Random.Range(0.0f, 5.0f));
            }
        }


        target.GetComponent<Renderer>().enabled = false;

        yield return new WaitForSeconds(1.0f);
        if (destroy == true)
        {
            Destroy(target);
        }
    }

}