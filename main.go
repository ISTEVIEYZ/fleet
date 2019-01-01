package main

import (
	"fmt"
	"image/color"

	"github.com/oakmound/oak"
	"github.com/oakmound/oak/entities"
	"github.com/oakmound/oak/event"
	"github.com/oakmound/oak/key"
	"github.com/oakmound/oak/physics"
	"github.com/oakmound/oak/render"
	"github.com/oakmound/oak/scene"
)

func main() {
	fmt.Println("steve is cool")

	oak.Add("firstScene",
		// Initialization function
		func(prevScene string, inData interface{}) {
			char := entities.NewMoving(100, 100, 16, 32,
				render.NewColorBox(16, 32, color.RGBA{255, 0, 0, 255}),
				nil, 0, 0)

			render.Draw(char.R)

			char.Speed = physics.NewVector(3, 3)

			char.Bind(func(id int, nothing interface{}) int {
				char := event.GetEntity(id).(*entities.Moving)
				// Move left and right with A and D
				if oak.IsDown(key.A) {
					char.ShiftX(-char.Speed.X())
				}
				if oak.IsDown(key.D) {
					char.ShiftX(char.Speed.X())
				}
				return 0
			}, event.Enter)

		},

		// Loop to continue or stop the current scene
		func() bool { return true },

		// Exit to transition to the next scene
		func() (nextScene string, result *scene.Result) { return "firstScene", nil })

	oak.Init("firstScene")
}
