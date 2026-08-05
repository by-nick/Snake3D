using UnityEngine;

public class Snake : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float vel = 2.5f;
    private Rigidbody RB;
    
    void Start()
    {
        //pega ref. do componente Rigidbody adicionado no obj
        RB = GetComponent <Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h,0,v) * vel * Time.deltaTime);


    }
}
