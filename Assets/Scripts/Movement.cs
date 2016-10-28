using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

<<<<<<< HEAD
	//Objetos
    public GameObject player1;				//Jugador 1
    public GameObject player2;				//Jugador 2
	Ball ballScript;						//Script
	public GameObject ballWithTheScript;	//Objeto que posee el script anterior
    Rigidbody2D player1RB;					//RigidBody del jugador 1
    Rigidbody2D player2RB;					//RigidBody del jugador 2

	//Variables
	bool isGrounded1 = true;			//Indica si si eje Y está quieto, para poder saltar
	bool isGrounded2 = true;			//Indica si si eje Y está quieto, para poder saltar
	float jumpSpeed = 20F;					//Velocidad de salto (fuerza)
	float movementSpeed = 5F;				//Velocidad de movimiento

	// Use this for initialization
	void Start () {
        player1RB = player1.GetComponent<Rigidbody2D>();
        player2RB = player2.GetComponent<Rigidbody2D>();
		ballScript = ballWithTheScript.GetComponent<Ball> ();
=======
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
 
>>>>>>> e9c91d9efdb39a150973bd2043e183c6365b93c5
    }

    void FixedUpdate()
    {
<<<<<<< HEAD
		//Si es 0, es que la partida está en curso
		if (ballScript.winner == 0) {
			player1RB.isKinematic = false;
			player2RB.isKinematic = false;

			float horizontal1 = Input.GetAxis ("Horizontal1");
			float horizontal2 = Input.GetAxis ("Horizontal2");
			float vertical1 = Input.GetAxis ("Vertical1");
			float vertical2 = Input.GetAxis ("Vertical2");

			//Puede saltar cuando su velocidad en Y es 0
			isGrounded1 = player1RB.position.y <= -2.22F;
			isGrounded2 = player2RB.position.y <= -2.22F;

			//Jugador 1
			//Movimiento horizontal
			if (horizontal1 != 0) {
				if (horizontal1 > 0) {
					player1.transform.eulerAngles = new Vector2 (0, 0);
					//aux = 
				}
				if (horizontal1 < 0) {
					player1.transform.eulerAngles = new Vector2 (0, 180);
				}

				player1RB.velocity = new Vector2 (horizontal1 * movementSpeed, player1RB.velocity.y);
				//player1.transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
			}
			//Movimiento vertical
			if (vertical1 > 0 && isGrounded1) {
				player1RB.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
				isGrounded1 = false;
			}

			//Jugador 2
			//Movimiento horizontal
			if (horizontal2 != 0) {
				if (horizontal2 > 0) {
					player2.transform.eulerAngles = new Vector2 (0, 180);
				}
				if (horizontal2 < 0) {
					player2.transform.eulerAngles = new Vector2 (0, 0);
				}

				player2RB.velocity = new Vector2 (horizontal2 * movementSpeed, player2RB.velocity.y);
				//player2.transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
			}
			//Movimiento vertical
			if (vertical2 > 0 && isGrounded2) {
				player2RB.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
				isGrounded2 = false;
			}
		} else {
			//Si la partida finaliza, bloqueamos los jugadores y llamamos la escena del menú
			player1RB.isKinematic = true;
			player2RB.isKinematic = true;
		}
=======
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

>>>>>>> e9c91d9efdb39a150973bd2043e183c6365b93c5
    }
}
