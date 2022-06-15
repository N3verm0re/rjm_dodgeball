using UnityEngine;
using System.Collections;

namespace Com.BlackRavenGames.Dodgeball
{
    public class PlayerAnimatorManager : MonoBehaviour
    {
        #region Private Fields
        private Animator animator;

        [SerializeField]
        private float directionDampTime = 0.25f;
        #endregion

        #region MonoBehaviour Callbacks
        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        void Update()
        {
            if (!animator)
            {
                return;
            }

            //Debugging
            /*
            if (Input.GetAxis("Horizontal") != 0)
            {
                Debug.Log("Registering Horizontal input...");
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                Debug.Log("Registering Vertical input...");
            }
            */

            // deal with Jumping
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // only allow jumping if we are running.
            if (stateInfo.IsName("Base Layer.Run"))
            {
                // When using trigger parameter
                if (Input.GetButtonDown("Fire2"))
                {
                    animator.SetTrigger("Jump");
                }
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            animator.SetFloat("Speed", Mathf.Abs(h) + Mathf.Abs(v));
            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
        }
        #endregion
    }
}