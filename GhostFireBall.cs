using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFireBall : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    
    

    // Update is called once per frame
    void Update()
    {
        //body.velocity = new Vector2(-speed, body.velocity.y);
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        Destroy(this.gameObject, 4f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == LevelTwoTags.escapeStump)
        {
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == LevelTwoTags.Cactus)
        {
            Destroy(this.gameObject);
        }
    }

   
}
