using System.Collections;
using UnityEngine;

namespace Game
{
    public class ItemFactory : MonoBehaviour
    {
        public enum State
        {
            Idle, CreatingItems, Completed,
        }

        [SerializeField] private float _creationTime;
        [SerializeField] private Storage _storage;
        [SerializeField] private ItemCreationProcess[] _itemCreationProcesses;

        public State<State> CurrentState { get; } = new(State.Idle);

        private void Start()
        {
            StartCoroutine(ItemCreationLoop());
        }

        private IEnumerator ItemCreationLoop()
        {
            while (true)
            {
                for (var i = 0; i < _itemCreationProcesses.Length; i++)
                {
                    if (_itemCreationProcesses[i].CurrentState.Value is ItemCreationProcess.State.Completed
                                                                        or ItemCreationProcess.State.Progress)
                    {
                        continue;
                    }

                    if (CurrentState.Value != State.CreatingItems)
                    {
                        CurrentState.ChangeTo(State.CreatingItems);
                    }

                    // yield return CreateItem(_itemCreationProcesses[i]);
                    yield return _itemCreationProcesses[i].Launch(_creationTime);

                    _storage.Add(_itemCreationProcesses[i].Result);
                }

                if (CurrentState.Value != State.Completed)
                {
                    CurrentState.ChangeTo(State.Completed);
                }

                yield return null;
            }
        }

        // private IEnumerator CreateItem(ItemCreationProcess itemCreationProcess)
        // {
        //     var timeElapsed = 0f;
        //     var percent = 0f;
        //
        //     while (percent < 1)
        //     {
        //         percent = timeElapsed / _creationTime;
        //         timeElapsed += Time.deltaTime;
        //
        //         itemCreationProcess.Progress(percent);
        //
        //         yield return null;
        //     }
        //
        //     itemCreationProcess.Complete();
        //     _storage.Add(itemCreationProcess.Result);
        // }
    }
}