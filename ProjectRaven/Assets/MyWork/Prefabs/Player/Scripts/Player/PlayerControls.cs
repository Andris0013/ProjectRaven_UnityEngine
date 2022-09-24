using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] new Transform camera;
    [SerializeField] Transform groundcheck;
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask groundMask;
    [SerializeField] GameObject GameOver;
    [SerializeField] GameObject[] enemies;
    [Space]

    [SerializeField] float rotationSpeedVelocity;
    [SerializeField] float rotationSpeed = 0.1f;
    [SerializeField] float speed = 6f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] float damage = 100f;
    [SerializeField] float maxHealth = 1f;
    float currentHealth;
    [Space]

    bool isGrounded;
    [Space]

    Vector3 velocity;




    void Start()
    {
        this.enabled = true;
        animator = GetComponentInChildren<Animator>();
        controller = GetComponentInChildren<CharacterController>();
        currentHealth = maxHealth;
        GameOver.SetActive(false);

    }

    void Update()
    {
        Gravity();

        Attack();

        Vector3 direction = GetDirection();

        AnimateRun(direction);

        Move(direction);

    }



    // Support
    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();

    }

    void Die()
    {
        foreach (GameObject enemy in enemies)
            enemy.SetActive(false);

        animator.SetBool("IsDead", true);
        
        GameOver.SetActive(true);
        camera.GetComponentInChildren<Cinemachine.CinemachineBrain>(enabled = false);

        this.enabled = false;
    }

    Vector3 GetDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        return direction;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("Attack");

        Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemy)
        {
            enemy.GetComponentInChildren<EnemyMovement>().TakeDamage(damage);
        }

    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void AnimateRun(Vector3 direction)
    {
        if (direction != Vector3.zero)
            animator.SetBool("IsRunning", true);
        else
            animator.SetBool("IsRunning", false);
    }

    void Move(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeedVelocity, rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

    } 
}


// Comments
/*

Wanted all damage to be instant kill but there is the option layed out to implement more health, less damaga and take hit animations.

Also tried to make the enemy replacable for graphics if I want to implement actual enemies not just spheres in the futur

Decided to try to learn new things that make movement and camera movement easier. (Main reason is that my character was always moving towards global forward even when I looked the oder way. This seemed to be the easiest solution.

*/



