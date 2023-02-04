using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Roots : MonoBehaviour
{
    Vector2 dir = Vector2.right;
    bool ate = false;
    [SerializeField] GameObject tailPrefab;
    [SerializeField] GameObject foodPrefab;
    [SerializeField] Transform foodSpawnPoint;

    List<Transform> tail = new List<Transform>();

    private void Start()
    {
        InvokeRepeating("Move", 2f, 2f);
    }

    void Move()
    {
        Vector2 v = transform.position;
        float rand = Random.Range(-1, 1);
        v.y = rand;

;
        transform.Translate(dir);

        if (ate)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            tail.Insert(0, g.transform);

            ate = false;
        }

        else if (tail.Count > 0)
        {
            tail.Last().position = v;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bark"))
        {
            ate = true;
            Destroy(collision.gameObject);

            var food = Instantiate(foodPrefab, foodSpawnPoint.position, Quaternion.identity);
        }
        else
        {

        }
    }
}
