using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Creator : MonoBehaviour
{
    public class EnemyProperties
    {
        public int ID;
        public Color Skin;
        public int Lives;
    }

    static public List<EnemyProperties> Enemies = new List<EnemyProperties>()
    {
        new EnemyProperties() { ID=0, Skin=Color.green, Lives=1},
        new EnemyProperties() { ID=1, Skin=Color.yellow, Lives=3},
        new EnemyProperties() { ID=2, Skin=Color.red, Lives=7},
    };

    static public EnemyProperties CloneEnemy(EnemyProperties _enemy)
    {
        EnemyProperties NewEnemy = new EnemyProperties()
        {
            ID = _enemy.ID,
            Skin = _enemy.Skin,
            Lives = _enemy.Lives,
        };
        return NewEnemy;
    }

    static public EnemyProperties GetEnemyById(int _id)
    {
        for(int i=0; i < Enemies.Count; i++)
        {
            if(Enemies[i].ID == _id)
            {
                return CloneEnemy(Enemies[i]);
            }
        }
        return null;
    }



}
