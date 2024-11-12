using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

 
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; // Array de prefabs de inimigos que podem ser instanciados
    [SerializeField] private int baseEnemies = 8; // N�mero base de inimigos em cada onda
    [SerializeField] private float enemiesPerSecond = 0.5f; // N�mero de inimigos gerados por segundo no in�cio do jogo
    [SerializeField] private float timeBetweenWaves = 5f; // Tempo entre ondas de inimigos
    [SerializeField] private float difficultyScalingFactor = 0.75f; // Fator de escala para aumentar a dificuldade em cada onda
    [SerializeField] private float enemiesPerSecondCap = 15f; // Limite m�ximo para inimigos gerados por segundo

    public static UnityEvent onEnemyDestroy = new UnityEvent(); // Evento est�tico chamado quando um inimigo � destru�do

    private int currentWave = 1; // N�mero da onda atual
    private float timeSinceLastSpawn; // Tempo decorrido desde o �ltimo spawn de inimigo
    private int enemiesAlive; // Contador de inimigos vivos
    private int enemiesLeftToSpawn; // Contador de inimigos restantes para spawn na onda atual
    private float eps; // Inimigos por segundo ajustado para a onda atual
    private bool isSpawning = false; // Indica se os inimigos est�o sendo gerados

    private void Awake()
    {
        // Adiciona um listener para reduzir o contador de inimigos quando um inimigo � destru�do
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        // Inicia a primeira onda
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        // Reduz o n�mero de inimigos vivos quando um inimigo � destru�do
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        // Espera o tempo especificado entre as ondas
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true; // Inicia o spawn de inimigos
        enemiesLeftToSpawn = EnemiesPerWave(); // Calcula o n�mero de inimigos para spawn nesta onda
        eps = EnemiesPerSecond(); // Calcula a taxa de spawn para esta onda
    }

    private int EnemiesPerWave()
    {
        // Calcula o n�mero de inimigos para spawn com base na dificuldade e no n�mero da onda
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Update � chamado a cada frame
    void Update()
    {
        if (!isSpawning) return; // Se n�o estiver gerando, retorna

        timeSinceLastSpawn += Time.deltaTime; // Atualiza o tempo desde o �ltimo spawn

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            // Se o tempo desde o �ltimo spawn for suficiente, gera um novo inimigo
            SpawnEnemy();
            enemiesLeftToSpawn--; // Reduz o n�mero de inimigos restantes para spawn
            enemiesAlive++; // Aumenta o contador de inimigos vivos
            timeSinceLastSpawn = 0f; // Reinicia o tempo desde o �ltimo spawn
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            // Se n�o houver mais inimigos vivos e para spawn, termina a onda
            EndWave();
        }
    }

    private void EndWave()
    {
        // Reseta vari�veis e inicia a pr�xima onda
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++; // Incrementa o n�mero da onda
        StartCoroutine(StartWave()); // Inicia a pr�xima onda
    }

    private void SpawnEnemy()
    {
        // Escolhe aleatoriamente um inimigo para spawn e o instancia no ponto de partida
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    private float EnemiesPerSecond()
    {
        // Calcula a taxa de spawn de inimigos, aumentando com a dificuldade e limitando ao cap
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }
}

 