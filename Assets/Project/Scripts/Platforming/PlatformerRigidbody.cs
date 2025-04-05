using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VariousMechanics
{
	public class PlatformerRigidbody : MonoBehaviour
	{
		public float HorzVelocity
		{
			get => horzVel;
			set => horzVel = value;
		}

		public bool Grounded
		{
			get => grounded;
		}

		private void FixedUpdate()
		{
			Vector2 vel = new(
				horzVel,
				Body.velocity.y
			);

			if (jumpVel != 0)
			{
				vel.y = jumpVel;
				jumpVel = 0;
			}

			Body.velocity = vel;
		}

		private void OnCollisionEnter(Collision collision)
		{
			foreach (var pt in collision.contacts)
			{
				if (Vector3.Angle(pt.normal, Vector3.down) < MaxCrushAngle)
				{
					Crushed.Invoke(new CrushInfo()
					{
						Source = collision.gameObject,
					});
				}

				if (Vector3.Angle(pt.normal, Vector3.up) < MaxGroundAngle)
				{
					grounded = true;
					if (ActiveSurface)
					{
						ActiveSurface.Detach();
					}
					Landed.Invoke();
					if (collision.gameObject.TryGetComponent<PlatformSurface>(out var surface))
					{
						ActiveSurface = surface;
						ActiveSurface.Attach(this);
					}
				}
			}
		}

		public void Detach()
		{
			grounded = false;
			if (ActiveSurface)
			{
				ActiveSurface.Detach();
			}
		}

		public void Jump(float upVel)
		{
			Detach();
			jumpVel = upVel;
		}

		public UnityEvent<CrushInfo> Crushed;
		public UnityEvent Landed;
		public Rigidbody Body;
		public CapsuleCollider Capsule;
		public PlatformSurface ActiveSurface;

		public bool AllowLanding;
		public float MaxCrushAngle = 20f;
		public float MaxGroundAngle = 20f;

		private bool grounded;
		private float horzVel;
		private float jumpVel;
	}
}