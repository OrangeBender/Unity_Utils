using UnityEngine;

public class DisplayCurrentDieValue : MonoBehaviour
{
    public LayerMask dieValueColliderLayer;
    public Rigidbody rb;

    private int currentValue;
    private bool rollComplete = false;

    void Update () {

        if (rb.IsSleeping() && !rollComplete)
        {
            rollComplete = true;
            Debug.Log("Dice stopped rolling, result is: " + currentValue.ToString());
        }
        else if(!rb.IsSleeping())
        {
            rollComplete = false;
        }

        RaycastHit hit;

        if(Physics.Raycast(transform.position,Vector3.up,out hit,Mathf.Infinity,dieValueColliderLayer)){

            // Reading the value of the collider on the die top face
            currentValue = hit.collider.GetComponent<DieValue>().getValue();
        }
    }   
}