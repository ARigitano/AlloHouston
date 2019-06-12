using CRI.HelloHouston.Experience;
using CRI.HelloHouston.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.GameElements
{
    public class RoomAnimator : MonoBehaviour
    {
        public struct RoomAnimatorInstruction
        {
            public bool install;
            public int index;
            public XPManager manager;
            public int managerIndex;
            public Action action;

            public RoomAnimatorInstruction(bool install, int index, XPManager manager, int managerIndex, Action action)
            {
                this.install = install;
                this.index = index;
                this.manager = manager;
                this.managerIndex = managerIndex;
                this.action = action;
            }
        }

        [SerializeField]
        [Tooltip("Animator of the arm.")]
        private ArmAnimator _armAnimator = null;
        [SerializeField]
        [Tooltip("Plier animators.")]
        private PlierAnimator[] _plierAnimators = null;
        [SerializeField]
        [Tooltip("Tubex database.")]
        private TubexDatabase _tubexDatabase = null;
        [SerializeField]
        [Tooltip("All the lights than can be dynamically activated and deactivated.")]
        private GameObject[] _dynamicLights;
        [SerializeField]
        [Tooltip("Array of items that should be enabled whenever there's an alarm.")]
        private GameObject[] _alarmObjects;
        [SerializeField]
        [Tooltip("Animator of the door left.")]
        private Animator _doorAnimatorLeft = null;
        [SerializeField]
        [Tooltip("Animator of the door right.")]
        private Animator _doorAnimatorRight = null;
        [SerializeField]
        private AudioSource _openDoor;
        [SerializeField]
        private AudioSource _closeDoor;
        [SerializeField]
        private AudioSource _moveArm;
        [SerializeField]
        private AudioSource _arrimage;
        [SerializeField]
        private AudioSource _moveArmReverse;


        private Queue<RoomAnimatorInstruction> _instructionQueue = new Queue<RoomAnimatorInstruction>();
        private float _lastDequeue;

        private GameObject[] _tubexs;

        private void OnEnable()
        {
            _armAnimator.onAnimationEnd += Dequeue;
        }

        private void Start()
        {
            _lastDequeue = Time.time;
            if (_armAnimator == null)
                _armAnimator = GetComponentInChildren<ArmAnimator>();
            if (_plierAnimators == null)
                _plierAnimators = GetComponentsInChildren<PlierAnimator>();
        }

        /// <summary>
        /// Open the door.
        /// </summary>
        public void OpenDoor()
        {
            if (_openDoor.clip != null)
                _openDoor.Play();

            _doorAnimatorLeft.SetBool("OpenDoor", true);
            _doorAnimatorRight.SetBool("OpenDoor", true);
        }

        /// <summary>
        /// Close the door.
        /// </summary>
        public void CloseDoor()
        {
            if (_closeDoor.clip != null)
                _closeDoor.Play();

            _doorAnimatorLeft.SetBool("OpenDoor", false);
            _doorAnimatorRight.SetBool("OpenDoor", false);
        }

        /// <summary>
        /// Deactivate all the lights.
        /// </summary>
        public void DeactivateAllLights()
        {
            for (int i = 0; i < _dynamicLights.Length; i++)
                _dynamicLights[i].SetActive(false);
        }

        /// <summary>
        /// Activate all the lights.
        /// </summary>
        public void ActivateAllLights()
        {
            for (int i = 0; i < _dynamicLights.Length; i++)
                _dynamicLights[i].SetActive(true);
        }

        /// <summary>
        /// Activate the alarm.
        /// </summary>
        public void ActivateAlarm()
        {
            for (int i = 0; i < _alarmObjects.Length; i++)
                _alarmObjects[i].SetActive(true);
        }

        /// <summary>
        /// Deactivate the alarm.
        /// </summary>
        public void DeactivateAlarm()
        {
            for (int i = 0; i < _alarmObjects.Length; i++)
                _alarmObjects[i].SetActive(false);
        }

        public void Init(XPManager[] managers)
        {
            _tubexs = new GameObject[managers.Length];
            for (int i = 0; i < managers.Length; i++)
            {
                GameObject tubex;
                if (i < _armAnimator.tubexAttachPoints.Length)
                    tubex = Instantiate(_tubexDatabase.GetTubex(managers[i].xpContext.tubexType), _armAnimator.tubexAttachPoints[i].transform);
                else
                    tubex = Instantiate(_tubexDatabase.GetTubex(managers[i].xpContext.tubexType), _armAnimator.tubexAttachPoints[0].transform);
                _tubexs[i] = tubex;
            }
        }

        /// <summary>
        /// Installs the tubex at the desired location. The index is the index of the plier on which the tubex is to be installed.
        /// </summary>
        /// <param name="index">Index of the plier on which the tubex is to be installed.</param>
        /// <param name="tubex">The tubex that will be installed.</param>
        /// <param name="actionOnInstall">Action to be called when the installation is done.</param>
        public bool InstallTubex(int index, XPManager manager, int managerIndex, Action actionOnInstall = null)
        {
            if (!_armAnimator.busy)
            {
                // We only start the animation if the tubex and the pliers exists.
                if (index < _plierAnimators.Length && manager != null)
                {
                    PlierAnimator plierAnimator = _plierAnimators[index];
                    // We can't start the animation if there's already a tubex in the plier or if a tubex of the same experiment is loaded elsewhere.
                    if (plierAnimator.manager == null && plierAnimator.tubex == null && !_plierAnimators.Any(x => x.manager == manager))
                    {
                        var tubex = _tubexs[managerIndex];
                        int animationIndex = managerIndex > 1 ? UnityEngine.Random.Range(0, 2) : managerIndex;
                        _armAnimator.SetTubex(tubex, animationIndex, plierAnimator.transform, true);
                        plierAnimator.manager = manager;
                        plierAnimator.index = index;
                        plierAnimator.tubex = tubex;

                        if (_moveArm.clip != null)
                            _moveArm.Play();

                        _armAnimator.InstallTubex(index, animationIndex, actionOnInstall);
                        plierAnimator.InstallTubex();
                        return true;
                    }
                    else
                        Debug.Log("Error 1");
                }
                else
                    Debug.Log("Error 2");
            }
            else
            {
                _instructionQueue.Enqueue(new RoomAnimatorInstruction(true, index, manager, managerIndex, actionOnInstall));
            }
            return false;
        }

        /// <summary>
        /// Installs the tubex from the desired location. The index is the index of the plier on which the tubex is to be uninstalled.
        /// </summary>
        /// <param name="index">Index of the plier on which the tubex is to be uninstalled.</param>
        /// <param name="manager">Manager of the tubex that will be uninstalled.</param>
        /// <param name="actionOnInstall">Action to be called when the uninstallation is done.</param>
        public bool UninstallTubex(int index, XPManager manager, Action actionOnUninstall = null)
        {
            if (!_armAnimator.busy)
            {
                if (index < _plierAnimators.Length)
                {
                    PlierAnimator plierAnimator = _plierAnimators[index];
                    GameObject tubex = plierAnimator.tubex;
                    XPManager plierManager = plierAnimator.manager;
                    // We can't play the animation if the plier doesn't have the tubex.
                    if (tubex != null && manager != null && plierManager == manager)
                    {
                        int animationIndex = plierAnimator.index > 1 ? UnityEngine.Random.Range(0, 2) : plierAnimator.index;
                        _armAnimator.SetTubex(tubex, index, plierAnimator.transform, false);
                        _armAnimator.UninstallTubex(index, animationIndex, actionOnUninstall);
                        plierAnimator.tubex = null;
                        plierAnimator.manager = null;
                        plierAnimator.index = 0;
                        if (index < _plierAnimators.Length)
                            _plierAnimators[index].UninstallTubex();

                        if (_moveArm.clip != null)
                            _moveArmReverse.Play();
                        return true;
                    }
                }
            }
            else
            {
                _instructionQueue.Enqueue(new RoomAnimatorInstruction(false, index, manager, 0, actionOnUninstall));
            }
            return false;
        }

        private void InstallTubex(RoomAnimatorInstruction instruction)
        {
            _armAnimator.busy = false;
            int index = instruction.index;
            XPManager manager = instruction.manager;
            int managerIndex = instruction.managerIndex;
            Action actionOnInstall = instruction.action;
            InstallTubex(index, manager, managerIndex, actionOnInstall);
        }

        private void UninstallTubex(RoomAnimatorInstruction instruction)
        {
            _armAnimator.busy = false;
            int index = instruction.index;
            XPManager manager = instruction.manager;
            Action actionOnInstall = instruction.action;
            UninstallTubex(index, manager, actionOnInstall);
        }

        private void Dequeue()
        {
            if (_instructionQueue.Count != 0)
            {
                RoomAnimatorInstruction instruction = _instructionQueue.Dequeue();
                if (instruction.install)
                    InstallTubex(instruction);
                else
                    UninstallTubex(instruction);
                _lastDequeue = Time.time;
            }
        }

        private void Update()
        {
            if (!_armAnimator.busy && Time.time - _lastDequeue > 1.0f)
                Dequeue();
        }
    }
}