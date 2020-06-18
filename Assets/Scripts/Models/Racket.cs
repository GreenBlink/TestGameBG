using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    public Rigidbody2D rigidbodyRacket;

    public void MoveRocket(Vector3 moveVector, float speedMoveRacket)
    {
        rigidbodyRacket.AddForce(moveVector * speedMoveRacket, ForceMode2D.Impulse);
    }
}
