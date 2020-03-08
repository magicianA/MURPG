using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    namespace Linker
    {
        [AddComponentMenu("Linker/InputSystem/InputGetter")]
        public class InputGetter : MonoBehaviour
        {
            public InputSystem.InputKey key;

            private void Update()
            {
                if (InputSystem.GetKey(key)) keyOutput?.Invoke();
                if (InputSystem.GetKeyDown(key)) keyDownOutput?.Invoke();
                if (InputSystem.GetKeyUp(key)) keyUpOutput?.Invoke();
            }

            //Output
            public SimpleEvent keyOutput;
            public SimpleEvent keyDownOutput;
            public SimpleEvent keyUpOutput;
        }
    }
}
