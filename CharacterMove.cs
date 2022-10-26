using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private Animator animator;
    private bool died = false;
    public static int index = 0;
    public static GameObject[] charactersAded = new GameObject[31];
    public static int score;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!died)
        {
            if (other.CompareTag("Waited") && this.CompareTag("Started"))
            {
                charactersAded[index] = other.gameObject;
                index++;
                DrawShape.numberOfCharacters++;
                Animator anim = other.GetComponentInChildren<Animator>();
                anim.enabled = true;
                other.tag = "Started";
            }

            if (other.CompareTag("Danger"))
            {
                this.tag = "Died";
                died = true;
                animator.SetTrigger("Die");
                Invoke(nameof(SetEnabled), 1.5f);
            }
        }
    }

    private void SetEnabled()
    {
        animator.enabled = false;
    }
}
