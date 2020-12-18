# Storm Spire

#### Name: Cathal O'Rourke

#### Number: C18408916

#### Course: DT508/TU984

## Project Description

Storm Spire is a Unity created program for visualising music through lightning bolts.
Each bolt is generated with a random pattern upon the music bands hitting a certain amplitude levels and who's colour is determined via the music band.
Lightning generated in the cloud is generated when lightning strikes are not viable and whose colour is generated the same way.

## How to Use
1. Turn on Computer
1. Boot up Unity
1. Make sure sound is turned on
1. Hit play

## How it works
The sky is generated using a custom moving cloud shader on a Quad gameobject.
The fog is generated using HDRP lighting and post processing alongside the bloom effect on the lightning bolts.
The lightning is generated with object pooling and an audio processing function that determines the amplitude of the audio and generates the bolts from that.
The particle effects are achieved with two methods, a particle system using a spherical mesh and an electricity shader, colour changes based on bad. 
The other is achieved with a trail renderer on a gameobject that is moved through the scene very fast to generate the bolt shape.
Everything is variable based, allowing for customisation.

## References
Brackeys Object Pooling
Used in the generation of my prefabs at runtime and recycling them to conserve processing power.

[Object Pooling](https://www.youtube.com/watch?v=tdSmKaJvCoA)

Electricity Shader
Used as a basis for my electricity shader as the one in the tutorial did not react well to changing colour, specifically anything with a 0 R value in RGB colourspace.

[Electricity Shader](https://www.youtube.com/watch?v=u9lOaPVtSqg&list=PLwldfBru-H_X4J2hKR6aSVEo-L3Ip33yY&index=129)

## What I am most proud of

For me, I am most proud of the cloud shader as I made it completely by myself and I think it came out really well and was my first real experience using shader graphs.
I hope to continue to use them in the future too

## YouTube Link

[Storm Spire Link]()

# Previous Proposal

My biggest issue with this assignment was deciding on an idea to use. I'm torn between my last two ideas and want to pursue both for this assignment. The first is a thunderstorm that reacts to music. The lightning eminating on note beats and the colour of which is based on the pitch of the noise with the generation being random. The second idea is a procedurally generated solar system using newtonian physics to orbit a star.

**Thunderstorm Game Idea**

My assignment concept is to make a music visualiser. A simple concept at first but hard to execute in a satisfactory manner. In an effort to make something that really stands out I thought of real life phenomina that shows a variety of colour. From the Aurora Australis and Borealis showing a dazzling display of colour due to heavy solar radiation, waterfall mist and rainbows. All the way to Galactic Nebulas. Finally after a great deal of brainstorming I ended up on my final iteration. A thunderstorm. Despite what many may feel I think a thunderstorm is a great display of natural beauty and colour played out on a dark backdrop highlighting the colour much more.

**Thunderstorm Execution**

In order for me to execute the assignment a few things are needed
1 A murky dark backdrop of sky or city. City could be generated by another students assignment to give a fantastic display for infinite forms once the assignment is complete.
2 A music reader that allows any song to be played and lets the thunderstorm procedurally react in unique ways that are partially randomised so that listening to a repeating song is never quite the same visually.
3 A randomised thunder effect
4 Randomised lighting effects

**Solar System Game Idea**

My second idea is to create a randomly generated solar system, creating a stable orbit using Newtonian physics and algorithms. Each planet will also be randomly generated with a basic texture and size and mass ratio. Due to the sheer scale of a solar system though the size shall be altered to be viewable from a stationary camera.

**Solar System Execution**

I shall execute the idea using newtonian equations for gravity and orbits. Each planet will have a randomly generated orbit path, mass and position. The entertainment will come from watching the orbits slowly intersect and create emergence. As the planets slowly decay in orbit due to gravitational influence from each other they will collide, exploding and creating more masses to watch orbit.
