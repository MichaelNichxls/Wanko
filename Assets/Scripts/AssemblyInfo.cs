#if NETFRAMEWORK || NETSTANDARD
using System.Runtime.InteropServices;

[module: DefaultCharSet(CharSet.Unicode)]
#if NET45_ORLATER || NETSTANDARD
[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
#endif
#endif