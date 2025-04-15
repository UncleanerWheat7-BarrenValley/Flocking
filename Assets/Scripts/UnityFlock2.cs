//using System.Collections;
//using UnityEngine;

//public class UnityFlock2 : MonoBehaviour
//{
//    public float minSpeed = 20;
//    public float turnSpeed = 20;
//    public float randomFreq = 20;
//    public float randomForce = 20;

//    public float toOriginForce = 50;
//    public float toOriginRange = 100;

//    public float gravity = 2;

//    public float avoidanceRadius = 50;
//    public float avoidanceForce = 20;

//    public float followVelocity = 4;
//    public float followRadius = 40;

//    private Transform origin;

//    Vector3 velocity;
//    Vector3 normalizedVelocity;
//    Vector3 randomPush;
//    Vector3 originPush;
//    private Transform[] objects;
//    UnityFlock[] otherFlocks;
//    Transform transformComponent;
//    float randomFreqInterval;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        randomFreqInterval = 1 / randomFreq;
//        origin = transform.parent;
//        transformComponent = transform;
//        Component[] tempFlocks = null;

//        if (transform.parent)
//        {
//            tempFlocks = transform.parent.GetComponentsInChildren<UnityFlock>();
//        }

//        objects = new Transform[tempFlocks.Length];
//        otherFlocks = new UnityFlock[tempFlocks.Length];

//        for (int i = 0; i < tempFlocks.Length; i++)
//        {
//            objects[i] = tempFlocks[i].transform;
//            otherFlocks[i] = (UnityFlock)tempFlocks[i];
//        }

//        transform.parent = null;

//        StartCoroutine(UpdateRandom());
//    }

//    IEnumerator UpdateRandom()
//    {
//        while (true)
//        {
//            randomPush = Random.insideUnitSphere * randomForce;
//            yield return new WaitForSeconds(randomFreqInterval + Random.Range(-randomFreqInterval / 2, -randomFreqInterval / 2));
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        float speed = velocity.magnitude;
//        Vector3 avgVelocity = Vector3.zero;
//        Vector3 avgPosition = Vector3.zero;
//        int count = 0;

//        Vector3 myPosition = transformComponent.position;
//        Vector3 forceV;
//        Vector3 toAvg;

//        for (int i = 0; i < objects.Length; i++)
//        {
//            Transform boidTransform = objects[i];
//            if (boidTransform != transformComponent)
//            {
//                Vector3 otherPosition = boidTransform.position;

//                avgVelocity += otherPosition;
//                count++;

//                forceV = myPosition - otherPosition;

//                float directionMagnitude = forceV.magnitude;
//                float forceMagnitude = 0;

//                if (directionMagnitude < followRadius)
//                {
//                    if (directionMagnitude < avoidanceRadius)
//                    {
//                        forceMagnitude = 1 - (directionMagnitude / avoidanceRadius);

//                        if (directionMagnitude > 0)
//                        {
//                            avgVelocity += (forceV / directionMagnitude) * forceMagnitude * avoidanceForce;
//                        }

//                        forceMagnitude = directionMagnitude / followRadius;
//                        UnityFlock tempOtherBoid = otherFlocks[i];
//                        avgVelocity += followVelocity * forceMagnitude * tempOtherBoid.normalizedVelocity;
//                    }

//                }
//            }

//            if (count > 0)
//            {
//                avgVelocity /= count;
//                toAvg = (avgPosition / count) - myPosition;
//            }
//            else
//            {
//                toAvg = Vector3.zero;
//            }

//            forceV = origin.position - myPosition;
//            float leaderDirectionMagnitude = forceV.magnitude;
//            float leaderForceMagnitude = leaderDirectionMagnitude / toOriginRange;

//            if (leaderDirectionMagnitude > 0)
//            {
//                originPush = leaderDirectionMagnitude * toOriginForce * (forceV / leaderDirectionMagnitude);
//            }

//            if (speed < minSpeed && speed > 0)
//            {
//                velocity = (velocity / speed) * minSpeed;
//            }

//            Vector3 wantedVel = velocity;

//            wantedVel -= wantedVel * Time.deltaTime;
//            wantedVel += randomPush * Time.deltaTime;
//            wantedVel += originPush * Time.deltaTime;
//            wantedVel += avgVelocity * Time.deltaTime;
//            wantedVel += gravity * Time.deltaTime * toAvg.normalized;

//            velocity = Vector3.RotateTowards(velocity, wantedVel, turnSpeed * Time.deltaTime, 100);
//            transformComponent.rotation = Quaternion.LookRotation(velocity);

//            transformComponent.Translate(velocity * Time.deltaTime, Space.World);

//            normalizedVelocity = velocity.normalized;

//        }
//    }
//}
