using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    Vector3 telePos;
    public LayerMask whoIsPlayer;
    public float respawnPos;
    public bool isVertical;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == (int)Mathf.Log(whoIsPlayer,2))
        {
            Debug.LogWarning("saaaaaaaaaaaaaaa");
            Vector3 playerPos = GameManager.Instance.playerStats.transform.position;
            if (isVertical)
            {

                GameManager.Instance.playerStats.transform.position
                    = new Vector3(-respawnPos, playerPos.y, 0);
            }
            else
            {
                GameManager.Instance.playerStats.transform.position
                    = new Vector3(playerPos.x, -respawnPos, 0);
            }
        }
    }
}
