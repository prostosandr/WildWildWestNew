using UnityEngine;

public interface IDamageble 
{
    public void TakeDamage(float damage, Vector3 postion, Quaternion rotation);
}