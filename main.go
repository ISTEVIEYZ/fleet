package main

import (
	"fmt"

	"github.com/go-gl/gl/v4.1-core/gl"
	"github.com/go-gl/mathgl/mgl32"
	"github.com/veandco/go-sdl2/sdl"
)

func main() {
	fmt.Println("golang sux")

	err := gl.Init()
	if err != nil {
		fmt.Println("you fucked up asshole")
		return
	}

	gl.Viewport(0, 0, 1280, 720)

	window, err := sdl.CreateWindow("penis", sdl.WINDOWPOS_UNDEFINED,
		sdl.WINDOWPOS_UNDEFINED,
		1280, 720, sdl.WINDOW_OPENGL)

	for {
		gl.Clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT)
		window.GLSwap()
	}

	transform := mgl32.Translate3D(0, 0, 0)
	transform2 := mgl32.Translate3D(640, 360, 0)

	result := transform.Mul4(transform2)

	fmt.Println(result)

	// game.Run()

}
