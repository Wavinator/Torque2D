//-----------------------------------------------------------------------------
// Copyright (c) 2013 GarageGames, LLC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

function MyFirstToy::create( %this )
{
    // Activate the package.
    activatePackage( MyFirstToyPackage );    

    // Initialize the toys settings.
    MyFirstToy.moveTime = 500;

    // Add the custom controls.
    addNumericOption("Move time", 100, 10000, 100, "setMoveTime", MyFirstToy.moveTime, true, "Sets the time it takes to move to the target position.");

    // Reset the toy initially.
    MyFirstToy.reset();        
}

//-----------------------------------------------------------------------------

function MyFirstToy::destroy( %this )
{
    // Deactivate the package.
    deactivatePackage( MyFirstToyPackage );
}

//-----------------------------------------------------------------------------

function MyFirstToy::reset( %this )
{
    // Clear the scene.
    SandboxScene.clear();
    
    // Create background.
    %this.createBackground();

    // Create target.
    %this.createTarget();
    
    // Create sight.
    %this.createSight();
}

//-----------------------------------------------------------------------------

function MyFirstToy::createBackground( %this )
{    
    // Create the sprite.
    %object = new Sprite();
    
    // Set the sprite as "static" so it is not affected by gravity.
    %object.setBodyType( static );
       
    // Always try to configure a scene-object prior to adding it to a scene for best performance.

    // Set the position.
    %object.Position = "0 0";

    // Set the size.        
    %object.Size = "100 75";
    
    // Set to the furthest background layer.
    %object.SceneLayer = 31;
    
    // Set an image.
    %object.Image = "ToyAssets:highlightBackground";
    
    // Set the blend color.
    %object.BlendColor = Bisque;
            
    // Add the sprite to the scene.
    SandboxScene.add( %object );    
}

//-----------------------------------------------------------------------------

function MyFirstToy::createSight( %this )
{
    // Create the sprite.
    %object = new Sprite();
    
    // Set the sight object.
    MyFirstToy.SightObject = %object;
    
    // Set the static image.
    %object.Image = "ToyAssets:Crosshair2";

    // Set the blend color.
    %object.BlendColor = Lime;
    
    // Set the transparency.
    %object.setBlendAlpha( 0.5 );
    
    // Set a useful size.
    %object.Size = 40;
    
    // Set the sprite rotating to make it more interesting.
    %object.AngularVelocity = -90;
    
    // Add to the scene.
    SandboxScene.add( %object );    
}

//-----------------------------------------------------------------------------

function MyFirstToy::createTarget( %this )
{
    // Create the sprite.
    %object = new Sprite();

    // Set the target object.
    MyFirstToy.TargetObject = %object;
    
    // Set the static image.
    %object.Image = "ToyAssets:Crosshair3";
    
    // Set the blend color.
    %object.BlendColor = DarkOrange;
    
    // Set a useful size.
    %object.Size = 20;
        
    // Set the sprite rotating to make it more interesting.
    %object.AngularVelocity = 60;
    
    // Add to the scene.
    SandboxScene.add( %object );    
}

//-----------------------------------------------------------------------------

function MyFirstToy::setMoveTime( %this, %value )
{
    %this.moveTime = %value;
}

//-----------------------------------------------------------------------------

package MyFirstToyPackage
{

function SandboxWindow::onTouchDown(%this, %touchID, %worldPosition)
{
    // Set the target to the touched position.
    MyFirstToy.TargetObject.Position = %worldPosition;
    
    // Move the sight to the touched position.
    MyFirstToy.SightObject.MoveTo( %worldPosition, MyFirstToy.moveTime );
    
    //AJM
    SandboxWindow.startCameraShake( 20, 1 );
}
    
};
