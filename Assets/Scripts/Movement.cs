using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

    Rigidbody2D player1RB;
    Rigidbody2D player2RB;
    public bool isGrounded1 = true;
    public bool isGrounded2 = true;

    public float jumpSpeed = -20F;
    public float movementSpeed = 20F;

	// Use this for initialization
	void Start () {

        player1RB = player1.GetComponent<Rigidbody2D>();
        player2RB = player2.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
 
    }

    void FixedUpdate()
    {
        float horizontal1 = Input.GetAxis("Horizontal1");
        float horizontal2 = Input.GetAxis("Horizontal2");
        float vertical1 = Input.GetAxis("Vertical1");
        float vertical2 = Input.GetAxis("Vertical2");

        //Can jump when is over the ground
        //isGrounded1 = player1.transform.position.y <= 0.64F;
        //isGrounded2 = player2.transform.position.y <= 0.64F;

        //Can jump when Y velocity is 0
        isGrounded1 = player1RB.velocity.y == 0;
        isGrounded2 = player2RB.velocity.y == 0;

        //Player 1
        //Horizontal movement
        if (horizontal1 != 0)
        {
            if (horizontal1 > 0)
                player1.transform.eulerAngles = new Vector2(0, 0);
            if (horizontal1 < 0)
                player1.transform.eulerAngles = new Vector2(0, 180);
            player1.transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        }
        //Vertical movement
        if (vertical1 > 0 && isGrounded1)
        {
            player1RB.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            isGrounded1 = false;
        }

        //Player 2
        //Horizontal movement
        if (horizontal2 != 0)
        {
            if (horizontal2 > 0)
                player2.transform.eulerAngles = new Vector2(0, 0);
            if (horizontal2 < 0)
                player2.transform.eulerAngles = new Vector2(0, 180);
            player2.transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        }
        //Vertical movement
        if (vertical2 > 0 && isGrounded2)
        {
            player2RB.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            isGrounded2 = false;
        }

    }
}
