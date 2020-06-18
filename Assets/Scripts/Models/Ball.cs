using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isChangeRotation;

    public Rigidbody2D rigidbodyBall;
    public SpriteRenderer spriteRenderer;
    public ParticleSystem trail;
    public ExplosionBall explosion;
    public float speedBall;
    public float maxVelocity = 10;
    public float minVelocity = 4;

    private void Start()
    {
        Init();
    }  

    public void Init()
    {
        rigidbodyBall.AddForce(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)).normalized * speedBall);
        StartCoroutine(ControllerBallProcess());
    }

    public void SetColor(Color color)
    {
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(color, 1.0f), new GradientColorKey(color, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        var col = trail.colorOverLifetime;
        col.enabled = true;
        col.color = grad;

        spriteRenderer.color = color;
    }

    public void DestroyBall()
    {
        Destroy(gameObject);
        Instantiate(explosion).Init(transform.position, rigidbodyBall.velocity, spriteRenderer.color);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Gates")
        {
            DestroyBall();
            GameController.instance.InitNewRound();
        }
        else 
        {
            if (isChangeRotation)
            {
                rigidbodyBall.AddForce(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)).normalized * speedBall);
                isChangeRotation = false;
            }

            AddForce(maxVelocity);
        }
    }

    private void AddForce(float border)
    {
        if (rigidbodyBall.velocity.magnitude < border)
            rigidbodyBall.velocity *= 1.1f;
    }

    private IEnumerator ControllerBallProcess()
    {
        while (true)
        {
            yield return null;

            if (rigidbodyBall.velocity.x == 0 || rigidbodyBall.velocity.y == 0)
                isChangeRotation = true;

            AddForce(minVelocity);
        }
    }
}
