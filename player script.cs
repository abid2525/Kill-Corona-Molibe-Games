using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

  //declare GameObjects and create isShooting boolean.
  private GameObject gun;
  private GameObject spawnPoint;
  private bool isShooting;

  // Use this for initialization
  void Start () {

    //only needed for IOS
    Application.targetFrameRate = 60;

    //create references to gun and bullet spawnPoint objects
    gun = gameObject.transform.GetChild (0).gameObject;
    spawnPoint = gun.transform.GetChild (0).gameObject;

    //set isShooting bool to default of false
    isShooting = false;
  }

  //Shoot function is IEnumerator so we can delay for seconds
  IEnumerator Shoot() {
    //set is shooting to true so we can't shoot continuosly
    isShooting = true;
    //instantiate the bullet
    GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
    Debug.Log("Bullet genarator");
    //Get the bullet's rigid body component and set its position and rotation equal to that of the spawnPoint
    Rigidbody rb = bullet.GetComponent<Rigidbody>();
    bullet.transform.rotation = spawnPoint.transform.rotation;
    bullet.transform.position = spawnPoint.transform.position;
    //add force to the bullet in the direction of the spawnPoint's forward vector
    rb.AddForce(spawnPoint.transform.forward * 500f);
    //play the gun shot sound and gun animation
    GetComponent<AudioSource>().Play ();
    //gun.GetComponent<Animation>().Play ();
    //destroy the bullet after 1 second
    Destroy (bullet, 1);
    //wait for 1 second and set isShooting to false so we can shoot again
    yield return new WaitForSeconds (1f);
    isShooting = false;
  }
  
  // Update is called once per frame
  void Update () {
  
    //declare a new RayCastHit
    RaycastHit hit;
    //draw the ray for debuging purposes (will only show up in scene view)
    Debug.DrawRay(spawnPoint.transform.position, spawnPoint.transform.forward, Color.green);

    //cast a ray from the spawnpoint in the direction of its forward vector
    if (Physics.Raycast(spawnPoint.transform.position, spawnPoint.transform.forward, out hit, 500)){

      //if the raycast hits any game object where its name contains "zombie" and we aren't already shooting we will start the shooting coroutine
      if (hit.collider.name.Contains("zombie"))
      
       {
         Debug.Log("zombie found ");
        if (!isShooting) {
          StartCoroutine ("Shoot");
        }

      }
        
    }
      
  }
}

