using UnityEngine;

public class FSMExample : MonoBehaviour
{
    public string AIState = "Idle";
    public float aiSenseRadius;
    public Vector3 targetPosition;
    public float health;
    public float healthCutoff;
    public float moveSpeed;
    public float healingRate;
    public float maxHealth;
    public float hearingDistance;
    public float fieldOfView = 90f;
    public float visionMaxDistance = 40f;

    // Update is called once per frame
    void Update()
    {
        if (AIState == "Idle")
        {
            //Do the state behavior
            Idle();
            //Check for transitions
            if(Vector3.Distance(transform.position, targetPosition) < aiSenseRadius)
            {
                ChangeState("Seek");
            }
        }
        else if (AIState == "Seek")
        {
            //Do the state behavior
            Seek();
            //Check for transitions
            if(health < healthCutoff)
            {
                ChangeState("Rest");
            }
            else if(Vector3.Distance(transform.position, targetPosition) >= aiSenseRadius)
            {
                ChangeState("Idle");
            }
        }
        else if(AIState == "Rest")
        {
            //Do the state behavior
            Rest();
            //Check for transitions
            if(health >= healthCutoff)
            {
                ChangeState("Idle");
            }
        }
        else
        {
            Debug.LogWarning("AIState not found: " + AIState);
        }
    }

    void Idle()
    {
        //Do Nothing
    }

    void Seek()
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        transform.position += vectorToTarget.normalized * moveSpeed * Time.deltaTime;
    }

    void Rest()
    {
        //TO DO: Write the Rest State
        health += healingRate * Time.deltaTime;
        health = Mathf.Min(health, maxHealth);
    }

    public void ChangeState(string newState)
    {
        AIState = newState;
    }

    private bool CanHear(GameObject target)
    {
        //Get the target's noise maker
        NoiseMaker targetNoiseMaker = target.GetComponent<NoiseMaker>();

        //If they don't have a nise maker, we can't hear them
        if(targetNoiseMaker == null) { return false; }

        //If the distance between us and the target is less than the sum of the noise distance and hearing distance, we can hear it
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if((targetNoiseMaker.volumeDistance + hearingDistance) > distanceToTarget) { return true; }

        return false;
    }

    private bool CanSee(GameObject target)
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;

        float angleToTarget = Vector3.Angle(vectorToTarget, transform.up);

        //Check if the target is within the field of view.
        if(angleToTarget < fieldOfView)
        {
            //Use a raycast to see if there are obstuctions between us and the target 
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, vectorToTarget, visionMaxDistance);

            if(hitInfo.collider.gameObject == target)
            {
                return true;
            }

        }

        return false;
    }
}
