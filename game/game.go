package game

import (
	player "fleet/game/client"

	"github.com/oakmound/oak"
	"github.com/oakmound/oak/scene"
)

// Run the main game loop
func Run() {
	oak.Add("firstScene",
		// Initialization function
		func(prevScene string, inData interface{}) {
			player.Update()
		},

		// Loop to continue or stop the current scene
		func() bool { return true },

		// Exit to transition to the next scene
		func() (nextScene string, result *scene.Result) { return "firstScene", nil })

	// render.SetDrawStack(
	// 	render.NewCompositeR(),
	// 	render.NewDrawFPS(),
	// )

	oak.Init("firstScene")
}
