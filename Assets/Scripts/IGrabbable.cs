using UnityEngine;

namespace Controller
{
    ///Author: Antonio Bottelier
    
    public abstract class Grabbable : MonoBehaviour
    {
        public bool grabbed = false;
        public bool multigrab = false; // needed for the steering wheel.
        public WandController grabbedBy = null;
        
        public abstract void StartGrab(WandController controller);
        public abstract void StopGrab(WandController controller);
    }
}