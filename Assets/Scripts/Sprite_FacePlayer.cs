using UnityEngine;
using System.Collections;

public class Sprite_FacePlayer : MonoBehaviour
{
    public Transform player_Transform;
    
    void Start()
    {
        player_Transform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - player_Transform.position);

        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }
}
