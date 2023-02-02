using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Control : MonoBehaviour
{

    private SpriteRenderer Rend;
    private List<Sprite> AnimSprite;
    private float SpeedAnim, RateAnim;
    private int CurrentSprite;

    public void InitAnim(SpriteRenderer _rend, List<Sprite> _anim, float speed)
    {
        Rend = _rend;
        AnimSprite = _anim;
        SpeedAnim = speed;
        CurrentSprite = 0;
    }

    void Update()
    {
        RateAnim += Time.deltaTime;
        if (RateAnim >= SpeedAnim)
        {
            Rend.sprite = AnimSprite[CurrentSprite];
            CurrentSprite++;
            if(CurrentSprite >= AnimSprite.Count)
            {
                CurrentSprite = 0;
            }
            RateAnim = 0;
        }
    }
}
