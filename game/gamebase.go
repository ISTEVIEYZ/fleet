package game

import (
	"fleet/game/window"

	"github.com/veandco/go-sdl2/sdl"
)

// Constants for the game loop
const (
	TICK_RATE = 1.0 / 60.0
)

// Global variables
var (
	renderer *sdl.Renderer
)

func initialize() {
	sdl.Init(sdl.INIT_EVERYTHING)
	window.Init()
	renderer = window.GetRenderer()
}

// Run the main game loop
func Run() {
	initialize()
	setup()

	// Fixed timestep initial setup
	currentTime := float64(sdl.GetTicks()) / 1000.0
	accumulator := 0.0

	// Game loop
	for true {
		for event := sdl.PollEvent(); event != nil; event = sdl.PollEvent() {
			switch event.(type) {
			case *sdl.QuitEvent:
				return
			}
		}

		newTime := float64(sdl.GetTicks()) / 1000.0
		frameTime := newTime - currentTime

		if frameTime > 0.25 {
			accumulator += 0.25
		} else {
			accumulator += frameTime
		}

		for accumulator >= TICK_RATE {
			update()
			accumulator -= TICK_RATE
		}

		alpha := accumulator / TICK_RATE
		draw(alpha)
	}
}
