using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace new_game
{
    public class TestingCube : MonoBehaviour
    {
        public float Speed;

        void Update()
        {
            if (VirtualInputManager.Instance.MoveRight && VirtualInputManager.Instance.MoveLeft)
            {
                return;
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (VirtualInputManager.Instance.MoveLeft)
            {
                this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            }
        }
    }

}
