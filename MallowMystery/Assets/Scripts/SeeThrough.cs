using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SeeThrough : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private LayerMask layerMask;
    
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        // Vector2 cutoutPos = _mainCamera.WorldToViewportPoint(targetObject.position);
        // cutoutPos.y /= (Screen.width / Screen.height);

        // Vector3 offset = targetObject.position - transform.position;
        // RaycastHit[] hits = Physics.RaycastAll(transform.position, offset, offset.magnitude, layerMask);
        if (Physics.Linecast(transform.position, targetObject.position, out RaycastHit hitInfo,layerMask))
        {
            
            var materials = targetObject.transform.GetComponent<Renderer>().materials;

            for(int j = 0; j < materials.Length; j++)
            {
                materials[j].SetVector("_CutoutPos", hitInfo.textureCoord);
                materials[j].SetFloat("_CutoutSize", 0.1f);
                materials[j].SetFloat("_FalloffSize", 0.05f);
            }
        } 
    }
}

