using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

  public float parallaxSpeed = -0.9f;
  private Vector3 startPosition;
  private float backgroundWidth;
  private float backgroundTotalWidth;
  private GameObject bgCloneObject;
  private bool isObjectCloned = false;

  // Start is called before the first frame update
  void Start()
  {

    startPosition = transform.position;
    backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    backgroundTotalWidth = backgroundWidth * 2;

    // create clone
    if (!isObjectCloned) {
      float bgClonePositionX = transform.position.x + backgroundWidth;
      Vector3 bgClonePositionFull = transform.position + new Vector3(bgClonePositionX, transform.position.y, transform.position.z);
      bgCloneObject = Instantiate(gameObject, bgClonePositionFull, transform.rotation);
      bgCloneObject.transform.SetParent(transform.parent);
      bgCloneObject.transform.localScale = transform.localScale;

      // stop further cloning
      isObjectCloned = true;
      Destroy(bgCloneObject.GetComponent<ParallaxBackground>());

      Debug.Log("clone created: " + backgroundWidth);
    }

  }

  // Update is called once per frame
  void Update()
  {

    // move left slowly
    float newPosX = Time.deltaTime * parallaxSpeed;
    transform.position = transform.position + new Vector3(newPosX, transform.position.y, transform.position.z);

    // move the clone slowly
    if (bgCloneObject != null) {
      bgCloneObject.transform.position = bgCloneObject.transform.position + new Vector3(newPosX, bgCloneObject.transform.position.y, bgCloneObject.transform.position.z);
    }

    // reset position when overflown
    if (gameObject.transform.position.x < -backgroundTotalWidth / 2 ){
      ResetPosition(gameObject);
      Debug.Log("background reset ");
    }

    if (bgCloneObject.transform.position.x < -backgroundTotalWidth / 2 ){
      ResetPosition(bgCloneObject);
      Debug.Log("background reset ");
    }


  }

  void ResetPosition(GameObject obj)  {
    // Calculate the new position to place the object at the end of the pattern
    float resetPositionX = obj.transform.position.x + backgroundTotalWidth;
    obj.transform.position = new Vector3(resetPositionX, obj.transform.position.y, obj.transform.position.z);
  }

}
