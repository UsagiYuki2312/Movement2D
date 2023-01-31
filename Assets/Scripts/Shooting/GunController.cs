using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject baseGun;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            baseGun.transform.Rotate(0, 0, 20f, Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            baseGun.transform.Rotate(0, 0, -20f, Space.Self);
        }
    }
    void Shoot()
    {
        GameObject instanceBullet = Instantiate(bullet);
    }
}
