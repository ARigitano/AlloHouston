using CRI.HelloHouston.Experience;
using CRI.HelloHouston.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.GameElement
{
    public class RoomAnimator : MonoBehaviour
    {
        public struct RoomAnimatorInstruction
        {
            public bool install;
            public int index;
            public XPManager manager;
            public Action action;

            public RoomAnimatorInstruction(bool install, int index, XPManager manager, Action action)
            {
                this.install = install;
                this.index = index;
                this.manager = manager;
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

        private Queue<RoomAnimatorInstruction> _instructionQueue = new Queue<RoomAnimatorInstruction>();
        private float _lastDequeue = Time.time;

        private void OnEnable()
        {
            _armAnimator.onAnimationEnd += Dequeue;
        }

        private void Start()
        {
            if (_armAnimator == null)
                _armAnimator = GetComponentInChildren<ArmAnimator>();
            if (_plierAnimators == null)
                _plierAnimators = GetComponentsInChildren<PlierAnimator>();
        }

        /// <summary>
        /// Installs the tubex at the desired location. The index is the index of the plier on which the tubex is to be installed.
        /// </summary>
        /// <param name="index">Index of the plier on which the tubex is to be installed.</param>
        /// <param name="tubex">The tubex that will be installed.</param>
        /// <param name="actionOnInstall">Action to be called when the installation is done.</param>
        public void InstallTubex(int index, XPManager manager, Action actionOnInstall = null)
        {
            if (!_armAnimator.busy)
            {
                // We only start the animation if the tubex and the pliers exists.
                if (index < _plierAnimators.Length && manager != null)
                {
                    PlierAnimator plierAnimator = _plierAnimators[index];
                    // We can't start the animation if there's already a tubex in the plier or if a tubex of the same experiment is loaded elsewhere.
                    if (plierAnimator.manager == null && plierAnimator.tubex == null && _plierAnimators.Count(x => x.manager) == 0)
                    {
                        GameObject tubex = Instantiate(_tubexDatabase.GetTubex(manager.xpContext.tubexType));
                        _armAnimator.SetTubex(tubex, index, plierAnimator.transform, true);
                        plierAnimator.manager = manager;
                        plierAnimator.tubex = tubex;
                        _armAnimator.InstallTubex(index, actionOnInstall);
                        plierAnimator.InstallTubex();
                    }
                    else
                        Dequeue();
                }
                else
                    Dequeue();
            }
            else
            {
                _instructionQueue.Enqueue(new RoomAnimatorInstruction(true, index, manager, actionOnInstall));
            }
        }

        /// <summary>
        /// Installs the tubex from the desired location. The index is the index of the plier on which the tubex is to be uninstalled.
        /// </summary>
        /// <param name="index">Index of the plier on which the tubex is to be uninstalled.</param>
        /// 
        /// <param name="manager">Manager of the tubex that will be uninstalled.</param>
        /// <param name="actionOnInstall">Action to be called when the uninstallation is done.</param>
        public void UninstallTubex(int index, XPManager manager, Action actionOnUninstall = null)
        {
            if (!_armAnimator.busy)
            {
                if (index < _plierAnimators.Length)
                {
                    PlierAnimator plierAnimator = _plierAnimators[index];
                    GameObject tubex = plierAnimator.tubex;
                    XPManager plierManager = plierAnimator.manager;
                    // We can't play the animation if the plier doesn't have the tubex.
                    if (tubex != null && manager != null && plierManager == plierAnimator.manager)
                    {
                        _armAnimator.SetTubex(tubex, index, plierAnimator.transform, false);
                        _armAnimator.UninstallTubex(index, actionOnUninstall);
                        plierAnimator.tubex = null;
                        plierAnimator.manager = null;
                        if (index < _plierAnimators.Length)
                            _plierAnimators[index].UninstallTubex();
                    }
                    else
                        Dequeue();
                }
                else
                    Dequeue();
            }
            else
            {
                _instructionQueue.Enqueue(new RoomAnimatorInstruction(false, index, null, actionOnUninstall));
            }
        }

        private void InstallTubex(RoomAnimatorInstruction instruction)
        {
            int index = instruction.index;
            XPManager manager = instruction.manager;
            Action actionOnInstall = instruction.action;
            InstallTubex(index, manager, actionOnInstall);
        }

        private void UninstallTubex(RoomAnimatorInstruction instruction)
        {
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