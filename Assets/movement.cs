using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class movement : Agent
{
    public Transform cube;
    private int count;
     Rigidbody rb;
 
    public override void OnEpisodeBegin()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        count = 1200;
        transform.rotation = new Quaternion(0,0,0,0);
        transform.localPosition = new Vector3(Random.Range(-9f, 9f), 0, Random.Range(-9f, 9f));
        cube.transform.localPosition = new Vector3(Random.Range(-9f, 9f), 0, Random.Range(-9f, 9f));
     

    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name.Equals("Food"))
        {
            SetReward(1f);
            EndEpisode();
        }
         if(collision.gameObject.tag.Equals("Walls"))
        {
            SetReward(-2f);
            EndEpisode();
        }
        

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(cube.localPosition);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
      transform.Translate(new Vector3(vectorAction[0] * Time.deltaTime *7, 0, vectorAction[1] * Time.deltaTime * 7));
        count--;
        if (count <= 0)
        {
            SetReward(-1f);
            EndEpisode();
        }
       
    }
}
