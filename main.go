package main

import (
	"fmt"
	"time"

	"github.com/go-gl/gl/v4.1-core/gl"
	"github.com/veandco/go-sdl2/sdl"
)

var (
	window  *sdl.Window
	context sdl.GLContext
)

const (
	tickRate = 1.0 / 60.0
)

func main() {
	// Initialize sdl and create context
	initialize()

	// Setup opengl attributes
	setOpenGLAttributes()

	// This makes our buffer swap syncronized with the monitor's vertical refresh
	sdl.GLSetSwapInterval(1)

	// Run and quit when done
	run()
	quit()
}

func initialize() {
	sdl.Init(sdl.INIT_EVERYTHING)

	window, err := sdl.CreateWindow("Fleet", sdl.WINDOWPOS_CENTERED, sdl.WINDOWPOS_CENTERED, 1280, 720, sdl.WINDOW_OPENGL)
	if err != nil {
		fmt.Println(err)
		return
	}

	context, err = window.GLCreateContext()
	if err != nil {
		fmt.Println(err)
		return
	}

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

	// Fixed timestep initial setup
	currentTime := float64(sdl.GetTicks()) / 1000.0
	accumulator := 0.0

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

		for accumulator >= tickRate {
			// update()
			accumulator -= tickRate
		}

		alpha := accumulator / tickRate
		draw(alpha)
	}
}

func draw(alpha float64) {
	gl.ClearColor(0.0, 0.0, 0.0, 1.0) // Why wont this work?
	gl.Clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT)
	time.Sleep(50 * time.Millisecond)
	window.GLSwap()
}

func quit() {
	sdl.GLDeleteContext(context)
	window.Destroy()
	sdl.Quit()
}
