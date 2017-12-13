

using UnityEngine;

namespace Controller
{
    ///Author: Antonio Bottelier

    public class SteeringWheel : Grabbable
    {
        public GameObject[] hands;
        
        public override void StartGrab(WandController controller)
        {
            controller.HideControllerModel();
            int i = controller.isLeftHand ? 0 : 1;
            hands[i].SetActive(true);
        }

        public override void StopGrab(WandController controller)
        {
            controller.HideControllerModel(false);
            int i = controller.isLeftHand ? 0 : 1;
            hands[i].SetActive(false);
        }
    }
}