using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public List<GameObject> redPooledGO;
    public List<GameObject> bluePooledGO;
    [SerializeField] static GameState gameState;

    [Header("ObstacleHandler")]
    [SerializeField] Transform[] obstacleSpawners;
    [SerializeField] GameObject obstacleSpawnPrefab;
    [SerializeField] ObjectPooling objectPooling;
    [SerializeField] float obstacleSpawnDistance;
    List<GameObject> spawnedObstacles;
    float obstacleTimer;

    [Header("SFX")]
    [SerializeField] AudioClip deathSfx;
    [SerializeField] AudioSource gameBGM;

    [Header("Player Settings")]
    [SerializeField] float speedIncreaseTimeIntervalInSec;
    [SerializeField] float speedIncreaseRate;
    float playerTimer;

    [Header("Menu Object")]
    [SerializeField] GameObject mobileInput;
    [SerializeField] GameObject mainGame;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject gameMenu;


    private void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Debug.Log("Game object destroyed");
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        redPooledGO = new List<GameObject>();
        bluePooledGO = new List<GameObject>();
        spawnedObstacles = new List<GameObject>();
        SetTimer();
        SetupGame();
    }

    private void Update() {
        if(PlayerController.instance && PlayerController.instance.playerIsAlive){
            ObstacleSpawnTimer();
            IncreasePlayerSpeed();
        }
        DisableObstacles();
    }

#region ObstacleHandler
    void SetTimer(){
        obstacleTimer = Random.Range(3, 8);
    }

    void ObstacleSpawnTimer(){
        if(obstacleTimer <= 0){
            GameObject spawnedObstacle;
            spawnedObstacle = objectPooling.GetObjectFromPool(obstacleSpawnPrefab);
            spawnedObstacle.SetActive(true);
            spawnedObstacle.transform.position = obstacleSpawners[Random.Range(0, obstacleSpawners.Length)].position;
            spawnedObstacles.Add(
                spawnedObstacle
            );
            obstacleTimer = Random.Range(3, 8);
        }else{
            obstacleTimer -= Time.deltaTime;
        }
    }

    void DisableObstacles(){
        if(spawnedObstacles.Count > 0){
            for (int i=0; i<spawnedObstacles.Count; i++)
            {
                if(Vector3.Distance(spawnedObstacles[i].transform.position, obstacleSpawners[0].position) >= obstacleSpawnDistance){
                    objectPooling.AddToPool(spawnedObstacles[i]);
                    spawnedObstacles.RemoveAt(i);
                }
            }
        }
    }
#endregion

    public void PlayDeathSFX(){
        gameBGM.Stop();
        gameObject.GetComponent<AudioSource>().clip = deathSfx;
        gameObject.GetComponent<AudioSource>().Play();
    }

    void IncreasePlayerSpeed(){
        playerTimer += Time.deltaTime;
        if(playerTimer > speedIncreaseTimeIntervalInSec){
            PlayerController.instance.IncreseMovementSpeed(speedIncreaseRate);
            playerTimer = 0f;
        }
    }

    public void GameOver(){
        gameMenu.SetActive(true);
    }

#region MenuImplementations
    void SetupGame(){
        if(GameHandler.instance.currentGameState == GameState.Restart){
            StartGame();
        }
    }

    public void RetryGame(){
        GameHandler.instance.currentGameState = GameState.Restart;
        SceneManager.LoadScene(0);
    }

    public void ManiMenu(){
        SceneManager.LoadScene(0);
    }

    public void ExitApplication(){
        Application.Quit();
    }

    public void StartGame(){
        gameBGM.Play();
        startMenu.SetActive(false);
        mainGame.SetActive(true);
        mobileInput.SetActive(true);
    }
#endregion
}

public enum GameState{
    Start,
    Restart
}
