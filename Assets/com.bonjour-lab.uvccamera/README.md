# Unity3D-Camera

Simple package to grab UVC camera feed.

### How to use
* Create a RenderTexture to the size of your camera
* Add ````WebcamInput.cs``` to your scene and defines its parameters

### Install Package

This package uses the scoped registry feature to import dependent packages.
 Please add the following sections to the package manifest file (Packages/manifest.json).

To the scopedRegistries section:
```
{
    "name": "Bonjour-lab",
    "url": "https://registry.npmjs.com",
    "scopes": [
    "com.bonjour-lab"
    ]
}
```

To the dependencies section:

```
"com.bonjour-lab.uvccamera": "1.0.2",
```

After changes, the manifest file should look like below:
```
{
  "scopedRegistries": [
    {
      "name": "Bonjour-lab",
      "url": "https://registry.npmjs.com",
      "scopes": [
        "com.bonjour-lab"
      ]
    }
  ],
  "dependencies": {
    "com.bonjour-lab.uvccamera": "1.0.2",
    ...
```
