using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	//Objetos
    public GameObject player1;				//Jugador 1
    public GameObject player2;				//Jugador 2
	Ball ballScript;						//Script
	public GameObject ballWithTheScript;	//Objeto que posee el script anterior
    Rigidbody2D player1RB;					//RigidBody del jugador 1
    Rigidbody2D player2RB;					//RigidBody del jugador 2

	//Variables
	public bool player2Active;			//Si es false, jugará contra la máquina
	bool isGrounded1 = true;			//Indica si si eje Y está quieto, para poder saltar
	bool isGrounded2 = true;			//Indica si si eje Y está quieto, para poder saltar
	float jumpSpeed = 20F;					//Velocidad de salto (fuerza)
	float movementSpeed = 5F;				//Velocidad de movimiento
	float movementSpeedIA = 3F;

	public GameObject pauseCanvas; //Canvas que muestra "Pausa" en medio de la pantalla al pulsar Escape
	// Use this for initialization
	void Start () {
        player1RB = player1.GetComponent<Rigidbody2D>();
        player2RB = player2.GetComponent<Rigidbody2D>();
		ballScript = ballWithTheScript.GetComponent<Ball> ();
		StartCoroutine(PauseAndResume());
    }

	IEnumerator PauseAndResume ()
	{
		while (true) {
			if (Input.GetKeyDown ("escape")) {
				if (Time.timeScale == 1.0) {
					Time.timeScale = 0.0F;
					pauseCanvas.SetActive (true);
				} else {
					//ballScript.StartContador ();
					Time.timeScale = 1.0F;
					pauseCanvas.SetActive (false);
				}
			}
			yield return null;
		}

	}

	void Pause ()
	{
		
	}	

	void Resume ()
	{
		
	}

    void FixedUpdate()
	{
		//Si es 0, es que la partida está en curso
		if (ballScript.winner == 0) {
			if (!ballScript.paused) {
				player1RB.isKinematic = false;
				player2RB.isKinematic = false;
			}
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

			if (player2Active) {
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
				Vector3 ballPosition = ballWithTheScript.transform.position;
				if (ballPosition.x > 0) { //Si la bola está en su campo, se moverá
					//Movimiento horizontal
					if (ballPosition.x < player2.transform.position.x) { //Se moverá hacia la izquierda
						//player2.transform.eulerAngles = new Vector2 (0, 0);
						player2RB.velocity = new Vector2 (-1 * movementSpeedIA, player2RB.velocity.y);
					} else if (ballPosition.x > player2.transform.position.x) {
						//player2.transform.eulerAngles = new Vector2 (0, 180);
						player2RB.velocity = new Vector2 (movementSpeedIA, player2RB.velocity.y);
					}
					//Movimiento vertical
					if (ballPosition.y <= 0.05F && isGrounded2) {
						player2RB.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
						isGrounded2 = false;
					}
				} else {
					//Se mueve al centro
					if (ballPosition.x != 4.5F) {
						player2.transform.position = Vector2.MoveTowards (player2.transform.position, new Vector2 (4.5F, -2.2778F), 0.08F);
						//if (4.5F < player2.transform.position.x) { //Se moverá hacia la izquierda
						//	//player2.transform.eulerAngles = new Vector2 (0, 0);
						//	player2RB.velocity = new Vector2 (-1 * movementSpeedIA, player2RB.velocity.y);
						//} else if (4.5F > player2.transform.position.x) {
						//	//player2.transform.eulerAngles = new Vector2 (0, 180);
						//	player2RB.velocity = new Vector2 (movementSpeedIA, player2RB.velocity.y);
						//}
					}
				}
			}
		} else {
			//Si la partida finaliza, bloqueamos los jugadores y llamamos la escena del menú
			player1RB.isKinematic = true;
			player2RB.isKinematic = true;
		}
    }
}
