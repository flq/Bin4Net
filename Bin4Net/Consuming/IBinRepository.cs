namespace Bin4Net.Consuming
{
  public interface IBinRepository
  {
      /// <summary>
      /// Get a binary package by providing a path of publisher/product/version.
      /// </summary>
      Bin Get(string binPath);
  }
}