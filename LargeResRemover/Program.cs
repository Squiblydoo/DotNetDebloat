using AsmResolver;
using AsmResolver.DotNet;
using AsmResolver.DotNet.Serialized;
using AsmResolver.DotNet.Builder;

namespace LargeResRemover;

public static class Program
{
	public static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("No input file given.");
			return;
		}

		// Read file.		
		string inputPath = args[0];
		var module = ModuleDefinition.FromFile(
			inputPath,
			new ModuleReaderParameters(EmptyErrorListener.Instance) // optional: but can be good for obfuscated binaries to ignore reader errors.
		);

		// Strip large resources.
		StripLargeResources(module);

		// Save modified file to file.patched.extension.
		string outputPath = Path.ChangeExtension(inputPath, ".patched" + Path.GetExtension(inputPath));
		module.Write(
			outputPath, 
			new ManagedPEImageBuilder(EmptyErrorListener.Instance) // optional: but can be good for obfuscated binaries to ignore writer errors.
		);
	}

	public static void StripLargeResources(ModuleDefinition module)
	{
		const uint SizeThreshold = 1024*1024; // 1MB

		// Loop over all managed resources.
		for (int i = 0; i < module.Resources.Count; i++)
		{
			var resource = module.Resources[i];

			// Measure size of resource data.
			uint resourceSize = resource.EmbeddedDataSegment?.GetPhysicalSize() ?? 0;

			// Remove when too large.
			if (resourceSize > SizeThreshold)
			{
				Console.WriteLine($"Removing large resource {resource.Name} ({resourceSize} bytes)");
				module.Resources.RemoveAt(i);
				i--;
			}
		}
	}
}

