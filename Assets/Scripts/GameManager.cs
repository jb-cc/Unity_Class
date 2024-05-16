using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _playerHealth = 100;
    private int _score = 0;

    public int PlayerHealth
    {
        get { return _playerHealth; }
        set { _playerHealth = value; }
    }

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
    
    public static GameManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
