using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

    float minForce = 20;
    float maxForce = 55;
    public float timer = 3;
    public GameObject textObject;
    public TextMesh text;
    public bool started = false;
    public bool timerDone = false;
    // Use this for initialization
    void Start()
    {
        text = textObject.GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        if (started && !timerDone)
        {
            Vector2 vectorAux;
            float forceAux;
            forceAux = Random.Range(minForce, maxForce);
            if (Random.Range(0, 2) == 0)
                vectorAux = Vector2.left * forceAux;
            else
                vectorAux = Vector2.right * forceAux;
            GetComponent<Rigidbody2D>().AddForce(vectorAux * forceAux);
            timerDone = true;
        }
        else if (!timerDone && !started)
        {
            timer -= Time.deltaTime;
            if (timer >= 2 && timer < 3)
                text.text = "2";                    
            else if (timer >= 1 && timer < 2)
                text.text = "1";
            else if (timer >= 0 && timer < 1)
                text.text = "GO";
            else if( timer < -0.5)
            {
                text.text = "";
                started = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
	}
}
