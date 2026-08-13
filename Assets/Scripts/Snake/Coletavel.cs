using UnityEngine;

public class Coletavel : MonoBehaviour
{
    //quantidade de itens coletados ao iniciar
    public int coletavel = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mostra no console quantos coletou
        Debug.Log(coletavel);
    }
    //utiliza de gatilhos para coletar item
    void OnTriggerEnter(Collider outro)
    {
        if (outro.gameObject.CompareTag("item"))
        {
            coletavel++;
            Destroy(outro.gameObject);
        }
    }
}
