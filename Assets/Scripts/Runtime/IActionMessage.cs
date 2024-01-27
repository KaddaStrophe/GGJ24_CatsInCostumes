using UnityEngine.InputSystem;

namespace CatsInCostumes {
    interface IActionMessage {
        void OnSetAction(InputAction action);
    }
}
