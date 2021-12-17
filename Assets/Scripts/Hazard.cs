using Cinemachine;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    Vector3 rotation;

    public ParticleSystem breakingEffect;

    private CinemachineImpulseSource cinemachineImpulseSource;

    private CubePlayer player;

    private void Start()
    {
        var xRotation = Random.Range(90f, 180f);
        rotation = new Vector3(-xRotation, 0);
        player = FindObjectOfType<CubePlayer>();

        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        transform.Rotate (rotation * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Hazard"))
        {
            Destroy (gameObject);

            if (player != null)
            {
                Instantiate(breakingEffect,
                transform.position,
                Quaternion.identity);

                var distance =
                    Vector3
                        .Distance(transform.position,
                        player.transform.position);

                var force = 1f / distance;

                cinemachineImpulseSource.GenerateImpulse (force);
            }
        }
    }
}
