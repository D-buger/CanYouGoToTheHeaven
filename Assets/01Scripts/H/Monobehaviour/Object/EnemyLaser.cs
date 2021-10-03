using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] public GameObject laser;
    [SerializeField] public GameObject chargeParticle;
    public List<LineRenderer> laserComps = new List<LineRenderer>();
    public List<LaserCollider> laserCollider = new List<LaserCollider>();
    public ParticleSystem particleComp;

    public void Initialize(int _count, float _totalAngle)
    {
        GameObject charge = Instantiate(chargeParticle);
        charge.transform.position = transform.position;
        charge.transform.SetParent(transform);
        particleComp = charge.GetComponent<ParticleSystem>();

        float currentRotZ = -90f;
        if (_count == 1)
        {
            GameObject las = Instantiate(laser);
            las.transform.position = transform.position;
            las.transform.rotation = Quaternion.Euler(0, 0, currentRotZ);
            las.transform.SetParent(transform);
            laserComps.Insert(0, las.GetComponent<LineRenderer>());
            laserCollider.Insert(0, las.GetComponent<LaserCollider>());
        }
        else
        {
            float shotBtwAngle = _totalAngle / _count;
            currentRotZ -= shotBtwAngle * (_count - 1) * 0.5f;

            for (int i = 0; i < _count; i++)
            {
                GameObject las = Instantiate(laser);
                las.transform.position = transform.position;
                las.transform.rotation = Quaternion.Euler(0, 0, currentRotZ);
                las.transform.SetParent(transform);
                currentRotZ += shotBtwAngle;
                laserComps.Insert(i, las.GetComponent<LineRenderer>());
                laserCollider.Insert(i, las.GetComponent<LaserCollider>());
            }
        }
    }
}
