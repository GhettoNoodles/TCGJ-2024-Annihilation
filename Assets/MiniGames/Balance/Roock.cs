using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roock : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Goblin"))
      {
         ManagerBalance.Instance.IncreaseScore(other.gameObject.GetComponentInParent<Balance_Player>().playerNumber);
      }
      else if (other.gameObject.CompareTag("Floor"))
      {
         Destroy(gameObject);

      }
      
   }
}
