﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestinationScript : MonoBehaviour
{
    private Renderer _renderer;
    private AudioSource _audio;

    [ColorUsage(true, true)]
    public Color originalColor, captureColor;
    public Vector3 capturePointOffset;
    [Space]
    [Header("Particle Systems")]
    public ParticleSystem captureParticle;
    public ParticleSystem storeParticle;
    public ParticleSystem smokeParticle;
    public ParticleSystem capsuleParticle;

    [Space]
    [Header("Sounds")]
    public AudioClip suckSound;
    public AudioClip collectSound;

    public bool active = true;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();
    }

    public Vector3 Point()
    {
        return transform.position + capturePointOffset;
    }

    public void StartCapture()
    {
        if (!active)
            return;

        _audio.pitch = 1.5f;
        _audio.PlayOneShot(suckSound);
        captureParticle.Play();
    }

    public void FinishCapture()
    {
        if (!active)
            return;

        _audio.pitch = 1;
        _audio.PlayOneShot(collectSound);

        storeParticle.Play();
        smokeParticle.Play();
        capsuleParticle.Play();
        _renderer.material.DOColor(captureColor, "_EmissionColor", .2f).OnComplete(() => _renderer.material.DOColor(originalColor, "_EmissionColor", .5f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Point(), .2f);
    }
}
