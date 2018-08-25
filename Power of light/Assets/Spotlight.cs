﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : Tower {

    [SerializeField] float Range;
    [SerializeField] LayerMask WhatToHit;
    [SerializeField] float radius;
    [SerializeField] float damage;
    [SerializeField] float cooldown;
    float timer = 0;

    private void Awake()
    {
        range = Range;
        whatToHit = WhatToHit; 
    }

    private void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (target == null) return;
        transform.LookAt(target);
        if (timer <= 0)
        {
            AttackEnemies();
            timer = cooldown; 
        }
        timer -= Time.deltaTime; 
    }

    void AttackEnemies ()
    {
        Collider[] hits = Physics.OverlapSphere(target.position, radius); 
        foreach (Collider h in hits)
        {
            Enemy enemy = h.GetComponent<Enemy>();
            Status.Damage(enemy, damage); 
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
