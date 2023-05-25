using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    Animator animator;

    int health = 3;

    [SerializeField] Text healthText;

    Vector3 initialPosition;
    CharacterController characterController;
    [SerializeField] AudioSource gameOverSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        healthText.text = "Vidas: " + health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy Body"))
        {
            LoseLife();
        }
        else if (other.gameObject.CompareTag("World"))
        {
            FallOutOfWorld();
        }

        if (other.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(other.transform.parent.gameObject);
        }
    }

    void LoseLife()
    {
        gameOverSound.Play();

        health--;
        if (health <= 0)
        {
            GoToGameOver();
        }
        else
        {
            ResetPlayerPosition();
        }
    }

    void FallOutOfWorld()
    {
        LoseLife();
    }

    void ResetPlayerPosition()
    {
        characterController.enabled = false;
        transform.position = initialPosition;
        characterController.enabled = true;
    }

    void GoToGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
