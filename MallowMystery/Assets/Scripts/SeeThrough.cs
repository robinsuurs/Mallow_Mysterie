using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SeeThrough : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private LayerMask layerMask;
    
    private  Material material;
    private Camera _mainCamera;
    
    //Beunhaase functie voor aan script gooien
    public void setFollowPlayer() {
       
            targetObject = GameObject.FindWithTag("Player").transform;
       
    }
    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }
    
    // Update is called once per frame
    private void Update()
    {
         Vector2 cutoutPos = _mainCamera.WorldToViewportPoint(targetObject.position);
         cutoutPos.y /= (Screen.width / Screen.height);
        // Vector3 offset = targetObject.position - transform.position;
        // RaycastHit[] hits = Physics.RaycastAll(transform.position, offset, offset.magnitude, layerMask);
        if (Physics.Linecast(transform.position, targetObject.position, out RaycastHit hitInfo,layerMask))
        {
            
             material = hitInfo.transform.GetComponent<Renderer>().material;
      
            
            material.SetVector("_CutoutPos", cutoutPos);
            material.SetFloat("_CoutoutSize", 0.1f);
            material.SetFloat("_FalloffSize", 0.05f);
            
        }
        else if(material!=null)
        {
            material.SetFloat("_CoutoutSize", 0f);
        }
    }
}

