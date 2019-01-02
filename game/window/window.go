package window

import (
	"github.com/veandco/go-sdl2/img"
	"github.com/veandco/go-sdl2/sdl"
)

type window struct {
	window   *sdl.Window
	renderer *sdl.Renderer
}

// Window default values
const (
	TITLE  = "Fleet"
	WIDTH  = 1280
	HEIGHT = 720
	X      = sdl.WINDOWPOS_CENTERED
	Y      = sdl.WINDOWPOS_CENTERED
)

var win window

// Init initializes the window and renderer with default values
func Init() (err error) {
	err = createWindow()
	if err != nil {
		return err
	}

	err = createRenderer()
	if err != nil {
		return err
	}

	return nil
}

// GetWindow retrieves a pointer to the window
func GetWindow() *sdl.Window {
	return win.window
}

// GetRenderer retrieves a pointer to the renderer
func GetRenderer() *sdl.Renderer {
	return win.renderer
}

// DestroyWindow destroys the window to free up memory
func DestroyWindow() {
	win.window.Destroy()
}

// DestroyRenderer destroys the renderer to free up memory
func DestroyRenderer() {
	win.renderer.Destroy()
}

func createWindow() (err error) {
	win.window, err = sdl.CreateWindow(TITLE, X, Y, WIDTH, HEIGHT, sdl.WINDOW_OPENGL)

	if err != nil {
		return err
	}

	iconSurface, _ := img.Load("./assets/images/icon.png")
	win.window.SetIcon(iconSurface)
	defer iconSurface.Free()

	return nil
}

func createRenderer() (err error) {
	win.renderer, err = sdl.CreateRenderer(win.window, -1, sdl.RENDERER_ACCELERATED|sdl.RENDERER_PRESENTVSYNC)

	if err != nil {
		return err
	}

	return nil
}
