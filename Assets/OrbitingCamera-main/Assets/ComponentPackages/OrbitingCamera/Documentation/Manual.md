# Orbiting Behaviour

Add the script `Orbit.cs` to the object that you want to orbit. Set the object that you want it to orbit as the `target` of the orbit script.

The default speed of the orbit is 90 degrees per second. You can change this with the `speed` parameter.

To set a rotation, use the `Rotate` function and pass in values for horizontal and vertical rotation around the target. The range of these inputs is from -1 to 1. An input value of 1 will cause the object to orbit at `speed` degrees per second.

If you want the horizontal or vertical inputs to be flipped, you can set the `invertHorizontal` or `invertVertical` flags. By default, a horizontal input of 1 will rotate to the right around the `target` object, and a vertical input of 1 will rotate up above the `target` object.

By default the orbiting object will automatically remain level with the world's up vector. Also, the rotation vertically is limited to within 5 degrees of the north and south poles of the object. You can change the limit of the rotation by changing the `autoLevelLimit` variable.

If you uncheck the `autoLevel` flag, the orbiting object can rotate all the way over the poles, but the orientation is not automatically levelled. The `autoLevelLimit` has no effect in this case.

## Example scene

Included in this package is an example scene to demonstrate the orbiting behaviour. To build this scene:

- create a target object
- add the `Orbit` script to the camera object
- drag the target object into the `target` slot on the camera
- add the `TestKeyboardInput` or `TestMouseInput` script to the camera
- run the game to test

The camera should orbit the cube if you press the direction keys or `WASD` keys with `TestKeyboardInput` or if you move the mouse with `TestMouseInput`. If you press `Space` with `TestKeyboardInput` it will switch between auto level mode and free rotation.

The `TestKeyboardInput` and `TestMouseInput` scripts demonstrate how to send the input from the `Input` system to the orbit script.
