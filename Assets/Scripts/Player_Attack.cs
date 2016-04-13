using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Attack : MonoBehaviour
{
    [Header("Player Information")]
    public Transform player_Transform;
    public Transform camera_Transform;

    [Header("Weapon Information")]
    public Transform bullet_SpawnPoint;
    public LayerMask layerMask_IgnoreLayer;
    public Transform weapon_AimPoint;
    public Transform weapon_RandomPoint;
    public GameObject bulletHole_Wall;

    [Header("Weapon Data")]
    public List<Player_Weapons> player_Weapons;
 
    [Header("Current Weapon Variables")]
    public int weapon_Current;
    public float weapon_Damage;

    [Header("Boolean Variables")]
    public bool player_IsPullingTrigger;
    public bool player_HasPulledTrigger;
    public bool player_HasReleasedTrigger;

    void Start()
    {
        player_Transform = GetComponent<Transform>();
        bullet_SpawnPoint = GameObject.Find("Bullet_SpawnPoint").GetComponent<Transform>();

        layerMask_IgnoreLayer = ~layerMask_IgnoreLayer;
    }

    void Update()
    {
        VariableUpdates();
        FireWeapon();
    }

    void VariableUpdates()
    {
        player_IsPullingTrigger = (Input.GetMouseButton(0)) ? true : false;

        if (Input.GetMouseButtonDown(0))
        {
            player_HasPulledTrigger = true;
            player_HasReleasedTrigger = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            player_HasPulledTrigger = false;
            player_HasReleasedTrigger = true;
        }

        weapon_AimPoint.localPosition = new Vector3(0, 0, player_Weapons[weapon_Current].weapon_Range);

        if (Input.GetKeyDown("1"))
        {
            weapon_Current = 0;
        }

        if (Input.GetKeyDown("2"))
        {
            weapon_Current = 1;
        }

        if (Input.GetKeyDown("3"))
        {
            weapon_Current = 2;
        }
    }

    void FireWeapon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = player_Weapons[weapon_Current].weapon_RayCastsPerShot; i > 0; i--)
            {
                Vector3 randomPoint = Random.insideUnitSphere * player_Weapons[weapon_Current].weapon_Accuracy;

                weapon_RandomPoint.position = weapon_AimPoint.position + randomPoint;

                //Get vector direction by subtracting the destination's position by the origin's position
                Vector3 vectorDirection = weapon_RandomPoint.position - camera_Transform.position;

                Ray ray = new Ray(camera_Transform.position, vectorDirection);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, player_Weapons[weapon_Current].weapon_Range))
                {
                    if(hit.collider.tag == "Enemy")
                    {
                        Enemy_Controller enemy_Controller = hit.collider.gameObject.GetComponent<Enemy_Controller>();
                        enemy_Controller.DamageTaken(weapon_Damage);
                    }

                    Instantiate(bulletHole_Wall, hit.point, Quaternion.FromToRotation(Vector3.back, hit.normal));
                    Debug.DrawLine(camera_Transform.position, hit.point, Color.blue);
                }
            }
        }
    }
}
