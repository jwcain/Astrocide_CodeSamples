# SpriteImporter

## CODE AUTHOR
- Justin Cain 
- @AffinityForFun
- jwcain@mtu.edu
- jwcain.github.io/Portfolio
	
## STRUCTURE
There are two files, "SpriteImporter" that provides the functionality for the tool and "SpriteImporterEditor" which exposes the functionality within the UnityEditor.
	
## DESIGN
The SpriteImporter creates game objects built out of voxels (3D cube representations of pixels). It has code for generating a hollow circle, a filled cube, and convert a from a sprite. Sprite conversion is done by checking the alpha value of each pixel. If the section is not transparent, a voxel is placed in that space.
