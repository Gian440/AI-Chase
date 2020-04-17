using UnityEngine;


public class Pathfinding : MonoBehaviour
{
    #region Variables
    public float Speed = 5.0f;
    public GameObject[] Waypoint;
    public GameObject AiSprite;
    public float MinDistanceToWaypoint;
    public GameObject playerObject;
    public float ChasePlayerDistance;
    public float DamagePlayerDistance;
    private int CurrentWaypoint = 0;
    public GameObject YouLose;
    public PlayerController playerController;
    float AttackCooldown = 1f;
    public float DealDamage;
    float TimeOfAttack = float.MinValue;
    public float AiHealth = 3;
    public GameObject aiBase;
    public GameObject aiBaseTrigger;
    #endregion



    void Update()
    #region Victory Lap
    {
        //increases the ai speed when the player is dead to simulate a victory lap
        if (playerController.Health <= 0)
        {
            Speed = 50;
            Patrol();

        }
        #endregion
        #region AI Movement
        //ai gors to base when health is low
        else if (playerController.Health > 0 && AiHealth <= 1)
        {
            MoveAI(aiBase.transform.position, Speed);
        }
        //creates a cooldown between attacks
        else if (Vector2.Distance(playerObject.transform.position, AiSprite.transform.position) < DamagePlayerDistance)
        {
            if (Time.time > TimeOfAttack + AttackCooldown)
            {
                playerController.TakeDamage(DealDamage);
                TimeOfAttack = Time.time;
            }
        }
        //makes the ai chase player in a certain range instead of following waypoints
        else if (Vector2.Distance(playerObject.transform.position, AiSprite.transform.position) < ChasePlayerDistance)
        {
            MoveAI(playerObject.transform.position, Speed);
        }
        //if no player is present then the ai will patrol using waypoints
        else
        {
            Patrol();
        }
    }

    public void OnTriggerEnter2D(Collider2D aiBaseTrigger)
    {
        AiHealth = 4;
    }
    #endregion
    private void MoveAI(Vector2 targetPosition, float speed)
    #region MoveAI
    {
        
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    }
    #endregion
    private void Patrol()
    #region Patrol
    {

        //resets the waypoint order back to 0 to allow an endless loop for the ai to follow
        if (CurrentWaypoint >= Waypoint.Length)
        {
            CurrentWaypoint = 0;
        }

        MoveAI(Waypoint[CurrentWaypoint].transform.position, Speed);

        if (Vector2.Distance(transform.position, Waypoint[CurrentWaypoint].transform.position) <= 0.01f)
        {
            CurrentWaypoint++;
        }
    }
    #endregion
}