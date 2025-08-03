using NUnit.Framework.Constraints;
using System.Collections;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class RobotController : MonoBehaviour
{

    private SpriteRenderer fuelMeterSR;
    private RobotAttachment attachment;
    private Rigidbody2D rb;

    private float maxFuel;
    [SerializeField]
    private Corpse corpse;

    [SerializeField]
    private float _fuel;

    private float resetTimer = 0;
    public float Fuel

    {
        get
        {
            return _fuel;
        }
        set
        {
            if (_fuel != value)
            {
                _fuel = value;
                fuelMat.SetFloat("_FuelLevel", _fuel / maxFuel);
            }
        }
    }

    [SerializeField]
    private Material fuelMat;

    public float centerOfMassX = 0f;
    public float centerOfMassY = -0.5f;

    private List<GameObject> closeEnoughInteractables = new List<GameObject>();
    private GameObject closestInteractableObject;
    private bool isHoldingInteractable = false;
    private GameObject holdingLocation;
    private bool throwing = false;
    public float rightThrowSpeed = 5.0f;
    public float upThrowSpeed = 5.0f;
    public float throwCooldown = 1.0f;

    public bool facingRight = true;

    

    public GameObject GetInteractableObject()
    {
        return closestInteractableObject;
    }

    public void SetInteractableObject(GameObject _interactableObject)
    {
        closestInteractableObject = _interactableObject;
    }

    public bool GetIsHoldingInteractable()
    {
        return isHoldingInteractable;
    }

    public void addCloseInteractable(GameObject go)
    {
        if (!closeEnoughInteractables.Contains(go))
        {
            closeEnoughInteractables.Add(go);
        }
    }

    public void removeCloseInteractable(GameObject go)
    {
        if (closeEnoughInteractables.Contains(go))
        {
            closeEnoughInteractables.Remove(go);
        }
    }

    void Start()
    {
        attachment = GetComponentInChildren<RobotAttachment>();

        fuelMeterSR = GameObject.Find("Fuel Meter").GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        attachment.rb = rb;
        rb.centerOfMass = new Vector2(-centerOfMassX, centerOfMassY);
        maxFuel = Fuel;
        holdingLocation = GameObject.Find("holdingLocation");
    }

    private void Respawner()
    {
        Instantiate(corpse, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        bool keyPressed = false;
        bool leftRightPressed = false;
        if (Fuel > 0)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                attachment.RightArrow();
                keyPressed = true;
                leftRightPressed = true;
                facingRight = true;
                FaceDirection(1);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                attachment.LeftArrow();
                keyPressed = true;
                leftRightPressed = true;
                facingRight = false;
                FaceDirection(-1);
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                attachment.UpArrow();
                keyPressed = true;
            }
            if (Input.GetKey(KeyCode.R))
            {
                resetTimer += Time.fixedDeltaTime;
                if (resetTimer < 0.5)
                {
                    return;
                }
                /*int ExplosionRadius = 2;
                Vector2 pos = transform.position;
                LayerMask mask = LayerMask.GetMask("GroundLayer");
                Tilemap ground = FindAnyObjectByType<Tilemap>();//Get the tilemap

                for (int x = -ExplosionRadius; x < ExplosionRadius; x++)
                {
                    for (int y = -ExplosionRadius; y < ExplosionRadius; y++) //find the box
                    {
                        Vector3Int Tilepos = ground.WorldToCell(new Vector2(pos.x + x, pos.y + y));
                        ground.SetTile(Tilepos, null);
                    }
                }*/
                Respawner();

            }
        }
        else
        {
            Respawner();
        }

        if (!leftRightPressed)
        {
            attachment.NoLeftRight();
        }

        if (!keyPressed)
        {
            attachment.Idle();
            resetTimer = 0;
        }

        if (Input.GetKey(KeyCode.E) && !throwing)
        {
            StartCoroutine(PickUpOrThrow());
        }

        if (throwScale > 2 || (!Input.GetKey(KeyCode.E) && throwScale > 0))
        {
            throwScale = 2;
            StartCoroutine(ThrowObject());
        }

        if (Mathf.Abs(rb.linearVelocityX) < .5)
        {
            rb.linearVelocityX = 0;
        }
    }

    public float maxTiltAngle = 15f;
    void LateUpdate()
    {
        if (rb.linearVelocityX < .1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            return;
        }

        //clampRotation so that he doesn't fall over
        Vector3 currentRotation = transform.eulerAngles;
        float z = currentRotation.z > 180 ? currentRotation.z - 360 : currentRotation.z;
        z = Mathf.Clamp(z, -maxTiltAngle, maxTiltAngle);
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    void FaceDirection(int direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
        fuelMeterSR.flipX = direction > 0 ? false : true;
    }

    private float throwScale = 0f;

    private IEnumerator PickUpOrThrow()
    {
        float closestDist = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject obj in closeEnoughInteractables)
        {
            float dist = Vector2.Distance(transform.position, obj.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = obj;
            }
        }
        closestInteractableObject = closest;

        if (closestInteractableObject != null)
        {
            GameObject pickUpObject = closestInteractableObject;
            Rigidbody2D interactableRB = pickUpObject.GetComponent<Rigidbody2D>();
            Collider2D col = pickUpObject.GetComponent<Collider2D>();
            ThowableObjectBehavior tob = pickUpObject.GetComponent<ThowableObjectBehavior>();
            if (!isHoldingInteractable)
            {
                //Disables collision so it can go through the player and makes it so it wont move once it is there
                col.isTrigger = true;
                interactableRB.gravityScale = 0;
                pickUpObject.transform.SetParent(holdingLocation.transform, true);
                tob.setLerping(true);

                //Lerp Variables
                float t = 0.0f;
                float lerpSpeed = 0.005f;
                float minLerpDistance = 0.1f;
                Vector3 startLoc = pickUpObject.transform.position;

                //The Lerp
                while (Vector3.Magnitude(pickUpObject.transform.position - holdingLocation.transform.position) > minLerpDistance)
                {
                    t += lerpSpeed;
                    pickUpObject.transform.position = Vector3.Lerp(startLoc, holdingLocation.transform.position, t);
                    yield return null;
                }

                //couple touch-ups
                pickUpObject.transform.position = holdingLocation.transform.position;
                isHoldingInteractable = true;
                tob.setLerping(false);
                closestInteractableObject = pickUpObject;
            } else
            {
                throwScale += Time.deltaTime;
            }
        }
    }

    private IEnumerator ThrowObject()
    {
        if (isHoldingInteractable)
        {
            GameObject pickUpObject = closestInteractableObject;
            Rigidbody2D interactableRB = pickUpObject.GetComponent<Rigidbody2D>();
            Collider2D col = pickUpObject.GetComponent<Collider2D>();
            Vector3 throwDirectionVector = transform.right * rightThrowSpeed * (facingRight ? 1 : -1) + transform.up * upThrowSpeed;
            interactableRB.gravityScale = 1;
            col.isTrigger = false;
            closestInteractableObject.transform.parent = null;
            interactableRB.AddForce(throwDirectionVector * throwScale, ForceMode2D.Impulse);
            isHoldingInteractable = false;
            throwing = true;
            throwScale = 0;
            yield return new WaitForSeconds(throwCooldown);
            throwing = false;
        }
    }

    private bool respawning = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dangerous" && !respawning)
        {
            respawning = true;
            Respawner();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dangerous" && !respawning)
        {
            respawning = true;
            Respawner();
        }
    }
}
