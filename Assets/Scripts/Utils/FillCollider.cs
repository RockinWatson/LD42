using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var pc2 = gameObject.GetComponent<BoxCollider2D>();

        int pointCount = 4;
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        Vector2[] points = new Vector2[pointCount];
        points[0] = new Vector2(0, 0);
        points[1] = new Vector2(0, pc2.size.x);
        points[2] = new Vector2(0, pc2.size.y);
        points[3] = new Vector2(pc2.size.x, pc2.size.y);

        Vector3[] vertices = new Vector3[pointCount];
        Vector2[] uv = new Vector2[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            Vector2 actual = points[i];
            vertices[i] = new Vector3(actual.x, actual.y, 0);
        }

    }
}
