using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { PLAYER, ENEMY }

public class Bullet_Control : MonoBehaviour
{
    private BulletType Type;
    private float Speed;
    private int Dir;
    
    public void InitBullet(BulletType _type, float _speed)
    {
        Type = _type;
        Speed = _speed;
        if (_type == BulletType.PLAYER)
        {
            Dir = 1;
        }
        else
        {
            Dir = -1;
        }

    }

    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }
}
