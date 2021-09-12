using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShotPJ : MonoBehaviour
{
    [SerializeField] GameObject pj;
    [SerializeField] int damage;
    [SerializeField] float velocity;
    bool shotMod = true;
    [SerializeField] Camera cam;
    SpriteRenderer sprit;

    // Start is called before the first frame update
    void Start()
    {
        sprit = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            ToggleShotMod();
        }
        if (shotMod && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);
            Vector2 dir = mousePos - transform.position;
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            float currentRotZ = angle;
            for (int i = 0; i < 5; i++)
            {
                InstantiateProjectile(pj, damage, velocity, mousePos, currentRotZ);
                currentRotZ += 30f;
            }
        }
    }

    void ToggleShotMod()
    {
        shotMod = !shotMod;
        if (shotMod)
        {
            sprit.color = new Color(sprit.color.r, sprit.color.g, sprit.color.b, 1f);
        }
        else
        {
            sprit.color = new Color(sprit.color.r, sprit.color.g, sprit.color.b, 0.6f);
        }
    }

    void InstantiateProjectile(GameObject _projectile, int _damage, float _velocity, Vector2 _destination, float aaann)
    {
        GameObject pj = Instantiate(_projectile);
        pj.transform.position = transform.position;
        pj.GetComponent<MonsterProjectile>().Initialize(_damage, _velocity, aaann);
    }
}
