using UnityEngine;

public class Snake : MonoBehaviour
{
    //velocidade
    private float vel = 2.5f;

    void Start()
    {
        
    }
    void Update()
    {
        //codigo para movimentação utilizando axis das configurações
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        //h = x y = 0 v = z
        Vector3 mover = new Vector3(h, 0f, v); 
        transform.Translate(mover * vel * Time.deltaTime);

        
    }

}
