using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts
{
    [RequireComponent(typeof (RealPlatformerCharacter2D))]
    public class RealPlatformer2DUserControl : MonoBehaviour
    {
        private RealPlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_buttonJump;


        private void Awake()
        {
            m_Character = GetComponent<RealPlatformerCharacter2D>();
        }


        private void Update()
        {
            //if (!m_Jump)
            //{
            //    Read the jump input in Update so button presses aren't missed.


            //}
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            
            //Below jump is for joystick (currently commented out)
            //m_buttonJump = Input.GetKeyDown(KeyCode.JoystickButton1);

            m_buttonJump = Input.GetKeyDown(KeyCode.Space);

            //Below is unity editor jump code. Currently commented out.
//#if UNITY_EDITOR
//            m_buttonJump |= Input.GetKeyDown(KeyCode.Space);
//#endif
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            m_Character.Move(h, crouch, m_buttonJump);
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            
            
            // Pass all parameters to the character control script.
            //m_Character.Move(h, crouch, m_Jump, m_buttonJump);
            m_Jump = false;
        }
    }
}
