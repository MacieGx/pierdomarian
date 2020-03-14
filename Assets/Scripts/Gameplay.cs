using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
	private Character character;
	private GameObject mainMenu;
	private GameObject score;
	private GameObject deadMenu;
	AudioSource audio;

	private float jumpDelay = 0.15f; // sec
	private int jumpForce = 200;

	// Start is called before the first frame update
	void Start()
	{
		character = Character.getInstance();
		mainMenu = GameObject.Find("Main Menu");
		score = GameObject.Find("Score");
		deadMenu = GameObject.Find("Dead Menu");
		audio = gameObject.AddComponent<AudioSource>();

		score.SetActive(false);
		deadMenu.SetActive(false);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (!character.isPlaying) return;

		character.isPlaying = false;
		character.canJump = false;

		character.rigidbody2D.AddForce(new Vector2(100, 100));

		audio.PlayOneShot((AudioClip)Resources.Load("Sounds/dead"));
		score.SetActive(false);
		Transform deadMenuScore = deadMenu.transform.Find("Score");
		Transform deadMenuBestScore = deadMenu.transform.Find("Best Score");
		deadMenuScore.Find("Score Value").GetComponent<Text>().text = character.score.ToString();
		deadMenuBestScore.Find("Best Score Value").GetComponent<Text>().text = character.bestScore.ToString();
		deadMenu.SetActive(true);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!character.isPlaying || col.name != "CoinCollision") return;
		character.score++;
		score.GetComponentInChildren<Text>().text = character.score.ToString();

		if(character.score > character.bestScore)
		{
			character.bestScore = character.score;
			if (!character.hasReachRecord)
			{
				character.hasReachRecord = true;
				audio.PlayOneShot((AudioClip)Resources.Load("Sounds/record"));
			}
		}

		audio.PlayOneShot((AudioClip)Resources.Load("Sounds/coins"));
	}

	public void OnButtonPlayClick()
	{
		character.isPlaying = true;
		character.canJump = true;
		character.rigidbody2D.constraints = RigidbodyConstraints2D.None;
		mainMenu.SetActive(false);
		score.SetActive(true);
	}

	public void OnButtonPlayAgainClick()
	{
		score.GetComponentInChildren<Text>().text = "0";

		Tube.DeleteAll();

		character.SetDefaultValues(true);

		character.rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
		character.gameObject.transform.position = Vector3.zero;
		character.rigidbody2D.velocity = Vector3.zero;
		character.gameObject.transform.rotation = Quaternion.identity;
		character.rigidbody2D.constraints = RigidbodyConstraints2D.None;
		deadMenu.SetActive(false);
		score.SetActive(true);

		// remove button focus
		EventSystem.current.SetSelectedGameObject(null);
	}

	// Update is called once per frame
	void Update()
	{
		if (character.canJump && Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine("OnCharacterJump");
		}
	}

	IEnumerator OnCharacterJump()
	{
		character.canJump = false;

		// Reset old jump and falling down force
		character.rigidbody2D.velocity = Vector2.zero;

		// Add jump foce
		character.rigidbody2D.AddForce(Vector2.up * jumpForce);

		// Play jump sound
		audio.PlayOneShot((AudioClip)Resources.Load("Sounds/jump"));

		yield return new WaitForSeconds(jumpDelay);

		character.canJump = true;
	}
}