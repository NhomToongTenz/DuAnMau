using System;
using Unity.VisualScripting;
using UnityEngine;

public class IceandFireController : ThunderStikeController
{
  protected override void OnTriggerEnter2D(Collider2D other)
  {
    base.OnTriggerEnter2D(other);
    
  }
}
