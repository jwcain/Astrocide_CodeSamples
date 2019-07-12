# SpriteImporter

## Author
- Justin W. Cain 
- @AffinityForFun
- jwcain@mtu.edu
- [Portfolio](https://jwcain.github.io/Portfolio/)
	
## Structure
There are two files, "SpriteImporter" that provides the functionality for the tool and "SpriteImporterEditor" which exposes the functionality within the UnityEditor.
	
## Design
The SpriteImporter creates game objects built out of voxels (3D cube representations of pixels). It has code for generating a hollow circle, a filled cube, and convert a from a sprite. Sprite conversion is done by checking the alpha value of each pixel. If the section is not transparent, a voxel is placed in that space.
