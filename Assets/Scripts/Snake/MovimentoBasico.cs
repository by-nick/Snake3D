using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // velocidade
    [SerializeField] private float vel = 5f;
    // tamanho inicial da cobrinha
    [SerializeField] private int TamInicial = 5;

    [SerializeField] private GameObject Prefabs; // Lembre-se: use um Prefab SEM o script Snake!
    // distancia entre os segmentos da cobrinha
    [SerializeField] private float DistanciaDosSegmentos = 1.0f;

    private List<GameObject> corpo = new List<GameObject>();
    private List<Vector3> HistoricoPos = new List<Vector3>();

    void Start()
    {
        // faz com que player comece com posição inicial
        HistoricoPos.Add(transform.position);

        for (int i = 0; i < TamInicial; i++)
        {
            AumentaTamanho();
        }
    }

    void Update()
    {
        // Movimentação
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 mover = new Vector3(h, 0, v);
        
        // Move no espaço global (Space.World) para evitar desalinhamento nos eixos
        transform.Translate(mover * vel * Time.deltaTime, Space.World);

        if (mover != Vector3.zero)
        {
            transform.forward = mover; // Faz a cabeça virar para onde está andando
        }

        TamanhoPlayer(); // Atualiza o histórico de posições
        MoveCorpo();     // Atualiza a posição dos segmentos do corpo
    }

    private void TamanhoPlayer()
    {
        // Adiciona a posição atual da cabeça ao início da lista
        if (HistoricoPos.Count == 0 || Vector3.Distance(HistoricoPos[0], transform.position) > 0.01f)
        {
            HistoricoPos.Insert(0, transform.position);
        }

        // Limpa posições antigas que não são mais necessárias
        int limiteHistorico = (corpo.Count + 1) * 100;
        if (HistoricoPos.Count > limiteHistorico)
        {
            HistoricoPos.RemoveAt(HistoricoPos.Count - 1);
        }
    }

    private void MoveCorpo()
    {
        float distanciaAcumulada = 0f;

        for (int i = 0; i < corpo.Count; i++)
        {
            distanciaAcumulada += DistanciaDosSegmentos;

            Vector3 posicaoAlvo = PosicaoNoHistorico(distanciaAcumulada);

            // Trava a altura Y para ser exatamente igual à altura da cabeça
            posicaoAlvo.y = transform.position.y;

            corpo[i].transform.position = posicaoAlvo;
        }
    }

    private Vector3 PosicaoNoHistorico(float distanciaDesejada)
    {
        float distanciaAtual = 0f;

        for (int i = 0; i < HistoricoPos.Count - 1; i++)
        {
            float d = Vector3.Distance(HistoricoPos[i], HistoricoPos[i + 1]);

            if (distanciaAtual + d >= distanciaDesejada)
            {
                float t = (distanciaDesejada - distanciaAtual) / d;
                return Vector3.Lerp(HistoricoPos[i], HistoricoPos[i + 1], t);
            }

            distanciaAtual += d;
        }

        return HistoricoPos[HistoricoPos.Count - 1];
    }

    public void AumentaTamanho()
    {
        if (Prefabs == null)
        {
            Debug.LogError("O 'Prefab Corpo' não foi atribuído no Inspector da Snake!");
            return;
        }

        Vector3 posInicial = corpo.Count > 0 
            ? corpo[corpo.Count - 1].transform.position 
            : transform.position;

        GameObject novoSegmento = Instantiate(Prefabs, posInicial, Quaternion.identity);
        corpo.Add(novoSegmento);
    }
}




