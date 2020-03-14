using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ScriptableObject
{
    private static Character instance = null;

    public void OnEnable()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore");
    }

    public static Character getInstance()
    {
        if (instance == null)
        {
            instance = CreateInstance<Character>();
        }

        return instance;
    }

    public GameObject gameObject;
    public Rigidbody2D rigidbody2D;
    public Animator animator;

    public bool canJump = false;
    public bool isPlaying = false;
    public bool hasReachRecord = false;
    public int score = 0;
    private int _bestScore = 0;
    public int bestScore
    {
        get => _bestScore;
        set
        {
            _bestScore = value;
            PlayerPrefs.SetInt("BestScore", value);
        }

    }

    // Awake is called before any Start functions
    void Awake()
    {
        this.gameObject = GameObject.FindWithTag("Player");
        this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        this.animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDefaultValues(bool _isPlaying = true)
    {
        canJump = _isPlaying;
        isPlaying = _isPlaying;
        hasReachRecord = false;
        score = 0;
    }

    public bool IsJumping()
    {
        return isPlaying && rigidbody2D.velocity.y > -0.1f;
    }
}
