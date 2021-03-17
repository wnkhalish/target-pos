using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isScoreUpdated) {
            UpdateBulletPosition();
        }
    }

    void UpdateBulletPosition() {
        Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = Camera.main.ScreenToWorldPoint(currentMousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
