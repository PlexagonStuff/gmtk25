using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Corpse : MonoBehaviour
{
    public float fuelDeposit = 25;
    private bool isDead = false;
    public Sprite DeathEndState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isDead)
        {
            GetComponent<Animator>().SetBool("Done", true);
            return;
        }
        StartCoroutine(Death());   
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(1.5f);
        Animator anim = GetComponent<Animator>();
        anim.SetBool("Done", true);
        anim.enabled = false;
        SceneManager.LoadSceneAsync("RoundStart", LoadSceneMode.Additive);
        GetComponent<SpriteRenderer>().sprite = DeathEndState;
        isDead = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead && collision.gameObject.tag == "Dangerous")
        {
            if (GetComponent<Rigidbody2D>().linearVelocity.magnitude > .1f)
            {
                StartCoroutine(collision.gameObject.GetComponent<CrawlerEnemy>().Die());
                Destroy(gameObject);
            }
        }
    }
}
