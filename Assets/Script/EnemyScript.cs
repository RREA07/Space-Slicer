using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform playerPosition;
    private Rigidbody2D en;
    private Vector2 movement;
    public float movementSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        en = this.GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindWithTag("Player").GetComponent<RectTransform>();
        //player = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = playerPosition.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        en.rotation = angle;
        direction.Normalize();
        movement = direction;
        /*
        Debug.Log(direction.y + "Y diff"); ////
        Debug.Log(direction.x + "X diff"); ////
        if (direction.x >= 300 || direction.y >= 300)
        {
            Destroy(this.gameObject);
        }
        */
    }
    private void FixedUpdate()
    {
        moveEnemy(movement);
    }
    void moveEnemy(Vector2 direction)
    {
        //if (player.iTimer == 0)
        //{
        en.MovePosition((Vector2)transform.position + (direction * movementSpeed * Time.deltaTime));
        //}
        //else if (player.iTimer > 0)
        //{
        //en.MovePosition((Vector2)transform.position + (-direction * movementSpeed * Time.deltaTime));
        //}
    }

}
