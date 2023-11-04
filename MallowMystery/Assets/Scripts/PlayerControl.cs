using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    
    private Rigidbody rb;
    private Vector2 _movement;
    [SerializeField] private float speed = 10;
    [SerializeField] private int walkDegrees = -45;
    [SerializeField] private bool overworldWalking;
    public float maxVelocityChange = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = GameObject.FindWithTag("MainCamera").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector3(_movement.x, 0, _movement.y);
        targetVelocity = transform.TransformDirection(targetVelocity) * speed;
        
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public void setDegrees() {
        walkDegrees = -5;
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
    
     //Remove later on for test purposes only
     [SerializeField] private GameObject mainCam;
    
        void OnCamera1() {
            if (SceneManager.GetActiveScene().name.Equals("DetectiveRoom")) {
            mainCam.transform.position = new Vector3(-120, 100, -120);
            mainCam.transform.eulerAngles = new Vector3(30, 45, 0);
            }
        }
        
        void OnCamera2() {
            if (SceneManager.GetActiveScene().name.Equals("DetectiveRoom")) {
            mainCam.transform.position = new Vector3(-80, 120, -80);
            mainCam.transform.eulerAngles = new Vector3(45, 45, 0);
            }
        }
        
        void OnCamera3() {
            if (SceneManager.GetActiveScene().name.Equals("DetectiveRoom")) {
            mainCam.transform.position = new Vector3(-60, 150, -60);
            mainCam.transform.eulerAngles = new Vector3(60, 45, 0);
            }
        }
        
        void OnCamera4() {
            if (SceneManager.GetActiveScene().name.Equals("DetectiveRoom")) {
            mainCam.transform.position = new Vector3(-120, 85, -70);
            mainCam.transform.eulerAngles = new Vector3(30, 60, 0);
            }
        }
        
        void OnCamera5() {
            if (SceneManager.GetActiveScene().name.Equals("DetectiveRoom")) {
            mainCam.transform.position = new Vector3(-120, 90, -90);
            mainCam.transform.eulerAngles = new Vector3(30, 53, 0);
            }
        }
        
        void OnCamera6() {
            if (SceneManager.GetActiveScene().name.Equals("DetectiveRoom")) {
            mainCam.transform.position = new Vector3(-120, 121, -70);
            mainCam.transform.eulerAngles = new Vector3(40, 60, 0);
            }
        }
    
        public void OnWalkLeft(InputValue inputValue) {
            if (SceneManager.GetActiveScene().name.Equals("DetectiveRoom")) {
            walkDegrees = 0;
            }
        }
        
        public void OnWalkForward(InputValue inputValue) {
            if (SceneManager.GetActiveScene().name.Equals("DetectiveRoom")) {
            walkDegrees = -45;
            }
        }
        
        public void OnWalkRight(InputValue inputValue) {
            if (SceneManager.GetActiveScene().name.Equals("DetectiveRoom")) {
            walkDegrees = -90;
            }
        }
}
