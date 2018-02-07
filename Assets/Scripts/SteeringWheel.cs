using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Controller
{
    ///Author: Antonio Bottelier

    public class SteeringWheel : Grabbable
    {
        public float Rotation { get; private set; }

        public GameObject[] hands;
        private GrabbingController[] controllers = new GrabbingController[2]; // 0 is left, 1 is right

        public int ControllersGrabbed
        {
            get { return controllers.Count(x => x.Controller != null); }
        }

        private float radius = 1f / 2; //diameter / 2
        Vector3 up;
        Vector3 oldpos;

        void Start()
        {
            oldpos = transform.position;
            up = transform.forward;
            hands.ForEach(x => x.SetActive(false));
            for (int i = 0; i < controllers.Length; i++)
            {
                controllers[i] = new GrabbingController();
            }
        }

        public override void StartGrab(WandController controller)
        {
            
            controller.HideControllerModel();
            int i = controller.isLeftHand ? 0 : 1;
            hands[i].SetActive(true);
            
            controllers[i].Controller = controller;
            controllers[i].SetLastPosition((controllers[i].Position));
            
            hands[i].transform.position = transform.position + 
                     GetCircleEdgeCollision(controller.transform.position);
            hands[i].transform.rotation = Quaternion.LookRotation(hands[i].transform.position - transform.position, hands[i].transform.up);
        }

        float GetAngle(Vector3 from, Vector3 to)
        {
            Vector3 O = to - from;
            Vector3 A = transform.position - from;
            return Mathf.Atan2(Mathf.Abs(O.magnitude), Mathf.Abs(A.magnitude) ) * Mathf.Rad2Deg;
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
        public float speeee = 4;
        private void Update()
        {
            int controllercount = ControllersGrabbed;
            GrabbingController controller;
            Vector3 from;
            Vector3 to;
            float angle;
            float sign;
            GameObject hand;

            switch (controllercount)
            {
                case 1:
                    controller = controllers.First(x => x.Controller != null);

                    Vector3 controllerpos = controller.Position;
                    Vector3 lastPos = transform.TransformVector(controller.LastPosition);
                    to = GetCircleEdgeCollision(controllerpos);
                    from = GetCircleEdgeCollision(lastPos);
                    angle = GetAngle(from, to);
                    sign = Mathf.Sign(from.x * -to.y + from.y * to.x);

                    if (transform.eulerAngles.y < 0) angle = -angle;

                    transform.Rotate(0, -angle * sign * speeee, 0, Space.Self);

                    hand = controller.Controller.isLeftHand ? hands[0] : hands[1];

                    hand.transform.position = transform.position +
                     GetCircleEdgeCollision(controller.Controller.transform.position);
                    hand.transform.rotation = Quaternion.LookRotation(hand.transform.position - transform.position, hand.transform.up);

                    controller.LastPosition = transform.InverseTransformVector(controllerpos);

                    Rotation += angle * sign * speeee / 360;

                    break;
                case 2:
                    for(int i = 0; i < controllers.Length; i++)
                    {
                        controller = controllers[i];

                        to = GetCircleEdgeCollision(controller.Controller.transform.position);
                        from = GetCircleEdgeCollision(controller.LastPosition);
                        angle = GetAngle(from, to);
                        sign = Mathf.Sign(from.x * -to.y + from.y * to.x);

                        transform.Rotate(0, -angle * sign * (speeee / 2), 0, Space.Self);

                        hand = controller.Controller.isLeftHand ? hands[0] : hands[1];

                        hand.transform.position = transform.position +
                         GetCircleEdgeCollision(controller.Controller.transform.position);
                        hand.transform.rotation = Quaternion.LookRotation(hand.transform.position - transform.position, hand.transform.up);

                        Rotation += angle * sign * (speeee / 2) / 360;

                        controller.LastPosition = controller.Position;
                    }
                    break;
            }

            oldpos = transform.position;
        }

        public override void StopGrab(WandController controller)
        {
            controller.HideControllerModel(false);
            int i = controller.isLeftHand ? 0 : 1;
            hands[i].SetActive(false);
            controllers[i].Controller = null;
        }
    }

    class GrabbingController
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

        public GrabbingController()
        {
            lastPosition = Vector3.zero;
            controller = null;
            initialAngle = 0;
        }
    }
}