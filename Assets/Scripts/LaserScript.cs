using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour, IDamaging
{
      [Header("Laser pieces")]
      public GameObject laserStart;
      public GameObject laserMiddle;
      public GameObject laserEnd;
      public LayerMask mask;
      public int damageDealt = 3;

      private GameObject start;
      private GameObject middle;
      private GameObject end;

      public int GetDamageDealt()
      {
            return damageDealt;
      }

      private void OnEnable()
      {
            FixedUpdate();
      }

    void FixedUpdate()
      {
            var aimDirection = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            aimDirection.Normalize();


            // Create the laser start from the prefab
            if (start == null)
            {
                  start = Instantiate(laserStart) as GameObject;
                  start.transform.parent = this.transform;
                  start.transform.localPosition = Vector2.zero;
            }

            // Laser middle
            if (middle == null)
            {
              middle = Instantiate(laserMiddle) as GameObject;
              middle.transform.parent = this.transform;
              middle.transform.localPosition = Vector2.zero;
            }

            // Define an "infinite" size, not too big but enough to go off screen
            float maxLaserSize = 20f;
            float currentLaserSize = maxLaserSize;

            // Raycast at the right as our sprite has been design for that
            Vector2 laserDirection = aimDirection;//this.transform.right;
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, laserDirection, maxLaserSize, mask);

            if (hit.collider != null)
            {
                  // We touched something!
                  IDamageable dmg = hit.collider.GetComponent<IDamageable>();
                  if (dmg != null)
                  {
                        dmg.Damage(GetDamageDealt());
                  }
                
                  // -- Get the laser length
                  currentLaserSize = Vector2.Distance(hit.point, this.transform.position);

                  // -- Create the end sprite
                  if (end == null)
                  {
                        end = Instantiate(laserEnd) as GameObject;
                        end.transform.parent = this.transform;
                        end.transform.localPosition = Vector2.zero;
                        end.transform.rotation = this.transform.rotation;
                  }
            }
            else
            {
                  // Nothing hit
                  // -- No more end
                  if (end != null) Destroy(end);
            }

            // Place things
            // -- Gather some data
            float startSpriteWidth = start.GetComponent<Renderer>().bounds.size.x * 0.5f ;
            float endSpriteWidth = 0f;
            if (end != null) endSpriteWidth = end.GetComponent<Renderer>().bounds.size.x;

            // -- the middle is after start and, as it has a center pivot, have a size of half the laser (minus start and end)
            middle.transform.localScale = new Vector3(currentLaserSize - startSpriteWidth, middle.transform.localScale.y, middle.transform.localScale.z);
            middle.transform.localPosition = new Vector2((currentLaserSize/2f), 0f);

            // End?
            if (end != null)
            {
              end.transform.localPosition = new Vector2(currentLaserSize, 0f);
            }

            //Rotate everything according to aimDirection
            float deg = Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x);
            Quaternion quat = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, deg);
            this.transform.rotation = quat;

      }
}
