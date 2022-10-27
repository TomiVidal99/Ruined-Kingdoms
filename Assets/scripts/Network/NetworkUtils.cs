using Steamworks;
using UnityEngine;

/// <summary>
/// Set of utity methods to help with network
/// </summary>
public class NetworkUtils : MonoBehaviour
{
    /// <summary>
    /// Transform a steam image to a texture
    /// </summary>
    /// <param name="iImage">Image from steamworks API</param>
    public Texture2D GetSteamImageAsTexture(int iImage)
    {
      Texture2D texture = null;

      bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
      if (isValid)
      {
        byte[] image = new byte[width * height * 4];

        isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));
        if (isValid)
        {
          texture = new Texture2D( (int)width, (int)height, TextureFormat.RGBA32, false, true);
          texture.LoadRawTextureData(image);
          texture.Apply();
        }

      }
      return texture;
    }
}
