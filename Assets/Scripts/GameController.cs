using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static float health = 3;

    private static float maxHealth = 3;

    private static float moveSpeed = 5f;

    private static float fireRate = 0.5f;

    private static float bulletSize = 0.5f;

    private bool speederCollected = false;

    private bool bombCollected = false;

    private List<string> collectedNames = new List<string>();


    public static float Health { get => health; set => health = value; }

    public static float MaxHealth { get => maxHealth; set => MaxHealth = value; }

    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }



    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate)
    {
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }

    public void UpdateCollectedItems(CollectionController item)
    {
        collectedNames.Add(item.item.name);

        foreach(string i in collectedNames)
        {
            switch (i)
            {
                case "Speeder":
                    speederCollected = true;
                    break;
                case "Bomb":
                    bombCollected = true;
                    break;
            }
        }

        if(speederCollected && bombCollected)
        {
            FireRateChange(0.25f);
        }

    }

    private static void KillPlayer()
    {

    }

}
