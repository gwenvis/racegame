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
        
        private float radius = 1f / 2; //diameter / 2
        
        public override void StartGrab(WandController controller)
        {
            controller.HideControllerModel();
            int i = controller.isLeftHand ? 0 : 1;
            hands[i].SetActive(true);
            controllers[i].Controller = controller;
            hands[i].transform.position = transform.position + 
                     GetCircleEdgeCollision(controller.transform.position);
            hands[i].transform.rotation = Quaternion.LookRotation(hands[i].transform.position - transform.position, hands[i].transform.up);
        }

        void Start()
        {
            hands.ForEach(x => x.SetActive(false));
        }

        Vector3 GetCircleEdgeCollision(Vector3 track)
        {
            // map track pos to local space
            var controllerpos = transform.InverseTransformPoint(track);
            // also map current pos to local space.
            var localCirclePos = transform.InverseTransformPoint(transform.position);
            controllerpos = controllerpos - localCirclePos;
		
            var a = controllerpos.x - localCirclePos.x;
            var b = controllerpos.z - localCirclePos.z;
            var mag = Mathf.Sqrt(a * a + b * b);
		
            // the formula is 
            // C(x,y) = A(x,y) + r * B(x,y) - A(x,y) / √ (Bx - Ax)^2 + (By - Ay)^2
            float cx = localCirclePos.x + radius * a / mag;
            float cz = localCirclePos.z + radius * b / mag;

            Vector3 returnvec =  transform.TransformVector(new Vector3(cx, 0, cz));
            return returnvec;
        }

        private void Update()
        {
            int controllercount = ControllersGrabbed;
            
            foreach (var controller in controllers)
            {
                if (controller.Controller != null)
                {
                    var vector = controller.LastPosition - controller.Position;
                    
                    controller.SetLastPosition(controller.Position);
                }
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