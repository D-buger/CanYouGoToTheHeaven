using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameResultScreen : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameClearScreen;

    private void Awake()
    {
        /*GameObject s1 = Instantiate(gameOverScreen);
        s1.transform.SetParent(transform);
        GameObject s2 = Instantiate(gameClearScreen);
        s2.transform.SetParent(transform);*/
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
