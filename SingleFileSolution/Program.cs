using AsmResolver;
using AsmResolver.DotNet;
using AsmResolver.DotNet.Serialized;
using AsmResolver.DotNet.Builder;
using AsmResolver.DotNet.Bundles;
using System.Text;

namespace SingleFileSolution;

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

		var bundle = new BundleFile(inputPath);
		var manifest = BundleManifest.FromFile( inputPath );
		var bundle_relativePath = manifest.Files[0].RelativePath;


		foreach (var file in manifest.Files)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string subdirectory = fileName;
            string path = Path.Combine(subdirectory, file.RelativePath);
            byte[] contents = file.GetData();

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            Console.WriteLine($"Extracting {path} {contents.Length} bytes");
            System.IO.File.WriteAllBytes(path, contents);
        
		}


        Console.ReadKey();

    }

}

