using UnityEngine;

namespace Controller
{
    ///Author: Antonio Bottelier

    public abstract class Grabbable : MonoBehaviour
    {
        public bool Grabbed { get { return grabbedBy != null || grabbedBySecond != null; } } 
        public bool multigrab = false; // needed for the steering wheel.
        public WandController grabbedBy = null;
        public WandController grabbedBySecond = null;

        public void _INTERNAL_StartGrab(WandController controller)
        {
            StartGrab(controller);
            if (multigrab && grabbedBySecond == null)
                grabbedBySecond = controller;
            else
                grabbedBy = controller;
        }

        public void _INTERNAL_StopGrab(WandController controller)
        {
            StopGrab(controller);
            if (multigrab && grabbedBySecond != null && grabbedBySecond == controller)
                grabbedBySecond = null;
            else
                grabbedBy = null;
        }

        public abstract void StartGrab(WandController controller);
        public abstract void StopGrab(WandController controller);
    }
}