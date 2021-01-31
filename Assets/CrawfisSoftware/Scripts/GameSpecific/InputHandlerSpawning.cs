using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CrawfisSoftware.SpawnerTest
{
    public class InputHandlerSpawning : MonoBehaviour
    {
        public InputActionAsset testActions;
        InputActionMap keyboardActionMap;
        InputAction quitInputAction;
        InputAction spawnInputAction;
        [SerializeField] EventStarTrekBeamDown spawner;

        private void Awake()
        {
            keyboardActionMap = testActions.FindActionMap("Test");
            quitInputAction = keyboardActionMap.FindAction("Quit");
            quitInputAction.performed += context => { Application.Quit(); };

            spawnInputAction = keyboardActionMap.FindAction("Spawn");
            spawnInputAction.performed += context => HandleSpawnInputEvent();
        }

        private void HandleSpawnInputEvent()
        {
            Debug.Log("Spawned on Input");
            spawner.SpawnNow();
        }

        private void OnEnable()
        {
            quitInputAction.Enable();
            spawnInputAction.Enable();
        }

        private void OnDisable()
        {
            quitInputAction.Disable();
            spawnInputAction.Disable();
        }
    }
}
