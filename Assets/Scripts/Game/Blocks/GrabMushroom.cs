﻿using UnityEngine;
using System.Collections;

/*
 * GrabMushroom Class - Handler/Mediator for Mushroom Component
 *
 */

public class GrabMushroom : MonoBehaviour {

    public int pointEarned = 500;

    [SerializeField]
    private GameModel _refGameModel;

    [SerializeField]
    private AudioManager _audioMgr;

    void Awake()
    {
        _refGameModel = GameModel.GetInstance();
        _audioMgr = AudioManager.GetInstance();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == GameHash.HERO_TAG)
        {
            GetComponent<AudioSource>().volume = _audioMgr.SfxAudioVolume;
            GetComponent<AudioSource>().Play();
            Destroy(gameObject.GetComponent<Collider2D>());
            gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 0.47f);// Destroy the object -after- the sound played

            _refGameModel.setLevelScore(pointEarned);
            _refGameModel.addBonus();
        }
    }
}
