using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float enemieHitForce;
    [SerializeField] private float climbingVelocity;
    [SerializeField] private Transform groundCheck1, groundCheck2;

    private GameObject gameController;
    private Animator playerAnimator;
    private Rigidbody2D playerRb2D;
    private SpriteRenderer playerSprite;
    private float movement;
    private bool dead, leftMove, stay, attacking, jumpAttacking, grounded, getHit;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        playerAnimator = GetComponent<Animator>();
        playerRb2D = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (dead || (attacking && grounded)) return;

        Vector2 pos = transform.position;
        if (!leftMove && !stay) transform.position = new Vector2(pos.x + (moveSpeed * Time.deltaTime), pos.y);
        else if (leftMove && !stay) transform.position = new Vector2(pos.x - (moveSpeed * Time.deltaTime), pos.y);
    }
    void Update()
    {
        if (!dead)
        {
            HorizontalMovement();
            JumpMovement();
            AttackMovement();
            
        }
    }

    void HorizontalMovement()
    {
        movement = Input.GetAxisRaw("Horizontal");
        if (movement > 0.1f)
        {
            leftMove = false;
            stay = false;
            playerAnimator.SetBool("Run", true);
            playerSprite.flipX = false;
        }
        else if (movement < -0.1f)
        {
            leftMove = true;
            stay = false;
            playerAnimator.SetBool("Run", true);
            playerSprite.flipX = true;
        }
        else
        {
            stay = true;
            playerAnimator.SetBool("Run", false);
        }
    }
    void JumpMovement()
    {
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            playerRb2D.AddForce(new Vector2(0, jumpForce));
            playerAnimator.SetBool("Jump", true);
            gameController.GetComponent<SoundEffectsScript>().JumpSound();
            grounded = false;
        }

        if (Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            playerAnimator.SetBool("Jump", false);
            playerAnimator.SetBool("JumpAttack", false);
            playerAnimator.SetBool("Hit", false);
            Invoke("SetHitFalse", 2.5f);
            grounded = true;
        }
        else if (getHit)
        {
            playerAnimator.SetBool("Hit", true);
        }
        else
        {
            playerAnimator.SetBool("Jump", true);
            grounded = false;
        }
    }
    void SetHitFalse()
    {
        getHit = false;
    }
    void AttackMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameController.GetComponent<SoundEffectsScript>().AttackSound();
            playerAnimator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.S)) playerAnimator.SetBool("JumpAttack", true);

        string clipName = "";
        try { clipName = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name; }
        catch { clipName = ""; }
        
        if (clipName == "Attack")
        {
            attacking = true;
            for (int i=0;i< transform.childCount;i++)
            {
                if (transform.GetChild(i).tag == "AttackR" && !leftMove) transform.GetChild(i).gameObject.SetActive(true);
                else if (transform.GetChild(i).tag == "AttackL" && leftMove) transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            attacking = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "AttackR"|| transform.GetChild(i).tag == "AttackL") transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (clipName == "JumpAttack")
        {
            jumpAttacking = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "AttackJump") transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            jumpAttacking = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "AttackJump") transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void DeleteGemTaken(Collision2D collision)
    {
        collision.gameObject.GetComponent<Animator>().SetTrigger("gemsTaken");
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        collision.gameObject.GetComponent<Collider2D>().isTrigger = true;
        Destroy(collision.gameObject, 2);
    }
    public void PlayerDead()
    {
        dead = true;
        playerAnimator.SetTrigger("Dead");
        PlayerPrefs.SetInt("goldPlayerDead", gameController.GetComponent<UIScript>().gold/2);
        PlayerPrefs.SetFloat("posXPlayerDead", transform.position.x);
        PlayerPrefs.SetFloat("posYPlayerDead", transform.position.y);
        Invoke("RestartGame", 3);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "JumpDeadCollider") PlayerDead();

        if (collision.gameObject.tag == "BossWeapon" && collision.gameObject.transform.parent.GetComponent<BossScript>().bossAttacking && !collision.transform.parent.gameObject.GetComponent<BossScript>().bossDead)
        {
            playerRb2D.velocity = new Vector2(0, 0);
            if (transform.position.x <= collision.transform.position.x) playerRb2D.AddForce(new Vector2(-enemieHitForce / 2, enemieHitForce));
            else if (transform.position.x > collision.transform.position.x) playerRb2D.AddForce(new Vector2(enemieHitForce / 2, enemieHitForce));

            getHit = true;
            playerAnimator.SetBool("Hit", true);
            gameController.GetComponent<SoundEffectsScript>().HitSound();
            GetComponent<PlayerHealthScript>().RestarVida(2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bug" && !collision.gameObject.GetComponent<BugScript>().dead)
        {
            playerRb2D.velocity = new Vector2(0, 0);
            if (transform.position.x <= collision.transform.position.x) playerRb2D.AddForce(new Vector2(-enemieHitForce / 2, enemieHitForce));
            else if (transform.position.x > collision.transform.position.x) playerRb2D.AddForce(new Vector2(enemieHitForce / 2, enemieHitForce));

            getHit = true;
            playerAnimator.SetBool("Hit", true);
            gameController.GetComponent<SoundEffectsScript>().HitSound();
            GetComponent<PlayerHealthScript>().RestarVida(1);
        }
        else if(collision.gameObject.tag == "Bug" && jumpAttacking)
        {
            playerRb2D.AddForce(new Vector2(0, enemieHitForce*1.5f));
        }

        if (collision.gameObject.tag == "Slim" && !collision.gameObject.GetComponent<SlimScript>().dead)
        {
            playerRb2D.velocity = new Vector2(0, 0);
            if (transform.position.x <= collision.transform.position.x) playerRb2D.AddForce(new Vector2(-enemieHitForce / 2, enemieHitForce));
            else if (transform.position.x > collision.transform.position.x) playerRb2D.AddForce(new Vector2(enemieHitForce / 2, enemieHitForce));

            getHit = true;
            playerAnimator.SetBool("Hit", true);
            gameController.GetComponent<SoundEffectsScript>().HitSound();
            GetComponent<PlayerHealthScript>().RestarVida(2);
        }

        if (collision.gameObject.tag == "Boss" && !collision.gameObject.GetComponent<BossScript>().bossDead)
        {
            playerRb2D.velocity = new Vector2(0, 0);
            if (transform.position.x <= collision.transform.position.x) playerRb2D.AddForce(new Vector2(-enemieHitForce / 2, enemieHitForce));
            else if (transform.position.x > collision.transform.position.x) playerRb2D.AddForce(new Vector2(enemieHitForce / 2, enemieHitForce));

            getHit = true;
            playerAnimator.SetBool("Hit", true);
            gameController.GetComponent<SoundEffectsScript>().HitSound();
            GetComponent<PlayerHealthScript>().RestarVida(2);
        }

        if (collision.gameObject.tag == "gema0")
        {
            gameController.GetComponent<UIScript>().PlusGold(1);
            DeleteGemTaken(collision);
        }
        else if (collision.gameObject.tag == "gema1")
        {
            gameController.GetComponent<UIScript>().PlusGold(5);
            DeleteGemTaken(collision);
        }
        else if (collision.gameObject.tag == "gema2")
        {
            gameController.GetComponent<UIScript>().PlusGold(10);
            DeleteGemTaken(collision);
        }
        else if (collision.gameObject.tag == "gema3")
        {
            gameController.GetComponent<UIScript>().PlusGold(20);
            DeleteGemTaken(collision);
        }
        else if (collision.gameObject.tag == "gema4")
        {
            gameController.GetComponent<UIScript>().PlusGold(50);
            DeleteGemTaken(collision);
        }
        else if (collision.gameObject.tag == "gema5")
        {
            gameController.GetComponent<UIScript>().PlusGold(200);
            DeleteGemTaken(collision);
        }
    }
}
