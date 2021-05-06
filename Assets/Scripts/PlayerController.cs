using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;

	// Al momento de iniciar el juego
	void Start()
	{
		// Asignamos el componente Rigidbody a nuestra variable
		rb = GetComponent<Rigidbody>();

		// Inicializamos el contador en 0
		count = 0;

		SetCountText();

		//Ponemos el texto de ganar o win en false para que aparezca solo cuando gane 
		winTextObject.SetActive(false);
	}

	void FixedUpdate()
	{
		// Creamos un vector de 3 para asignar las posiciones en las que se moverá nuestro objeto
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		// Si el objeto choca con alguno que tenga la etiqueta pues desaparecera
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);

			// Nuestro contador aumentara
			count = count + 1;

			// Y la funcion para mostrar el texto debe llamarse
			SetCountText();
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 12)
		{
			// En caso gane deberá mostrarse recien
			winTextObject.SetActive(true);
		}
	}
}