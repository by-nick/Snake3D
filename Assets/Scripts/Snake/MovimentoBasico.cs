using UnityEngine;

public class Snake : MonoBehaviour
{
    private float vel = 2.5f;
    private Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector3 mover = new Vector3(h, 0f, v); 
        transform.Translate(mover * vel * Time.deltaTime);
        
    }
}
