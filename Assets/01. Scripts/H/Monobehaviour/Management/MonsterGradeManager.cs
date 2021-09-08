using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGradeManager : MonoBehaviour
{
    public static MonsterGradeManager instance = null;

    void MakeSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public GameObject[] EGradeMonster;
    public GameObject[] DGradeMonster;
    public GameObject[] CGradeMonster;
    public GameObject[] BGradeMonster;
    public GameObject[] AGradeMonster;
    public GameObject[] SGradeMonster;
    //Dictionary Ȱ���ؼ� ������ �����


    private void Awake()
    {
        MakeSingleton();
    }
}
