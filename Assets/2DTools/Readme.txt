--------------Usage and notes------------------
Add UVSprite to sprites you want to animate.
In Inspector view:
	Set in Cell horizontal size and Cell vertical size the pixel dimension of a single sprite's full box. (eg. sprites in your sheet are 13 px wide, but you drawn them in a 16px box, 16 is the value that should be used)
	Starting cell is the cell to be used when no animation plays (and for object with no animation at all). The top-left corner is cell 0-0.
	Mirrored tells the sprite to mirror itself. Animations will be automatically mirrored when this toggle is on.
	Under "Animations", press "Add" to add one.
		Name will be used to call this particular animation in scripts.
		Next animation is the animation that will follow this one when it ends. Use "none" (or leave it blank) to stop the animation and set the sprite back to its starting cell. Use "loop" or the same animation name to loop endlessly the same animation over and over. Use "keep" or "stop" to stop the animation and keep this frame.
		Starting cell tells the script where to look for the first image or frame.
		Length in frames set the number of frames the animation is composed of.
		Frame duration is the time (in seconds, float value) each frame/image should be visible before switching to the next one.
	Pressing "Delete" near an animation will delete that particular animation.
	
How to play animations:
Inside your game scripts get call setAnimation(animation name) from an object to start an animation sequence.
Example (try it in your create or awake):
	UVSprite spr = transform.GetComponent<UVSprite>();
	spr.setAnimation("myAnimation");


Please note:
- Best used with tiled animation and sprites
- The script only support animaton frames on the same line.
- Example scene, texture and it's relative material can be safely deleted.

--------------Changelog------------------------
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