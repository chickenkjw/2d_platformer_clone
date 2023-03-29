using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void Start()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.CompareTag("Player")) {
            Invoke(nameof(Finish), 0.2f);
        }
	}

    private void Finish() {
        Time.timeScale = 0f;
    }
}
