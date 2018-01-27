using UnityEngine;

namespace Core.SwitchCamera
{
    public class LookAtManager : MonoBehaviour
    {
        public LookAtTable LookAtTableRef;
        public LookAtClient LookAtClientRef;
        public bool LookAtTableFirst = true;

        private void Start()
        {
            if (LookAtTableFirst)
            {
                LookAtTableRef.Disable();
                LookAtClientRef.Enable();
            }
            else
            {
                LookAtClientRef.Disable();
                LookAtTableRef.Enable();
            }
        }
    }
}