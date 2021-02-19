﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{

    public class PoisonSporesCircleAttackSkill : BossBaseSkill
    {
        private const float DELAY_HAND_TRIGGER = 0.2f;

        public PoisonSporesCircleAttackSkill((bool, int, float, float, float, bool, bool) skillInfo, Dictionary<int, BossBaseSkill> skillDictionary, BossStateMachine stateMachine) : base(skillInfo, skillDictionary, stateMachine)
        {
        }

        public override void UseSkill(int id)
        {
            Debug.Log("POISON CIRCLE AttackSkill");
            _bossModel.BossTransform.rotation = _bossModel.BossData.RotateTo(_bossModel.BossTransform, _bossModel.BossCurrentTarget.transform, 1, true);
            _bossModel.BossAnimator.Play("PoisonAttack", 0, 0f);
            CreateSpores();

         //   TurnOnHitBoxTrigger(_currenTriggertHand,_stateMachine.CurrentState.CurrentAttackTime, DELAY_HAND_TRIGGER);

            ReloadSkill(id);
        }

        private void CreateSpores()
        {
            var bossPos = _bossModel.BossTransform.position;
            var radius = 5f;
            var sporeCount = 10;
            for (var j = 0; j < sporeCount; j++)
            {
                var groundedPosition = Services.SharedInstance.PhysicsService.GetGroundedPosition(CreateCircle(bossPos, radius), bossPos.y+2);
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, bossPos - groundedPosition);

                GameObject.Destroy(GameObject.Instantiate(_bossModel.SporePrefab, groundedPosition, rot), 5f);
            //  var TimeRem = new TimeRemaining(() => GameObject.Destroy(GameObject.Instantiate(_bossModel.SporePrefab, groundedPosition, rot), 5f), j * 0.1f);
            //TimeRem.AddTimeRemaining(j * 0.1f);
        }
        }
        public override void StopSkill()
        {
        }

        private Vector3 CreateCircle(Vector3 center, float radius)
        {
            var ang = Random.value * 360;
            Vector3 spawnPosition;
            spawnPosition.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            spawnPosition.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            spawnPosition.y = center.y;
            return spawnPosition;
        }
    }
}