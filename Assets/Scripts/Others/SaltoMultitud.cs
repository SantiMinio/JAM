using UnityEngine;
using System.Collections;

public class RandomJumper : MonoBehaviour
{
    public float jumpForce = 5f;
    public float minJumpInterval = 1f;
    public float maxJumpInterval = 3f;
    public LayerMask jumpMask;
    private Rigidbody rb;
    [SerializeField]private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(JumpRoutine());
    }

    void OnCollisionEnter(Collision collision)
    {
        // Considera grounded si toca el suelo
        if (collision.gameObject.layer==LayerMask.NameToLayer("Default"))
            isGrounded = true;
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minJumpInterval, maxJumpInterval);
            yield return new WaitForSeconds(waitTime);

            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
    }
}
