using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {

    Stop,

    Follow,

    Die,

    Attack

};

public enum EnemyType {
    Melee,

    Ranged
}

public class EnemyController : MonoBehaviour
{

    GameObject player;
    public EnemyState currState = EnemyState.Stop;

    public EnemyType enemyType;

    public float range;

    public float speed;

    public float attackRange;

    public float cooldown;

    private bool dead = false;

    private bool cooldownAttack = false;

    public GameObject bulletPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState) {
            case (EnemyState.Stop):
                StopAllCoroutines();
            break;
            case (EnemyState.Follow):
                Follow();
            break;
            case (EnemyState.Die):
                
            break;
            case (EnemyState.Attack):
                Attack();
            break;
        }

        if(IsPlayerInRange(range) && currState != EnemyState.Die) {
            currState = EnemyState.Follow;
        }
        else if(!IsPlayerInRange(range) && currState != EnemyState.Die) {
            currState = EnemyState.Stop;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange) {
            currState = EnemyState.Attack;
        }

    }

    private bool IsPlayerInRange(float range) {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }


    void Follow() {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack() {
        if (!cooldownAttack) {
            switch (enemyType) {
                case (EnemyType.Melee):
                    GameController.DamagePlayer(1);
                    StartCoroutine(Cooldown());
                break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(Cooldown());
                break;
            }
        }
    }

    private IEnumerator Cooldown() {
        cooldownAttack = true;
        yield return new WaitForSeconds(cooldown);
        cooldownAttack = false;
    }

    public void Death() {
        Destroy(gameObject);
    }

}
