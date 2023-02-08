using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { PLAYER, ENEMY }

public class Bullet_Control : MonoBehaviour
{
    private BulletType Type;
    private float Speed;
    private int Dir;
    private Game_Manager Manager;
    
    public void InitBullet(BulletType _type, float _speed, Game_Manager _manager)
    {
        Type = _type;
        Speed = _speed;
        Manager = _manager;
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
         
        transform.Translate(Vector2.up * Dir * Speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (Type)
        {
            case BulletType.ENEMY:
                if (other.tag == "Player")
                {
                    Manager.GetDamgePlayer();
                    Destroy(gameObject);
                }
                break;
            case BulletType.PLAYER:
                if (other.tag == "Enemy")
                {
                    Manager.GetDamageEnemy(other.gameObject);
                    Destroy(gameObject);
                }
                break;
        }
        
    }
}
