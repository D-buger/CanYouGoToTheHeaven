using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Joystick
{
    private GameObject backgroundObject;
    private GameObject handleObject;
    
    private Sprite backGroundImage;
    private Sprite handleImage;

    private FileLoader<Sprite> joystickImages;

    [Header("Value")]
    public float horizontalValue;
    public Vector2 vecMove;
    public Vector2 vecNormal;

    private JoystickPart back;
    private JoystickPart handle;

    public bool isActive => backgroundObject.active;

    public void SetFirst()
    {
        joystickImages = new FileLoader<Sprite>("Images/UI/Joystick", "UI_");

        GameObject Canvas = GameObject.FindGameObjectWithTag("GameController");
        if (Canvas != null)
        {
            backgroundObject = new GameObject("Background");
            backgroundObject.transform.SetParent(Canvas.transform);
            backgroundObject.transform.localScale = new Vector3(2, 2, 0);

            handleObject = new GameObject("Handle");
            handleObject.transform.SetParent(backgroundObject.transform);
            handleObject.transform.localScale = new Vector3(1.5f, 1.5f, 0);

            backgroundObject.AddComponent<Image>();
            handleObject.AddComponent<Image>();

            backGroundImage = joystickImages.GetFile("BackgroundImage");
            handleImage = joystickImages.GetFile("HandleImage");

            back = new JoystickPart(backgroundObject, backGroundImage);
            handle = new JoystickPart(handleObject, handleImage);

            JoyStickSetActive(false);
        }
    }

    private void JoyStickSetActive(bool active)
    {
        backgroundObject.SetActive(active);
        handleObject.SetActive(active);
    }

    public void JoyStickSetActive(Vector2 pos)
    {
        backgroundObject.SetActive(true);
        handleObject.SetActive(true);

        back.rectTransform.transform.position = pos;
    }

    //양방향 조이스틱
    private void Touch(Vector2 touchVec)
    {
        vecMove = new Vector2(
            touchVec.x - back.GetPos.x, 
            touchVec.y - back.GetPos.y);

        vecMove = Vector2.ClampMagnitude(vecMove, back.Radius);
        handle.rectTransform.localPosition = vecMove;
        
        vecNormal = vecMove.normalized;
        horizontalValue = vecNormal.x;
    }
    //가로로만 움직일 때 쓰인다
    private void Touch(float touchValue)
    {
        vecMove.x = touchValue - back.GetPos.x;

        vecMove.x = Mathf.Clamp(vecMove.x, -back.Radius , back.Radius);
        handle.rectTransform.localPosition = vecMove;
        
        vecNormal.x = vecMove.x / back.Radius;
        horizontalValue = vecNormal.x;
    }

    private void ResetPos()
    {
        vecMove = Vector2.zero;
        vecNormal = Vector2.zero;
        horizontalValue = 0;
        
        handle.rectTransform.localPosition =  Vector2.zero;
        JoyStickSetActive(false);
    }

    public void OnDrag(Vector2 pos)
    {
        if(isActive)
            Touch(pos.x);
    }

    public void OnPointerUp(Vector2 pos)
    {
        ResetPos();
    }
}

public struct JoystickPart
{
    public JoystickPart(GameObject obj)
    {
        if(!obj.TryGetComponent<Image>(out image))
             image = obj.AddComponent<Image>();

        rectTransform = obj.GetComponent<RectTransform>();

        Radius = rectTransform.rect.width * 0.5f;
    }

    public JoystickPart(GameObject obj, Sprite sprite) : this(obj) => image.sprite = sprite;

    private Image image;
    public RectTransform rectTransform;
    public Vector2 GetPos => rectTransform.position;

    public float Radius { get; private set; }
}