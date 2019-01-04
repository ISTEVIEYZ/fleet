package game

import (
	"fleet/game/entity"
)

// Global variables
var (
	player entity.Player
)

func setup() {
	player, _ = entity.LoadPlayer(renderer, "./assets/images/ship.png")
	player.Speed = 0.5
	player.Entity.Position.X = 100
	player.Entity.Position.Y = 100
	player.Entity.Scale.W = player.Entity.Size.W / 3.0
	player.Entity.Scale.H = player.Entity.Size.H / 3.0
}

func update() {
	player.Update(tickRate)
}

func draw(alpha float64) {
	renderer.SetDrawColor(1, 8, 20, 255)
	renderer.Clear()

	player.Draw(renderer, alpha)

	renderer.Present()
}
