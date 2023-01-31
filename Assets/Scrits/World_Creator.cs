using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Creator : MonoBehaviour
{
    public class WorldProperties
    {
        public string Name;
        public int ID;
        public int Lives;
        public int Seconds;

        public int Columns;
        public List<int> EnemiesID;
    }

    static public List<WorldProperties> Worlds = new List<WorldProperties>()
    {
        new WorldProperties()
        {
            Name = "Mundo 1", ID = 0, Lives = 3, Seconds = 60, Columns = 7, EnemiesID = new List<int>(){0,2},
        },
         new WorldProperties()
        {
            Name = "Mundo 2", ID = 1, Lives = 5, Seconds = 40, Columns = 8, EnemiesID = new List<int>(){2,2,1,0},
        },

    };

    static public WorldProperties GetWorldById(int _id)
    {
        for (int i=0; i<Worlds.Count; i++)
        {
            if (Worlds[i].ID == _id)
            {
                return Worlds[i];
            }
        }
        return null;
    }
}
