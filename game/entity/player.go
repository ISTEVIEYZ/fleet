package entity

import (
	"github.com/veandco/go-sdl2/sdl"
)

// Player holds all the properties of a world object
type Player struct {
	Entity Entity
	Speed  float64
}

// LoadPlayer initializes the player with the given sprite
func LoadPlayer(renderer *sdl.Renderer, file string) (player Player, err error) {
	player.Entity, err = LoadEntity(renderer, file)
	player.Speed = 0.5
	return player, err
}

// Update the player
func (player *Player) Update(tickRate float64) {
	keys := sdl.GetKeyboardState()

	if keys[sdl.SCANCODE_W] == 1 {
		player.Entity.Position.Y -= player.Speed
	}

	if keys[sdl.SCANCODE_S] == 1 {
		player.Entity.Position.Y += player.Speed
	}

	if keys[sdl.SCANCODE_A] == 1 {
		player.Entity.Position.X -= player.Speed
	}

	if keys[sdl.SCANCODE_D] == 1 {
		player.Entity.Position.X += player.Speed
	}
}

// Draw renders the player on the screen
func (player *Player) Draw(renderer *sdl.Renderer, alpha float64) {
	renderer.Copy(
		player.Entity.Texture,

		&sdl.Rect{
			X: 0,
			Y: 0,
			W: int32(player.Entity.Size.W),
			H: int32(player.Entity.Size.H),
		},

		&sdl.Rect{
			X: int32(player.Entity.Position.X),
			Y: int32(player.Entity.Position.Y),
			W: int32(player.Entity.Scale.W),
			H: int32(player.Entity.Scale.H),
		},
	)
}
