using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace MovieCatalog
{
    /// <summary>
    /// Подменяет типы Movie и List&lt;Movie&gt;, чтобы файл,
    /// записанный любой сборкой, читалcя нынешней.
    /// </summary>
    internal sealed class CrossAssemblyBinder : SerializationBinder
    {
        private readonly Assembly _asm = typeof(Movie).Assembly;

        public override Type BindToType(string assemblyName, string typeName)
        {
            if (typeName.StartsWith("MovieCatalog.Movie", StringComparison.Ordinal))
                return _asm.GetType("MovieCatalog.Movie")!;

            if (typeName.StartsWith("System.Collections.Generic.List", StringComparison.Ordinal) &&
                typeName.Contains("MovieCatalog.Movie", StringComparison.Ordinal))
                return typeof(List<Movie>);

            return Type.GetType($"{typeName}, {assemblyName}", throwOnError: true)!;
        }
    }
}
