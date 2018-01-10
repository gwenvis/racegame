using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Controller
{
    ///Author: Antonio Bottelier

    public class SteeringWheel : Grabbable
    {
        public GameObject[] hands;
        private GrabbingController[] controllers = new GrabbingController[2]; // 0 is left, 1 is right

        public int ControllersGrabbed
        {
            get { return controllers.Count(x => x.Controller != null); }
        }
        
        private float radius = 0.3f;
        
        public override void StartGrab(WandController controller)
        {
            controller.HideControllerModel();
            int i = controller.isLeftHand ? 0 : 1;
            hands[i].SetActive(true);
            controllers[i].Controller = controller;
            Vector3 rotation;
            hands[i].transform.position = GetCircleEdgeCollision(controller.transform.position, out rotation);
            hands[i].transform.eulerAngles = rotation;
        }

        void Start()
        {
            hands.ForEach(x => x.SetActive(false));
        }

        Vector3 GetCircleEdgeCollision(Vector3 pos, out Vector3 rotation)
        {
            // map track pos to local space
            var trackp = transform.InverseTransformPoint(transform.position);
            // also map current pos to local space.
            var position = transform.InverseTransformPoint(pos);
		
            var a = trackp.x - position.x;
            var b = trackp.z - position.z;
            var mag = Mathf.Sqrt(a * a + b * b);

            // the formula is 
            // C(x,y) = A(x,y) + r * B(x,y) - A(x,y) / √ (Bx - Ax)^2 + (By - Ay)^2
            // I use z instead of y, though.
            float cx = position.x + radius * a / mag;
            float cz = position.z + radius * b / mag;
            
            // get the needed (z?) angle for the rotation to be correct.

            return transform.TransformVector(new Vector3(cx, 0, cz));
        }

        private void Update()
        {
            int controllercount = ControllersGrabbed;
            
            
            foreach (var controller in controllers)
            {
                if (controller.Controller != null)
                    controller.SetLastPosition(controller.Position);
            }
        }

        public override void StopGrab(WandController controller)
        {
            controller.HideControllerModel(false);
            int i = controller.isLeftHand ? 0 : 1;
            hands[i].SetActive(false);
            controllers[i].Controller = null;
        }
    }

    struct GrabbingController
    {
        private WandController controller;
        private Vector3 lastPosition;

        public Vector3 Position
        {
            get { return controller.transform.position; }
        }

        public WandController Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        public Vector3 LastPosition
        {
            get { return lastPosition; }
            set { lastPosition = value; }
        }

        public void SetLastPosition(Vector3 pos)
        {
            lastPosition = pos;
        }

        public GrabbingController(WandController cont)
        {
            lastPosition = cont.transform.position;
            controller = cont;
        }
    }
}