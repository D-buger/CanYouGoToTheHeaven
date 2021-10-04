using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeBtn : MonoBehaviour
{
    public void SceneChange(int sceneNum)
    {
        GameManager.Instance.ChangeScene(sceneNum);
    }
}
