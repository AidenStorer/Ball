using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CreateColliders : MonoBehaviour
{
    public float colliderThickness = 1f;
    public float colliderHeight = 10f; // Vertical size in Z-axis
    public bool createTop = true;
    public bool createBottom = true;
    public bool createLeft = true;
    public bool createRight = true;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        CreateTheColliders();
    }

    void CreateTheColliders()
    {
        if (!cam.orthographic)
        {
            Debug.LogError("ScreenEdgeColliders3D only works with an Orthographic camera.");
            return;
        }

        float screenHeight = 2f * cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;

        Vector3 center = cam.transform.position;
        float z = 0; // Slightly in front of near plane

        Vector3 colliderSizeVertical = new Vector3(colliderThickness, screenHeight, colliderHeight);
        Vector3 colliderSizeHorizontal = new Vector3(screenWidth, colliderThickness, colliderHeight);

        if (createLeft)
            CreateEdge("Left", new Vector3(center.x - screenWidth / 2 - colliderThickness / 2, center.y, z), colliderSizeVertical);

        if (createRight)
            CreateEdge("Right", new Vector3(center.x + screenWidth / 2 + colliderThickness / 2, center.y, z), colliderSizeVertical);

        if (createTop)
            CreateEdge("Top", new Vector3(center.x, center.y + screenHeight / 2 + colliderThickness / 2, z), colliderSizeHorizontal);

        if (createBottom)
            CreateEdge("Bottom", new Vector3(center.x, center.y - screenHeight / 2 - colliderThickness / 2, z), colliderSizeHorizontal);
    }

    void CreateEdge(string name, Vector3 position, Vector3 size)
    {
        GameObject edge = new GameObject("Edge_" + name);
        edge.transform.position = position;

        BoxCollider collider = edge.AddComponent<BoxCollider>();
        collider.size = size;

/*        Rigidbody rb = edge.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.isKinematic = true;
        rb.useGravity = false;*/
    }
}
