using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class MeshDeformer : MonoBehaviour
{
    Mesh deformingMesh;
    public Vector3[] originalVertices, displacedVertices, vertexVelocities;

    float uniformScale = 1f;

    public float springForce = 20f;
    public float damping = 5f;

    // sets up vertices
    void Start()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;

        displacedVertices = new Vector3[originalVertices.Length];
        for(int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
        }

        vertexVelocities = new Vector3[originalVertices.Length];
    }

    // updates mesh
    void Update()
    {
        uniformScale = transform.localScale.x;

        for (int i = 0; i < displacedVertices.Length; i++)
        {
            UpdateVertex(i);
        }

        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();

        MeshCollider deformingCollider = GetComponent<MeshCollider>();
        deformingCollider.sharedMesh = null;
        deformingCollider.sharedMesh = deformingMesh;
    }

    // adds force to vertices to deform
    public void AddDeformingForce(Vector3 point, float force)
    {
        Debug.DrawLine(Camera.main.transform.position, point);

        point = transform.InverseTransformPoint(point);
        for(int i = 0; i < displacedVertices.Length; i++)
        {
            AddForceToVertex(i, point, force);
        }
    }

    // adds force to each vertices' velocity
    void AddForceToVertex(int i, Vector3 point, float force)
    {
        Vector3 pointToVertex = displacedVertices[i] - point;
        pointToVertex *= uniformScale;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;
        vertexVelocities[i] += pointToVertex.normalized * velocity;
    }

    // updates vertecies
    void UpdateVertex(int i)
    {
        Vector3 velocity = vertexVelocities[i];
        Vector3 displacement = displacedVertices[i] - originalVertices[i];
        displacement *= uniformScale;
        velocity -= displacement * springForce * Time.deltaTime;
        velocity *= 1f - damping * Time.deltaTime;
        vertexVelocities[i] = velocity;
        displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);

        // limits
        displacedVertices[i] = Vector3.ClampMagnitude(displacedVertices[i], 1f);
        // TODO limit min value
    }
}
