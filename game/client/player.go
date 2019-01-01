package player

import (
	"image/color"

	"github.com/oakmound/oak"
	"github.com/oakmound/oak/entities"
	"github.com/oakmound/oak/event"
	"github.com/oakmound/oak/key"
	"github.com/oakmound/oak/physics"
	"github.com/oakmound/oak/render"
)

// Update the player with the given implementation
func Update() {
	player := entities.NewMoving(100, 100, 16, 32,
		render.NewColorBox(16, 32, color.RGBA{255, 0, 0, 255}),
		nil, 0, 0)

	render.Draw(player.R)
	player.Speed = physics.NewVector(5, 5)

	player.Bind(func(id int, nothing interface{}) int {
		player := event.GetEntity(id).(*entities.Moving)
		// Move left and right with A and D
		if oak.IsDown(key.A) {
			player.ShiftX(-player.Speed.X())
		}
		if oak.IsDown(key.D) {
			player.ShiftX(player.Speed.X())
		}
		if oak.IsDown(key.S) {
			player.ShiftY(player.Speed.Y())
		}
		if oak.IsDown(key.W) {
			player.ShiftY(-player.Speed.Y())
		}

		return 0
	}, event.Enter)
}
