using System;
using Fleet.Components.Physics.Interfaces;
using Fleet.Entities.Base;
using Fleet.Managers;
using Microsoft.Xna.Framework;

namespace Fleet.Components.Physics
{
	public class AIMovementComponent : IMovementComponent
	{
		private Vector2 _targetDirection;
		private float _targetRotation;
		private Vector2 _playerPosition;

		public AIMovementComponent() { }

		public void Move(GameTime gameTime, Entity entity)
		{
			_playerPosition = GameManager.Instance.player.position;
			TurnToFace(entity, _playerPosition);
			entity.position = Vector2.Lerp(entity.position, _playerPosition, 0.005f);
		}

		private void TurnToFace(Entity turnee, Vector2 target)
		{
			_targetDirection = target - turnee.position;
			_targetRotation = (float)Math.Atan2(_targetDirection.Y, _targetDirection.X);

			if (turnee.rotation < _targetRotation) // The scaler here can be replaced by a "turnspeed" in the future
				turnee.rotation += 0.1f;
			else if (turnee.rotation > _targetRotation)
				turnee.rotation -= 0.1f;
		}
	}
}
