﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : Tower {

    [SerializeField] float Range;
    [SerializeField] LayerMask WhatToHit;
    [SerializeField] float radius;
    [SerializeField] float damage;
    [SerializeField] float cooldown;
    public bool isPowered;
    [SerializeField] GameObject pointLight;
    [SerializeField] Transform topOfTheTower;
    float timer = 0;
    [SerializeField] bool debug = false;
    [SerializeField] AudioSource powerSound;
    //[SerializeField] AudioSource shotSound;

    private void Awake()
    {
        isPowered = false;
        //shotSound.Pause();
        powerSound.Pause();
        range = Range;
        whatToHit = WhatToHit;
    }

    private void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.5f);
    }

    protected override void Update()
    {
        base.Update(); 

        pointLight.SetActive(isPowered);
        if (isPowered || debug)
        {
            powerSound.UnPause();
            isPowered = false;
            if (target == null) return;
            topOfTheTower.LookAt(target, Vector3.forward);
            topOfTheTower.rotation = Quaternion.Euler(new Vector3(topOfTheTower.rotation.eulerAngles.x, topOfTheTower.rotation.eulerAngles.y, 0)); 
            if (timer <= 0)
            {
                AttackEnemies();
                timer = cooldown;
            }
            timer -= Time.deltaTime;
        }
        else
        {
            powerSound.Pause();
        }
    }

    public void Powered()
    {
        isPowered = true;
    }

    void AttackEnemies ()
    {
        Collider[] hits = Physics.OverlapSphere(target.position, radius, whatToHit);
        foreach (Collider h in hits)
        {
            Enemy enemy = h.transform.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Status.Damage(enemy, damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.blue;
        if (target != null) Gizmos.DrawWireSphere(target.position, radius);
    }

}
