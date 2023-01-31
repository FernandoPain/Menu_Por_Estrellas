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
  


    void Start()
    {
        PrintStage();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
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
                NewEnemy.GetComponent<Renderer>().enabled = true;

                GameObject SpriteEnemy = new GameObject(NewEnemy.name);
                SpriteEnemy.transform.SetParent(Player.transform);
                SpriteEnemy.transform.localScale = new Vector3(1f, 1f, 1f);
                SpriteEnemy.transform.localPosition = new Vector2(0, 0);
                SpriteEnemy.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Ship_Player");
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
    }
}
