package entity

import (
	"fmt"

	"github.com/veandco/go-sdl2/img"
	"github.com/veandco/go-sdl2/sdl"
)

// Entity holds all the properties of a world object
type Entity struct {
	Texture  *sdl.Texture
	Position Position
	Size     Size
	Scale    Scale
	Angle    float64
}

// Position holds an X and Y point
type Position struct {
	X, Y float64
}

// Size holds a width and height value
type Size struct {
	W, H float64
}

// Scale holds a width and height value (goes off the original size)
type Scale struct {
	W, H float64
}

// LoadEntity constructs an Entity type
func LoadEntity(renderer *sdl.Renderer, file string) (entity Entity, err error) {
	surface, err := img.Load(file)

	if err != nil {
		fmt.Println(err)
		return Entity{}, fmt.Errorf("Loading entity: %v", err)
	}

	entity.Texture, err = renderer.CreateTextureFromSurface(surface)
	entity.Size.W = float64(surface.W)
	entity.Size.H = float64(surface.H)
	defer surface.Free()

	return entity, nil
}
