# ![Fleet](https://i.imgur.com/XgIqPLI.png) Fleet

Game written in Go using the SDL2 framework.

### Requirements

+ [dep](https://github.com/golang/dep): Run `go get -u github.com/golang/dep/cmd/dep` in your terminal.
+ [SDL](http://libsdl.org/)
    + Linux: Run `apt install libsdl2{,-image,-mixer,-ttf,-gfx}-dev`
    + Windows: Install [MSYS](http://www.msys2.org/) then run the following commands in the MSYS terminal:
        + Update the repositories - `pacman -Syu`, `pacman -Su` (Run this one again if any errors)
        + Install GCC and SDL2 - `pacman -S mingw-w64-x86_64-gcc mingw-w64-x86_64-SDL2{,_image,_mixer,_ttf,_gfx}`
        + Add mingw bin folder to your PATH under system environment variables.

### Build

+ Run `dep ensure` to get all the packages needed for the project (you should have a "vendor" folder once it's done).
+ Either run `go run main.go` or `go build` and then run the executable that Go creates.
