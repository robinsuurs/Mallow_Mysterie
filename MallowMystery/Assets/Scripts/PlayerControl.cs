using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    
    private Rigidbody _rigidbody;
    private Vector2 _movement;
    [SerializeField] private float speed = 10;
    private int walkDegrees = -45;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        mainCam = GameObject.FindWithTag("MainCamera").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(_movement.x +_movement.y  ,0,_movement.y -_movement.x)* (speed*Time.deltaTime));
    }

    void OnMove(InputValue inputValue)
    {
        _movement = Rotate(inputValue.Get<Vector2>(), walkDegrees);
    }
    
    private static Vector2 Rotate(Vector2 v, float degrees) {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
		
        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    void OnResetLocation() {
        transform.position = new Vector3(0, 1, 0);
    }
    
     //Remove later on for test purposes only
     [SerializeField] private GameObject mainCam;
    
        void OnCamera1() {
            mainCam.transform.position = new Vector3(-120, 100, -120);
            mainCam.transform.eulerAngles = new Vector3(30, 45, 0);
        }
        
        void OnCamera2() {
            mainCam.transform.position = new Vector3(-80, 120, -80);
            mainCam.transform.eulerAngles = new Vector3(45, 45, 0);
        }
        
        void OnCamera3() {
            mainCam.transform.position = new Vector3(-60, 150, -60);
            mainCam.transform.eulerAngles = new Vector3(60, 45, 0);
        }
        
        void OnCamera4() {
            mainCam.transform.position = new Vector3(-120, 85, -70);
            mainCam.transform.eulerAngles = new Vector3(30, 60, 0);
        }
        
        void OnCamera5() {
            mainCam.transform.position = new Vector3(-120, 90, -90);
            mainCam.transform.eulerAngles = new Vector3(30, 53, 0);
        }
        
        void OnCamera6() {
            mainCam.transform.position = new Vector3(-120, 121, -70);
            mainCam.transform.eulerAngles = new Vector3(40, 60, 0);
        }
    
        public void OnWalkLeft(InputValue inputValue) {
            walkDegrees = 0;
        }
        
        public void OnWalkForward(InputValue inputValue) {
            walkDegrees = -45;
        }
        
        public void OnWalkRight(InputValue inputValue) {
            walkDegrees = -90;
        }
}
