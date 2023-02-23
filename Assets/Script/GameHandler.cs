using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameState currentGameState;
    public static GameHandler instance;
    void Start()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            Destroy(gameObject);
        }
    }
}
