using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace MovieCatalog
{
    internal sealed class CrossAssemblyBinder : SerializationBinder
    {
        private readonly Assembly _currentAsm = typeof(Movie).Assembly;
        private readonly string _currentAsmName;

        public CrossAssemblyBinder()
        {
            _currentAsmName = _currentAsm.FullName!;
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            if (typeName.StartsWith("MovieCatalog.Movie", StringComparison.Ordinal))
                return _currentAsm.GetType("MovieCatalog.Movie")!;

            if (typeName.StartsWith("System.Collections.Generic.List", StringComparison.Ordinal) &&
                typeName.Contains("MovieCatalog.Movie", StringComparison.Ordinal))
            {
                return typeof(List<Movie>);
            }

            return Type.GetType($"{typeName}, {assemblyName}", throwOnError: true)!;
        }
    }
}
