--------------Usage and notes------------------
Create an object by selecting from menu GameObject -> Create Other -> Create Quad or by pressing CTLR+Q.

Then add UVSprite to sprites you want to animate.
INSPECTOR VIEW:
	Set in Cell horizontal size and Cell vertical size the pixel dimension of a single sprite's full box. (eg. sprites in your sheet are 13 px wide, but you drawn them in a 16px box, 16 is the value that should be used)
	Starting cell is the cell to be used when no animation plays (and for object with no animation at all). The top-left corner is cell 0-0.
	Mirrored tells the sprite to mirror itself. Animations will be automatically mirrored when this toggle is on.
	Autoplay animation, will tell the script to play a stored animation. This function may be useful for looping animated background objects.
	Under "Animations", press "Add" to add one.
		Name will be used to call this particular animation in scripts.
		Next animation is the animation that will follow this one when it ends. Use "starting" or "none" to stop the animation and set the sprite back to its starting cell. Use "loop" or the same animation name to loop endlessly the same animation over and over. Use "stop" or leave it blank to stop the animation and keep it's last frame.
		Starting cell tells the script where to look for the first image or frame.
		Length in frames set the number of frames the animation is composed of.
		Frame duration is the time (in seconds, float value) each frame/image should be visible before switching to the next one.
	Pressing "Delete" near an animation will delete that particular animation.

FUNCTIONS:
Note: Remember to call something like
	UVSprite mySprite = transform.GetComponent<UVSprite>();
during create, awake or right before calling any other of the following functions. In those examples I use "mySprite" as reference name, you can use whatever name fits your programming habit.

SetAnimation(animation name)
Call it from an object to start an animation sequence. Animation name must be a string like this example:
	mySprite.SetAnimation("myAnimation");
	
SetSingleImage(horizontal cell, vertical cell)
This function will stop any playing animation and set a fixed image as sprite by telling its horizontal and vertical position. This example will set the top-leftmost one.
	mySprite.SetSingleImage(0, 0);
	
SetMirror()
Toggle image mirroring. You can specify true or false inside parentheses to selectively turn it ON or OFF.


Please note:
- Best used with tiled animation and sprites
- The script only support animaton frames on the same line.
- Example folder (scene, script, texture and it's relative material) can be safely deleted.
----------------FAQs---------------------------
Q: Does it support pixel perfect sprites?
A: It should. Set your scene camera to Orthographic mode, then its size to vertical resolution / 2 (eg. if running 640x480, size should be 240).
--------------Changelog------------------------
Versione 0.02
	-Added animation autoplay feature.
	-Edited Loop example to show an example of this new feature.
	-Renamed a bunch of function to uppercase
	-Renamed ExamplesScript to SetAnimationExample

Version 0.01a
	-Added a static image example
	-README: Added instruction on how to create quad sprites
	-README: Added FAQs section

Version 0.01
	-First published version
--------------Credits--------------------------
Created by
	Andrea Giorgio "Muu?" Cerioli

Website
	www.lanoiadimuu.it

License:
	BSD-3-Clause License:
	
	Copyright (c) 2012, Andrea Giorgio "Muu?" Cerioli
	All rights reserved.
	
	Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
	-Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
	-Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
	-Neither the name of the Andrea Giorgio "Muu?" Cerioli nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
	
	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.