using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ReviewBullet : MonoBehaviour
{
    private Collider2D col;
    private SpriteRenderer render;
    private Vector2 size;
    private GameManager gameManager;
    private Collider2D targetCollider;
    private bool isDragging;

    void Start()
    {
        col = this.GetComponent<Collider2D>();
        render = this.GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetCollider = GameObject.Find("ReviewScreen").transform.GetChild(0).gameObject.GetComponent<Collider2D>();
        size = col.bounds.size;
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isEditingBullets)
        {
            DetectMouseClick();
        }
    }

    Vector2 GetMousePosition()
    {
        Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Camera.main.ScreenToWorldPoint(currentMousePosition);
    }

    public void SelectBullet()
    {
        render.color = new UnityEngine.Color(0.8156863f, 0.1921569f, 0.1921569f, 1f);
    }

    public void UnSelectBullet()
    {
        render.color = new UnityEngine.Color(0.1921569f, 0.7490196f, 0.8156863f, 1f);
    }


    void DetectMouseClick()
    {
        Vector2 xBounds = new Vector2(transform.position.x - size.x / 2, transform.position.x + size.x / 2);
        Vector2 yBounds = new Vector2(transform.position.y - size.y / 2, transform.position.y + size.y / 2);

        Vector2 currentMousePosition = GetMousePosition();
        if ((currentMousePosition.x > xBounds.x && currentMousePosition.x < xBounds.y) &&
            (currentMousePosition.y > yBounds.x && currentMousePosition.y < yBounds.y))
        {

            if (Input.GetMouseButtonDown(0))
            {
                gameManager.SelectBullet(this.gameObject);
                isDragging = true;
            }
            if (!Input.GetMouseButton(0))
            {
                isDragging = false;
            }
        }

        if (gameManager.CheckIsBulletSelected(this.gameObject) && isDragging)
        {
            Vector2 relativeMousePosition = new Vector2(currentMousePosition.x - targetCollider.transform.position.x,
            currentMousePosition.y - targetCollider.transform.position.y);
            
            if(relativeMousePosition.x < targetCollider.bounds.size.x/2 && relativeMousePosition.x > -targetCollider.bounds.size.x / 2
                && relativeMousePosition.y < targetCollider.bounds.size.y / 2 && relativeMousePosition.y > -targetCollider.bounds.size.y / 2)
            {
                transform.position = currentMousePosition;
            }

        }
    }
}
