using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed;
    public float moveTime;
    public bool dirRight = true;
    private float timer;
    void Update()
    {
        if (dirRight)
        {
            // Se verdadeiro a serra vai para a direita
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            // Se falso a serra vai para a esquerda
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        timer += Time.deltaTime;

        if (timer >= moveTime)
        {
            // Se o tempo de movimento for maior que o tempo de movimento, inverte a direção
            dirRight = !dirRight;
            timer = 0f;
        }
        
    }
}
