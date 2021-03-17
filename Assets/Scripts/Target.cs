using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private CircleCollider2D currentCollider;
    private Vector2 size;
    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentCollider = GetComponent<CircleCollider2D>();
        size = currentCollider.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        size = currentCollider.bounds.size;
        DetectFire();
    }

    Vector2 GetMousePosition() {
        Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Camera.main.ScreenToWorldPoint(currentMousePosition);
    }

    void DetectFire() {
        Vector2 xBounds = new Vector2(transform.position.x - size.x / 2, transform.position.x + size.x / 2);
        Vector2 yBounds = new Vector2(transform.position.y - size.y / 2, transform.position.y + size.y / 2);

        Vector2 currentMousePosition = GetMousePosition();
        if ((currentMousePosition.x > xBounds.x && currentMousePosition.x < xBounds.y) &&
            (currentMousePosition.y > yBounds.x && currentMousePosition.y < yBounds.y))
        {
            Vector2 relativeMousePosition = new Vector2(currentMousePosition.x - transform.position.x,
            currentMousePosition.y - transform.position.y);
            gameManager.ShowAimAxis();
            gameManager.UpdateAxisPosition(relativeMousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                gameManager.UpdateScore(relativeMousePosition);
            }

        }
        else {
            gameManager.HideAimAxis();
        }

    }
}
