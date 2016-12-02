using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	//Objetos
	public GameObject topWall;					//Muro con un collider para detectar cuando está la bola fuera de pantalla
	public Text contador;						//Contador
	public Text textAux;						//Mensajes auxiliares
	public Text puntosJugador1;					//Puntos del jugador 1
	public Text puntosJugador2;					//Puntos del jugador 2

	public GameObject ballOutOfScreenGoingTop;	//Prefab que aparecerá cuando la bola esté fuera de pantalla
	GameObject ballOutOfScreenActive;			//Objeto clonado del prefab que se inicializa 
												//al entrar al collider y se destruye al salir
	public GameObject ballOutOfScreenGoingBot;	//Prefab que aparecerá cuando la bola esté fuera de pantalla
	Sprite ballOutOfScreenGoingBotSprite; 		//Render del objeto anterior, que cambiará el sprite cuando la dirección
												//  de la bola sea negativa (hacia abajo)
	AudioSource jumpSound;						//Sonido cuando salta
	//Variables
	public int winner = -1;						//Indica quién ha ganado
												//	-1 = Aún no ha empezado
												//	0 = La partida está en juego
												//	1 = Ha ganado el jugador 1
												//	2 = Ha ganado el jugador 2
    float minForce = 9F;						//Fuerza mínima para lanzar la bola al inicio
    float maxForce = 13.51F;						//Fuerza máxima para lanzar la bola al inicio
    float timer = 3;							//Tiempo inicial del contador
    bool started = false;						//Booleano para saber si la partida ha empezado
    bool timerDone = false;						//Booleano para saber si el contador ha finalizado
	Vector3 posicionInicial;					//Posición inicial de la bola
	int puntos1;								//Puntos del jugador 1
	int puntos2;								//Puntos del jugador 2
	public bool paused = false;
    // Use this for initialization
    void Start()
	{
		posicionInicial = this.transform.position;
		puntos1 = 0;
		puntos2 = 0;
		ballOutOfScreenGoingBotSprite = ballOutOfScreenGoingBot.GetComponent<SpriteRenderer> ().sprite;
		jumpSound = this.GetComponent<AudioSource> ();
		contador.gameObject.SetActive (false);
		textAux.gameObject.SetActive (true);
    }
	
	// Update is called once per frame
	void Update () {
        if (started && !timerDone)
        {
			LanzarBola ();
        }
		else if (!timerDone && !started && winner == 0)
        {
			Contador ();
        }
		if (winner != 0) {
			//Si se pulsa el Espacio, reiniciamos la partida
			if (Input.GetAxis("Restart") != 0) {
				Restart ();
			}
		}

		if (ballOutOfScreenActive != null) {
			if (this.GetComponent<Rigidbody2D>().velocity.y < 0) { 
				//Si está cayendo, cambiamos el sprite
				ballOutOfScreenActive.GetComponent<SpriteRenderer>().sprite = ballOutOfScreenGoingBotSprite;				
			}
		}
	}
	void OnTriggerEnter2D (Collider2D other)
	{
		//Si contacta con el scanner (la bola está fuera de la pantalla), inicializa un prefab para indicar por dónde va la bola
		if (other.name == "TopWallScanner") {
			ballOutOfScreenActive = (GameObject) Instantiate (ballOutOfScreenGoingTop, new Vector3 (this.transform.position.x, 4.2F, 0), new Quaternion (0, 0, 0, 0));
		}
	}
	void OnTriggerExit2D(Collider2D other){
		//Si sale del scanner (la bola se ve en la pantalla), destruye el prefab indicando por donde va la bola
		if (other.name == "TopWallScanner") {
			Destroy (ballOutOfScreenActive);
		}
	}
	void OnTriggerStay2D(Collider2D other) {
		//Mientras está la bola en el scanner (fuera de la pantalla), actualiza su posición respecto al eje X de la bola
		if (other.name == "TopWallScanner") {
			ballOutOfScreenActive.transform.position = new Vector3(this.transform.position.x, ballOutOfScreenActive.transform.position.y,0);
		}
	}
	void OnCollisionEnter2D(Collision2D coll){
		//Si la bola contacta con el suelo, comprobamos quién ha ganado
		if (coll.gameObject.name == "Ground") {
			if (this.transform.position.x < 0) {
				winner = 2;
				puntos2++;
				puntosJugador2.text = puntos2.ToString ();
			} else {
				winner = 1;
				puntos1++;
				puntosJugador1.text = puntos1.ToString ();
			}
			textAux.gameObject.SetActive (true);
			textAux.text = "Punto para el jugador "+winner;
			//Bloqueamos la bola
			this.GetComponent<Rigidbody2D>().isKinematic = true;
		}
		if (coll.gameObject.name.StartsWith("Player")) {
			jumpSound.Play ();
		}
	}

	public void Pause ()
	{
		this.GetComponent<Rigidbody2D>().isKinematic = true;
	}

	public void Resume ()
	{
		this.GetComponent<Rigidbody2D>().isKinematic = false;
	}

	void Contador ()
	{
		timer -= Time.deltaTime;
		Debug.Log (Time.timeScale);
		if (timer >= 2 && timer < 3)
			contador.text = "2";                    
		else if (timer >= 1 && timer < 2)
			contador.text = "1";
		else if (timer >= 0 && timer < 1)
			contador.text = "GO";
		else if( timer < -0.3)
		{
			contador.text = "";
			started = true;
			//Ponemos la bola como kinematic para que le afecten las fuerzas,
			//  ya que durante el timer se encontraba "pausada"
			GetComponent<Rigidbody2D>().isKinematic = false;
			contador.gameObject.SetActive (false);
		}
	}

	void Restart ()
	{
		this.transform.position = posicionInicial;
		winner = 0;
		started = false;
		timerDone = false;
		timer = 3.7F;
		contador.text = "3";
		contador.gameObject.SetActive (true);
		textAux.gameObject.SetActive (false);
	}

	void LanzarBola ()
	{
		//Impulsa inicialmente la bola hacia un lado aleatorio y con una fuerza aleatoria
		Vector2 vectorAux;
		float forceAux;
		//Obteniendo fuerza aleatoria
		forceAux = Random.Range(minForce, maxForce);
		//Obteniendo lado aleatorio
		if (Random.Range(0, 2) == 0)
			vectorAux = Vector2.left * forceAux;
		else
			vectorAux = Vector2.right * forceAux;
		//Impulsando bola
		GetComponent<Rigidbody2D>().AddForce(vectorAux * forceAux);
		timerDone = true;
	}

	public void StartContador(){
		started = false;
		timerDone = false;
		timer = 3.7F;
		contador.text = "3";
		contador.gameObject.SetActive (true);
		Contador ();
	}
}
