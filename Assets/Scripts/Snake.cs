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

        float movimentoZ = 0f; // é usado no 3d, frente e tras o eixo z

        //input de movimento basico
        
        //movimentação para direita
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(vel * Time.deltaTime, 0, 0));


        //movimentação para esquerda
        }else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3( -vel * Time.deltaTime,0,0));


        //movimentação para cima
        }else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movimentoZ = 1f;

        //movimentação para baixo
        }else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movimentoZ = -1f;
        }

        //move o rigidbody usando velocidade (Respeita colisoes e paredes do chao)
        Vector3 direcao = new Vector3(0f,0f, movimentoZ).normalized;
        RB.MovePosition(RB.position + direcao * vel * Time.fixedDeltaTime);
        
    }
}
