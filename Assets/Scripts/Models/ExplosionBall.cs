using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionBall : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public SpriteRenderer explosion;
    public float timeShow = 1f;
    public float timeHide = 3f;
    public float maxScale = 2f;

    public void Init(Vector3 positionExplosion, Vector3 velocity, Color colorExplosion)
    {
        Vector3 tempScale = transform.localScale * velocity.y / 4;

        if (tempScale.x > maxScale)
        {
            tempScale.x = maxScale;
            tempScale.y = maxScale;
        }
        else if (tempScale.x < -maxScale)
        {
            tempScale.x = -maxScale;
            tempScale.y = -maxScale;
        }

        if (sprites.Count != 0)
            explosion.sprite = sprites[Random.Range(0, sprites.Count)];

        transform.position = positionExplosion;
        transform.localScale = tempScale;
        explosion.color = colorExplosion;

        explosion.DOFade(1, timeShow).OnComplete(() => explosion.DOFade(0, timeHide).OnComplete(() => Destroy(gameObject)));
    }
}
