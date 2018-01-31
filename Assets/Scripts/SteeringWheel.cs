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
            controllers[i].SetLastPosition(controllers[i].Controller.transform.position);

            Vector3 controllerpos = controllers[i].Controller.transform.position;
            Vector3 middlePoint = transform.position;
            Vector3 topPoint = transform.position + -transform.forward * radius;
            controllers[i].InitialAngle = GetAngle(controllerpos, topPoint, middlePoint);
            
            hands[i].transform.position = transform.position + 
                     GetCircleEdgeCollision(controller.transform.position);
            hands[i].transform.rotation = Quaternion.LookRotation(hands[i].transform.position - transform.position, hands[i].transform.up);
        }

        float GetAngle(Vector3 controllerpos, Vector3 topPoint, Vector3 middlePoint)
        {
            float opposite = (middlePoint - topPoint).magnitude;
            float neighbor = (middlePoint - controllerpos).magnitude;
            return Mathf.Atan2(opposite, neighbor) * Mathf.Rad2Deg;
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

            switch (controllercount)
            {
                case 1:
                    var controller = controllers.First(x => x.Controller != null);
                    
                    Vector3 controllerpos = controller.Controller.transform.position;
                    Vector3 middlePoint = transform.position;
                    Vector3 topPoint = transform.position + -transform.forward * radius;
                    float angle = GetAngle(controllerpos, topPoint, middlePoint);
                    float angledifference = controller.InitialAngle - angle;
                    controller.InitialAngle = angle;

                    var rotation = transform.localEulerAngles;
                    rotation.y += angledifference;
                    transform.localEulerAngles = rotation;
                    break;
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
        private float initialAngle;

        public Vector3 Position
        {
            get { return controller.transform.position; }
        }

        public float InitialAngle
        {
            get { return initialAngle; }
            set { initialAngle = value; }
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
            initialAngle = 0;
        }
    }
}