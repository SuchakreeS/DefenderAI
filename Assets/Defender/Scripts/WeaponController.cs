using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Defender;

public class WeaponController : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------

    [SerializeField] Transform m_SprawnTransform;
    [SerializeField] GameObject m_BulletPrefab;
    [SerializeField] float m_FireRate;    // Every m_FireRate second
    [SerializeField] float m_BulletSpeed;
    [SerializeField] int m_Magazine;
    [SerializeField] float m_Reload;

    // --------------------------------------------------------------------------------------------------
    private DefenderAgent _Agent;
    private GameObject _BulletStack;
    private bool _isReload;
    private bool _isFiring;
    private IDisposable _FiringDisposable;

    // --------------------------------------------------------------------------------------------------
    private void Start()
    {

    }

    // --------------------------------------------------------------------------------------------------
    // Public Function
    public void Init()
    {
        _isReload = false;
        _isFiring = false;

        Refresh();

        _Agent = gameObject.GetComponent<DefenderAgent>();
    }
    public void Fire()
    {
        if (!_isFiring)
        {
            Debug.Log("Firing");
            _isFiring = true;
            SpawnBullet();
            _FiringDisposable?.Dispose();
            _FiringDisposable = Observable.Timer(TimeSpan.FromSeconds(m_FireRate)).Subscribe(_ => _isFiring = false);
        }
        else
        {
            Debug.Log("Wait for firing");
        }
    }
    // --------------------------------------------------------------------------------------------------
    // Private Function
    private void SpawnBullet()
    {
        var bulletObject = (GameObject) Instantiate(m_BulletPrefab, m_SprawnTransform);
        var bulletController = bulletObject.gameObject.GetComponent<BulletController>();
        bulletController.Init(_Agent);
        bulletObject.transform.SetParent(_BulletStack.transform);
        bulletObject.transform.position = m_SprawnTransform.position;
        var bulletRb = bulletObject.gameObject.GetComponent<Rigidbody>();

        bulletRb.AddForce(bulletObject.transform.forward * m_BulletSpeed, ForceMode.VelocityChange);
    }
    [ContextMenu("Destroy Stack")]
    public void Refresh()
    {
        _isReload = false;
        _isFiring = false;
        
        if(_BulletStack != null)
        {
            Destroy(_BulletStack.gameObject);
        }
        _BulletStack = new GameObject("BulletStack");
        _BulletStack.transform.SetParent(transform);
    }
    // --------------------------------------------------------------------------------------------------
    
}
