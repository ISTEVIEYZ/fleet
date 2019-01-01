package main

import (
	"fmt"

	"github.com/oakmound/oak"
	"github.com/oakmound/oak/scene"
)

func main() {
	fmt.Println("steve is cool")

	oak.Add("firstScene",
		// Initialization function
		func(prevScene string, inData interface{}) {},
		// Loop to continue or stop the current scene
		func() bool { return true },
		// Exit to transition to the next scene
		func() (nextScene string, result *scene.Result) { return "firstScene", nil })
	oak.Init("firstScene")
}
