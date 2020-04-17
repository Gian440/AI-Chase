using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    //different variables to alter properties of AI and player
    public float Health = 0;

    public float Speed;
    public Pathfinding playerController;
    private Rigidbody2D _PlayerRigidBody;

    private float _MoveHorizontal;
    private float _MoveVertical;

    #endregion
    void Start()
    #region Rigidbody
    {
        //creates a rigidbody and allows the use of Unity's physics engine
        _PlayerRigidBody = GetComponent<Rigidbody2D>();
    }
    #endregion
   
    public void TakeDamage(float damage)
    #region Damage 
    {
        Health -= damage;
    }
    #endregion

    private void Update()
    {
        //Makes the space bar take 1 point of health from the AI
        if((Vector2.Distance(playerController.playerObject.transform.position, playerController.AiSprite.transform.position) < playerController.ChasePlayerDistance) && Input.GetKeyDown(KeyCode.Space))
        {
            
            playerController.AiHealth -= 1;


        }
    }
    void FixedUpdate()
     #region Movement
    {
        //allows the player to move if health is above 0, otherwise they freeze
        if (Health > 0)
        {
            _MoveHorizontal = Input.GetAxis("Horizontal");
            _MoveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(_MoveHorizontal, _MoveVertical);

            _PlayerRigidBody.AddForce(movement * Speed);
        }
        #endregion
    }
}
