package main

import (
	"fmt"

	"github.com/go-gl/gl/v4.1-core/gl"
	"github.com/veandco/go-sdl2/sdl"
)

var (
	window  *sdl.Window
	context sdl.GLContext
)

func main() {
	// Initialize sdl and create context
	initialize()

	// Setup opengl attributes
	setOpenGLAttributes()

	// This makes our buffer swap syncronized with the monitor's vertical refresh
	sdl.GLSetSwapInterval(1)

	// Run game
	run()
}

func initialize() {
	sdl.Init(sdl.INIT_EVERYTHING)
	defer sdl.Quit()

	window, err := sdl.CreateWindow("Fleet", sdl.WINDOWPOS_CENTERED, sdl.WINDOWPOS_CENTERED, 1280, 720, sdl.WINDOW_OPENGL)
	if err != nil {
		fmt.Println(err)
		return
	}
	defer window.Destroy()

	context, err = window.GLCreateContext()
	if err != nil {
		fmt.Println(err)
		return
	}
	defer sdl.GLDeleteContext(context)

	// Initialize OpenGL
	gl.Init()
}

func setOpenGLAttributes() {
	// SDL_GL_CONTEXT_CORE gives us only the newer version, deprecated functions are disabled
	sdl.GLSetAttribute(sdl.GL_CONTEXT_PROFILE_MASK, sdl.GL_CONTEXT_PROFILE_CORE)

	// 3.2 is part of the modern versions of OpenGL, but most video cards whould be able to run it
	sdl.GLSetAttribute(sdl.GL_CONTEXT_MAJOR_VERSION, 3)
	sdl.GLSetAttribute(sdl.GL_CONTEXT_MINOR_VERSION, 2)

	// Turn on double buffering with a 24bit Z buffer.
	// You may need to change this to 16 or 32 for your system
	sdl.GLSetAttribute(sdl.GL_DOUBLEBUFFER, 1)
}

func run() {
	for {
		for event := sdl.PollEvent(); event != nil; event = sdl.PollEvent() {
			switch event.(type) {
			case *sdl.QuitEvent:
				return
			}
		}

		// Clear our buffer with a black background
		// This is the same as :
		// 		SDL_SetRenderDrawColor(&renderer, 255, 0, 0, 255);
		// 		SDL_RenderClear(&renderer);
		//
		gl.ClearColor(1, 8, 20, 1.0)
		gl.Clear(gl.COLOR_BUFFER_BIT)

		// Swap our back buffer to the front
		// This is the same as :
		// 		SDL_RenderPresent(&renderer);
		window.GLSwap()
	}
}
