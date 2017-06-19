namespace EasyPdfWriter.Security
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  ///   The Guard class.
  /// </summary>
  public static class Guard
  {
    /// <summary>
    ///   Guards the given object against null.
    /// </summary>
    /// <param name="obj">The object to use.</param>
    /// <param name="message">The message to display.</param>
    public static void AgainstNull(object obj, string message)
    {
      if (obj == null)
      {
        throw new ArgumentNullException(nameof(obj), message);
      }
    }

    /// <summary>
    /// Guards the given collection against an empty collection.
    /// </summary>
    /// <typeparam name="T">The type of an item in the given sequence.</typeparam>
    /// <param name="sequence">The sequence to check.</param>
    /// <param name="message">The message to display.</param>
    public static void AgainstEmpty<T>(IEnumerable<T> sequence, string message)
    {
      Guard.AgainstNull(sequence, message);

      if (!sequence.Any())
      {
        throw new ArgumentNullException(nameof(sequence), message);
      }
    }
  }
}