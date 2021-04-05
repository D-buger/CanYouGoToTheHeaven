using UnityEngine;

public abstract class SingletonBehavior<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T inst;
    public static T Instance
    {
        get
        {
            if (inst == null)
                inst = FindObjectOfType<T>();

            return inst;
        }
    }

    private void Awake()
    {
        if(inst == null)
        {
            inst = GetComponent<T>();
        }
        else if(inst != this)
        {
            Destroy(gameObject);
            return;
        }

        OnAwake();
    }

    protected abstract void OnAwake();
}
