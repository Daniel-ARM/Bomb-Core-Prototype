using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;
    new Rigidbody2D rigidbody;
    public Text collectedText;
    public static int collectedAmount = 0;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    } 

    // Update is called once per frame
    void Update()
    {
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigidbody.velocity = new Vector2(h, v).normalized * speed;

        collectedText.text = "Coins: " + collectedAmount;

        float shootHor = Input.GetAxisRaw("ShootHorizontal");
        float shootVert = Input.GetAxisRaw("ShootVertical");
        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay) {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }

    }

    void Shoot(float x, float y) {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
        (x < 0) ? Mathf.Floor(x) : Mathf.Ceil(x),
        (y < 0) ? Mathf.Floor(y) : Mathf.Ceil(y),
        0
        ).normalized * bulletSpeed;
    }
}
