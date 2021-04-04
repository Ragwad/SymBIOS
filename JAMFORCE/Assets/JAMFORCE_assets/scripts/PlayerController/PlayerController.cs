using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerManager playerManager;
    [HideInInspector] public Animator animator;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        animator = GetComponent<Animator>();
    }
}