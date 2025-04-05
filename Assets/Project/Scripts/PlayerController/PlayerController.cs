using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VariousMechanics
{
	public class PlayerController : MonoBehaviour
	{
		private void Awake()
		{
			inputWalk = InputSource.actions.FindAction("Player/Move");
			inputJump = InputSource.actions.FindAction("Player/Jump");
			Platformer.Crushed.AddListener(OnCrushed);
		}

		private void Update()
		{
			Vector2 move = inputWalk.ReadValue<Vector2>();
			bool jump = inputJump.WasPerformedThisFrame();

			Platformer.HorzVelocity = WalkSpeed * move.x;
			if (jump)
			{
				Platformer.Jump(JumpSpeed);
			}
		}

		private void OnCrushed(CrushInfo info)
		{
			Debug.Log($"Crushed by {info.Source}!", this);
			gameObject.SetActive(false);
		}

		public PlatformerRigidbody Platformer;
		public PlayerInput InputSource;
		public float WalkSpeed = 8f;
		public float JumpSpeed = 8f;

		private InputAction inputWalk;
		private InputAction inputJump;
	}
}