# Mesh Tools for Unity
A small set of tools for building and manipulating meshes.

## Installation

Unity Editor → Window ▸ Package Manager<br>
➕ Add package from Git URL<br>
https://github.com/Braneloc/MeshTools.git

_Unity downloads the package and recompiles scripts automatically._


## Features

* MeshBuilder - Vertices, Colours, UVs...
* Mesh Batcher

# Usage Batcher
The mesh batcher is in parts.  Add MeshBatcher to any object.
Add BatchingSettings to any object to specify if it should be batched or not.

As this makes changes to the meshes, it might not be a good thing to call it from edit mode, 
unless of course you actally want permanent changes to your meshes and you want to save afterwards.

# Usage Mesh Builder
Sorry, read the code.  Setup the MeshBuilder on a gameobject.  Add vertices and triangles to suit.
Call BuildMesh to finalise and build the mesh.

## Party on dudes  
![](https://avatars.githubusercontent.com/u/9757397?s=96&v=4)
