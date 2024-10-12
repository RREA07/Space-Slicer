using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public LogicManager logic;
    public EnemySpawner enemySpawner;
    public Boolean dash = false;
    public float dashTimer = 0;
    public double dashCoolDown = -0.000001;
    private bool notChecked = false;
    public float iTimer = 0;
    public float speed = 10f;
    public bool isDead;
    public bool keyDown;
    private SoundFXManager soundFXManager;
    [SerializeField] private ParticleSystem dashTrail;
    private ParticleSystem dashTrailInst;
    [SerializeField] private ParticleSystem explosion;
    private ParticleSystem explosionInst;
    private Vector3 starting;

    // Start is called before the first frame update
    void Start()
    {
        starting = transform.position;
        soundFXManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundFXManager>();
        myRigidbody = GetComponent<Rigidbody2D>();
        logic.gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.playerHealth <= 0 && !isDead)
        {
            isDead = true;
            logic.gameOver();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !isDead && dashCoolDown <= 0)
        {
            dashTimer = 1.0f;
            dashCoolDown = 3.0f;
            dashPlayer(transform.right);
            notChecked = true;
        }

        if (dashCoolDown <= 0 && notChecked)
        {
            soundFXManager.playSFX(soundFXManager.dashReady);
            notChecked = false;
        }

        if (transform.position.x >= 250 || transform.position.y >= 250)
        {
            transform.position = starting;
        }
        else if (transform.position.x <= -250 || transform.position.y <= -250)
        {
            transform.position = starting;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0) && myRigidbody.velocity.magnitude < 10 && !isDead)
        {
            movePlayer(transform.right);
        }

        if (dashTimer > 0)
        {
            dash = true;
            dashTimer -= Time.fixedDeltaTime;
        }
        else
        {
            dash = false;
            dashTimer = 0;
        }

        if (dashCoolDown > 0)
        {
            dashCoolDown -= Time.fixedDeltaTime;
        }

        if (iTimer > 0)
        {
            iTimer -= Time.fixedDeltaTime;
        }
    }

    private void movePlayer(Vector2 direction)
    {
        keyDown = true;
        myRigidbody.AddForce(direction * speed);
    }

    private void dashPlayer(Vector2 direction)
    {
        soundFXManager.playSFX(soundFXManager.dash);
        spawnDashTrail();
        myRigidbody.velocity = direction * (speed * 2);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (iTimer <= 0 && !isDead)
        {
            if (dash)
            {
                logic.scoreCount();
            }
            else
            {
                iTimer = 1;
                logic.healthCount();
            }
            soundFXManager.playSFX(soundFXManager.enemyExplode);
            GameObject enemy = collision.gameObject;
            Destroy(enemy);
            enemySpawner.maxTotalEnemy--;
            explosionInst = Instantiate(explosion, enemy.transform.position, Quaternion.identity);
            // if itimer > 0 ignore, in fixed update update timer. itimer = invincibility frame
        }
    }

    private void spawnDashTrail()
    {
        dashTrailInst = Instantiate(dashTrail, transform.position, Quaternion.identity);
    }
}
