package entity

import (
	"github.com/veandco/go-sdl2/sdl"
)

// Player holds all the properties of a world object
type Player struct {
	entity Entity
}

// LoadPlayer initializes the player with the given sprite
func LoadPlayer(renderer *sdl.Renderer, file string) (player Player, err error) {
	player.entity, err = LoadEntity(renderer, file)
	return player, err
}

// Update the player
func Update(player *Player, tickRate float64) {

}

// Draw renders the player on the screen
func Draw(player *Player, renderer *sdl.Renderer, alpha float64) {
	renderer.Copy(
		player.entity.texture,
		&sdl.Rect{X: 0, Y: 0, W: 348, H: 279},
		&sdl.Rect{X: 100, Y: 100, W: 116, H: 93},
	)
}
