using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MeshCreator : MonoBehaviour {

    // Public vars
    public int sides = 1;
    public float radius = 1;
    public Vector2 scale = new Vector2(1, 1);
    public PolygonCollider2D polygonCollider;
    public float colliderScale = 1;
    public Vector2 colliderOffset = new Vector2(0, 0);
    
    [ContextMenuItem("Create Shape", "setupShape")]
    public bool frontsided = true;

    // Private vars
    private Vector3[] vertexInputs;
    private Vector3 center;
    private Vector3[] vertexes;
    private Vector2[] uv;
    private int[] triangles;

	// Use this for initialization
	void Start () {
        sides = Random.Range(3, 10);
        GetComponent<ShapeManager>().maxSides = sides;
        
        setupShape();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void generateInput () {
        int i;
        float angle = 3.14f;
        float angleStep = Mathf.PI * 2 / sides;
        vertexInputs = new Vector3[sides];
        for(i = 0; i < vertexInputs.Length; i++) {
            vertexInputs[i] = new Vector2(Mathf.Sin(angle) * radius * scale.x, Mathf.Cos(angle) * radius * scale.y);
            angle += angleStep;
        }
    }

    public void setupShape ()
    {
        // Generate vertexes
        generateInput();

        // Create the mesh and save it
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = createMesh(); // = Mesh.Instantiate(meshFilter.sharedMesh) as Mesh;
        meshFilter.mesh = mesh;
        MeshToFile(meshFilter, "Assets/Shapes/Shape" + sides + ".obj");
        #if UNITY_EDITOR
        AssetDatabase.AddObjectToAsset(mesh, "Assets/Shapes/Shape" + sides + ".obj");
        AssetDatabase.SaveAssets();
        #endif
        
        // Setup 2d poly collider points
        PolygonCollider2D polygonTrigger = GetComponent<PolygonCollider2D>();
        Vector2[] shapeTriggerPoints = new Vector2[vertexInputs.Length];
        Vector2[] shapeColliderPoints = new Vector2[vertexInputs.Length];
        // Change 3d points to 2d points with scale and offset then set them
        for(int i = 0; i < shapeTriggerPoints.Length; i++) {
            shapeTriggerPoints[i] = new Vector2(vertexInputs[i].x * colliderScale + colliderOffset.x, vertexInputs[i].y * colliderScale + colliderOffset.y);
            shapeColliderPoints[i] = new Vector2(vertexInputs[i].x + colliderOffset.x, vertexInputs[i].y + colliderOffset.y);
        }
        polygonTrigger.points = shapeTriggerPoints;
        polygonCollider.points = shapeColliderPoints;
    }
    
    public Mesh createMesh ()
    {
        // Set the vertexs and trianges array
        vertexes = new Vector3[vertexInputs.Length * 3];
        triangles = new int[vertexInputs.Length * 3];
                
        // Find center position
        center = new Vector3(0, 0, 0);
        int total = 0;
        foreach (Vector3 vector in vertexInputs) {
            center += vector;
            total++;
        }
        center /= total;

        // Create the vertexes and trianges array
        int i;
        for(i = 0; i < vertexInputs.Length - 1; i++) {
            vertexes[i * 3] = vertexInputs[i];
            vertexes[i * 3 + 1] = vertexInputs[i + 1];
            vertexes[i * 3 + 2] = center;

            if(frontsided) {
                triangles[i * 3 + 2] = i * 3;
                triangles[i * 3 + 1] = i * 3 + 1;
                triangles[i * 3] = i * 3 + 2;
            } else {
                triangles[i * 3] = i * 3;
                triangles[i * 3 + 1] = i * 3 + 1;
                triangles[i * 3 + 2] = i * 3 + 2;                
            }
        }
        vertexes[i * 3] = vertexInputs[0];
        vertexes[i * 3 + 1] = vertexInputs[i];
        vertexes[i * 3 + 2] = center;
        if(frontsided) {
            triangles[i * 3] = i * 3;
            triangles[i * 3 + 1] = i * 3 + 1;
            triangles[i * 3 + 2] = i * 3 + 2;
        } else {
            triangles[i * 3 + 2] = i * 3;
            triangles[i * 3 + 1] = i * 3 + 1;
            triangles[i * 3] = i * 3 + 2;
        }

        //UV
        uv = new Vector2[vertexInputs.Length * 3]; 
        for(i = 0; i < uv.Length; i++) {
            uv[i] = new Vector2(vertexes[i].x, vertexes[i].y);
        }

        //Init and return
        Mesh mesh = new Mesh();
        mesh.Clear ();
        mesh.vertices = vertexes;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.Optimize ();
        mesh.RecalculateBounds ();
        mesh.RecalculateNormals ();
        
        return mesh;
    }
    
    void OnDrawGizmosSelected() {
        // Generate vertexes
        generateInput();

        if(vertexInputs.Length > 0) {
            Vector3 last = vertexInputs[0];
            Vector3 vec;
            Vector3 lastvec;
            lastvec = last;
            lastvec.Scale(transform.localScale);
            lastvec += transform.position;

            float squareSize = Mathf.Max(0.1f / 10, 0.05f);

            foreach (Vector3 vector in vertexInputs) {
                //Get the vertex data
                vec = vector;
                vec.Scale(transform.localScale);
                vec += transform.position;

                //Draw the vertexes, selected vertex is green
                Gizmos.color = new Color(1, 0, 0, 0.5f);
                Gizmos.DrawCube(vec, new Vector3(squareSize, squareSize, squareSize));

                //Draw the lines between this vector and the last vector
                Gizmos.color = new Color(0, 1, 0, 0.5f);
                Gizmos.DrawLine(vec, lastvec);

                last = vector;
                lastvec = vec;
            }
            
            //Draw the center vertex
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawCube(center, new Vector3(squareSize, squareSize, squareSize));

            Gizmos.color = new Color(0, 1, 0, 0.5f);
            vec = vertexInputs[0];
            vec.Scale(transform.localScale);
            vec += transform.position;
            lastvec = last;
            lastvec.Scale(transform.localScale);
            lastvec += transform.position;
            Gizmos.DrawLine(vec, lastvec);
        }
    }

    public static string MeshToString(MeshFilter mf) {
        Mesh m = mf.sharedMesh;
        Material[] mats = mf.GetComponent<Renderer>().sharedMaterials;
        
        StringBuilder sb = new StringBuilder();
        
        sb.Append("g ").Append(mf.name).Append("\n");
        foreach(Vector3 v in m.vertices) {
            sb.Append(string.Format("v {0} {1} {2}\n",v.x,v.y,v.z));
        }
        sb.Append("\n");
        foreach(Vector3 v in m.normals) {
            sb.Append(string.Format("vn {0} {1} {2}\n",v.x,v.y,v.z));
        }
        sb.Append("\n");
        foreach(Vector3 v in m.uv) {
            sb.Append(string.Format("vt {0} {1}\n",v.x,v.y));
        }
        for (int material=0; material < m.subMeshCount; material ++) {
            sb.Append("\n");
            sb.Append("usemtl ").Append(mats[material].name).Append("\n");
            sb.Append("usemap ").Append(mats[material].name).Append("\n");
            
            int[] triangles = m.GetTriangles(material);
            for (int i=0;i<triangles.Length;i+=3) {
                sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n", 
                                        triangles[i]+1, triangles[i+1]+1, triangles[i+2]+1));
            }
        }
        return sb.ToString();
    }
    
    public static void MeshToFile(MeshFilter mf, string filename) {
        using (StreamWriter sw = new StreamWriter(filename)) 
        {
            sw.Write(MeshToString(mf));
        }
    }
}
