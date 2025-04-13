# 🍈 MeronPan Time 🍈

A 3D dynamic wallpaper application featuring animated meronpan (Japanese melon bread) with ambient music.

![Meronpan Time](Assets/Media/readme_logo.png)

## 🍞 Overview

MeronPan Time is a relaxing 3D dynamic wallpaper that creates a soothing atmosphere with floating melon bread objects and ambient background patterns. The application includes a custom soundtrack that enhances the calming experience.

## 🎮 Features

- 🍈 3D animated meronpan (melon bread) objects that float, rotate, and move throughout the scene
- 🖼️ Dynamic background patterns that can be toggled between different textures
- 🎵 Ambient music ("Watermelon Flava") that complements the visual elements
- 🎛️ Simple controls to customize your experience

## 🕹️ Controls

- **Space**: Toggle between the meronpan pattern and shop background
- More controls can be implemented in future updates

## 🔧 Technical Details

You will find here the source code for the project and the assets. The app can be downloaded from [itch.io](https://greencube1.itch.io/meronpan-time) 🌍.

### 📝 C# Scripts

The project includes two main C# scripts that handle the core functionality:

#### 1. BackgroundPattern.cs

This script manages the scrolling background texture:

- Controls the movement of a tiled texture using UV offsetting
- Allows switching between two different background textures (meronpan pattern and shop)
- Manages texture tiling and scaling for different visual effects
- Resets texture offset when switching between patterns

```csharp
// Key functionality
void MoveUVTexture()
{
    // move UV texture, use delta time
    GetComponent<Renderer>().material.mainTextureOffset += 
        new Vector2(moveSpeed * moveDirection.x, moveSpeed * moveDirection.y) * Time.deltaTime;
}
```

#### 2. MeronPan2_particleSystem.cs

This script implements a custom particle system using prefab instances:

- Spawns 3D meronpan objects within a defined area
- Controls the number, scale, and lifetime of objects
- Manages continuous rotation with random axes and speeds
- Handles object movement and trajectory
- Provides editor visualization for easy configuration

```csharp
// Object spawning and management
void SpawnPrefab()
{
    // Calculate spawn position within the area
    float halfWidth = spawnAreaWidth / 2f;
    float halfLength = spawnAreaLength / 2f;
    
    float randomX = Random.Range(-halfWidth, halfWidth);
    float randomZ = Random.Range(-halfLength, halfLength);
    
    Vector3 localSpawnPosition = new Vector3(randomX, height, randomZ);
    Vector3 worldSpawnPosition = transform.TransformPoint(localSpawnPosition);
    
    // [Additional spawning code...]
}
```

## 📁 Project Structure

The project follows a standard Unity structure with assets organized into the following directories:

- 🖼️ **Assets/Media**: Contains background images, icons, and logos
- 🍈 **Assets/Models**: Includes 3D models for meronpan and background textures
- 🎵 **Assets/Music**: Stores the ambient soundtrack
- 🎬 **Assets/Scenes**: Contains the main scene configuration
- 📝 **Assets/Scripts**: Houses the C# scripts for functionality
- ⚙️ **Assets/Settings**: Includes Unity render pipeline settings

## 👏 Credits

- **Music**: "Watermelon Flava" by djpretzel from The Overlocked Remix. All rights reserved.

## License

This project is for personal use. All media assets and code are proprietary unless otherwise noted.

---

*🍈 MeronPan Time - Bringing the cozy atmosphere of freshly baked melon bread to your desktop! 🍞*