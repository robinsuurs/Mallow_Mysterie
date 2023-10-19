using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private LayerMask _layerMask;
    
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToVieuwportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, offset, offset.magnitude, _layerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            Material[] materials = hits[i].transform.GetComponent<Renderer>()materials;

            for (int j = 0; j < materials.Length; j++)
            {
                materials[m].SetVector("_CutoutPos");
                materials[m].SetFloat("_CutoutSize", 0.1f);
                materials[m].SetFloat("_FalloffSize", 0.05f);
            }
        }
    }
}
