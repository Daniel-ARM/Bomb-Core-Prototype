using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    new Rigidbody2D rigidbody;
    public Text collectedText;
    public static int collectedAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    } 

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigidbody.velocity = new Vector2(h * speed, v * speed).normalized;
        collectedText.text = "Items collected: " + collectedAmount;


    }
}
