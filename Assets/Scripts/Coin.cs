using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject coinParticle;
    [SerializeField] AudioClip pickUpSoundClip;
    [SerializeField] int amountCoinsToAdd;

    private void Update()
    {
        float rotationSpeed = 45f;
        transform.eulerAngles += Vector3.up * Time.deltaTime * rotationSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CreateBreakingParticle();
            PlayerController.Instance.AddPoint(amountCoinsToAdd);
            Destroy(this.gameObject);
            SoundManager.Instance.PlaySound(pickUpSoundClip);
            UIManager.Instance.AddCoinInfo(amountCoinsToAdd);
        }
    }
    private void CreateBreakingParticle()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        GameObject pointParticleObject = Instantiate(coinParticle, transform.position + offset, Quaternion.identity);
        Destroy(pointParticleObject, 0.5f);
    }
    public void MoveCoinToPlayer(Vector3 playerPosition)
    {
        Vector3 moveDirection = playerPosition - transform.position;
        float moveSpeed = 5f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
