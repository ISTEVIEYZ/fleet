package entity

import (
	"fmt"

	"github.com/veandco/go-sdl2/img"
	"github.com/veandco/go-sdl2/sdl"
)

// Entity holds all the properties of a world object
type Entity struct {
	texture *sdl.Texture
}

// LoadEntity constructs an Entity type
func LoadEntity(renderer *sdl.Renderer, file string) (entity Entity, err error) {
	surface, err := img.Load(file)

	if err != nil {
		fmt.Println(err)
		return Entity{}, fmt.Errorf("Loading entity: %v", err)
	}

	entity.texture, err = renderer.CreateTextureFromSurface(surface)
	defer surface.Free()

	return entity, nil
}
