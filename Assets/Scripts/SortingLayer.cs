using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour {
    [SerializeField]
    private string sortingLayerName;
    [SerializeField]
    private int sortingOrder;

    private void Awake()
    {
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = sortingLayerName;
        gameObject.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
    }
}
