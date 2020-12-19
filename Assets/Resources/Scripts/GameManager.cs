using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    public LevelManager levelManager;
    public List<Cell> cells = new List<Cell>();
    private void Awake()
    {
        INSTANCE = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
}
