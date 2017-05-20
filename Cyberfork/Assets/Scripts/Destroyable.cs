using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{

    public GameObject pieces;
    public int amount = 3;
    public float minBlastPower = 10;
    public float maxBlastPower = 20;

    private void OnDestroy()
    {
        for (int i = 0; i < amount; i++)
        {
            float blast = Random.Range(minBlastPower, maxBlastPower);
            float angle = Random.Range(0, 360);
            float rotation = Random.Range(0, 360);
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            GameObject piece = Instantiate(pieces, transform.position, Quaternion.identity);
            piece.transform.Rotate(0, 0, rotation);
            piece.GetComponent<Rigidbody2D>().velocity = blast * dir;
        }
    }
}
