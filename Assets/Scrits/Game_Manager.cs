using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private GameObject Player;
    public Vector2 LimitsStage;
    public Vector2 InitEnemiesPos;
    public float SpeedPlayer;

    private World_Creator.WorldProperties CurrentWorld;

    private List<GameObject> SavedEnemies;

    private float EnemySpeed, TotalEnemySpeed;

    public enum EnemyDir { RIGHT, LEFT };
    public EnemyDir Direction;
  


    void Start()
    {
        PrintStage();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        EnemyMovement();
    }
    private void PrintStage()
    {
        //Imprimir Mundo
        CurrentWorld = World_Creator.GetWorldById(1);
        
        
        //Imprimir Player
        Player = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Player.name = "Player";
        Player.transform.position = new Vector2(0, -7);
        Destroy(Player.GetComponent<MeshCollider>());
        StartCoroutine(AddCollider(Player));
        Player.GetComponent<Renderer>().enabled = false;

        GameObject SpritePlayer = new GameObject("PlayerSprite");
        SpritePlayer.transform.SetParent(Player.transform);
        SpritePlayer.transform.localScale = new Vector3(1f, 1f, 1f);
        SpritePlayer.transform.localPosition = new Vector2(0, 0);
        SpritePlayer.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Ship_Player");

        //Imprimir Enemigos

        SavedEnemies = new List<GameObject>();
        TotalEnemySpeed = 0.5f;

        for (int i = 0; i< CurrentWorld.EnemiesID.Count; i++)
        {
            for (int j = 0; j< CurrentWorld.Columns; j++)
            {
                GameObject NewEnemy = GameObject.CreatePrimitive(PrimitiveType.Quad);
                
                NewEnemy.name = "Enemy_" + CurrentWorld.EnemiesID[i];
                NewEnemy.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                //NewEnemy.transform.position = new Vector2(j, -i);
                NewEnemy.transform.position = new Vector2(InitEnemiesPos.x +j, InitEnemiesPos.y - i);
                Destroy(NewEnemy.GetComponent<MeshCollider>());
                StartCoroutine(AddCollider(NewEnemy));
                
                //Clase Hoy ==================================================================================

                NewEnemy.GetComponent<Renderer>().enabled = false;

                GameObject SpriteEnemy = new GameObject(NewEnemy.name);
                SpriteEnemy.transform.SetParent(NewEnemy.transform);
                SpriteEnemy.transform.localScale = new Vector3(1f, 1f, 1f);
                SpriteEnemy.transform.localPosition = new Vector2(0, 0);
                
                
                SpriteEnemy.AddComponent<SpriteRenderer>().sprite = 
                    Resources.Load<Sprite>(CurrentWorld.EnemiesID[i] + "/" + CurrentWorld.EnemiesID[i]+"_1");

                Enemy_Creator.EnemyProperties TempEnemy = Enemy_Creator.GetEnemyById(CurrentWorld.EnemiesID[i]);
                SpriteEnemy.GetComponent<SpriteRenderer>().color = TempEnemy.Skin;
            

                NewEnemy.AddComponent<Anim_Control>().InitAnim(SpriteEnemy.GetComponent<SpriteRenderer>(),
                    new List<Sprite>(Resources.LoadAll<Sprite>(CurrentWorld.EnemiesID[i].ToString())), 0.5f);
                SavedEnemies.Add(NewEnemy);
            }
        }

    }
    IEnumerator AddCollider(GameObject _obj)
    {
        yield return new WaitForSeconds(0.1f);
        _obj.AddComponent<BoxCollider2D>().isTrigger = true;
        _obj.AddComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void PlayerMovement()
    {
        Player.transform.Translate(Vector2.right * SpeedPlayer * Input.GetAxis("Horizontal") * Time.deltaTime);
        Vector2 CurrentPos = Player.transform.position;
        CurrentPos.x = Mathf.Clamp(CurrentPos.x, LimitsStage.x, LimitsStage.y);
        Player.transform.position = CurrentPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateBullet(BulletType.PLAYER, Player.transform.position, 30);
        }
    }

    private void EnemyMovement()
    {
        EnemySpeed += Time.deltaTime;
        if (EnemySpeed >= TotalEnemySpeed)
        {
            for (int i = 0; i < SavedEnemies.Count; i++)
            {
                switch (Direction)
                {
                    case EnemyDir.RIGHT:
                        SavedEnemies[i].transform.position =
                            new Vector2(SavedEnemies[i].transform.position.x + 1, SavedEnemies[i].transform.position.y);
                        break;
                    case EnemyDir.LEFT:
                        SavedEnemies[i].transform.position =
                            new Vector2(SavedEnemies[i].transform.position.x - 1, SavedEnemies[i].transform.position.y);
                        break ;

                }
            }
            //Comprobar si algún enemigo ha llegado al límite de la pantalla
            CheckEnemyLimits();
            EnemySpeed = 0;
        }
    }
    private void CheckEnemyLimits()
    {
        for(int i = 0; i < SavedEnemies.Count; i++)
        {
            if(SavedEnemies[i].transform.position.x <= LimitsStage.x) //Moviendo a la izquierda
            {
                Direction = EnemyDir.RIGHT;
                break;
            }
            if(SavedEnemies[i].transform.position.x >= LimitsStage.y)
            {
                Direction=EnemyDir.LEFT;
                break;
            }
        }
        
    }
    private void CreateBullet(BulletType _type, Vector2 _pos, float _speed)
    {
        GameObject NewBullet = GameObject.CreatePrimitive(PrimitiveType.Quad);
        NewBullet.name = "Bullet";
        NewBullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        NewBullet.transform.position = _pos;
        Destroy(NewBullet.GetComponent<MeshCollider>());
        StartCoroutine(AddCollider(NewBullet));

        NewBullet.AddComponent<Bullet_Control>().InitBullet(_type, _speed);
    }
}
