using mwg.Compression;

[afh.Tester.TestTarget]
public static class UnitTestCompress{
	public static void CompressMZipEnc(afh.Application.Log log) {
		log.WriteLine("gzcomp.cs Çà≥èkíÜ ...");
		//System.IO.Stream sread=System.IO.File.OpenRead(@"compress-test\gzcomp.cs");
		//System.IO.Stream swrite=System.IO.File.OpenWrite(@"compress-test\gzcomp.cs.mwg");
		//System.IO.Stream sread=System.IO.File.OpenRead(@"compress-test\test.txt");
		//System.IO.Stream swrite=System.IO.File.OpenWrite(@"compress-test\test.txt.mwg");
		System.IO.Stream sread=System.IO.File.OpenRead(@"compress-test\target.htm");
		System.IO.Stream swrite=System.IO.File.OpenWrite(@"compress-test\target.mwg");
		System.IO.Stream comp=CompressionUtil.MZipCompress(sread);
		afh.File.StreamUtil.PassAll(swrite,comp);
		swrite.Close();
		comp.Close();
		sread.Close();
	}
	public static void CompressMZipDec(afh.Application.Log log){
		log.WriteLine("gzcomp.cs ÇâìÄíÜ ...");
		//System.IO.Stream sread=System.IO.File.OpenRead(@"compress-test\gzcomp.cs.mwg");
		//System.IO.Stream swrite=System.IO.File.OpenWrite(@"compress-test\gzcomp.def.cs");
		//System.IO.Stream sread=System.IO.File.OpenRead(@"compress-test\test.txt.mwg");
		//System.IO.Stream swrite=System.IO.File.OpenWrite(@"compress-test\test.def.txt");
		System.IO.Stream sread=System.IO.File.OpenRead(@"compress-test\target.mwg");
		System.IO.Stream swrite=System.IO.File.OpenWrite(@"compress-test\target.def.htm");
		System.IO.Stream comp=CompressionUtil.MZipDecompress(sread);
		afh.File.StreamUtil.PassAll(swrite,comp);
		swrite.Close();
		comp.Close();
		sread.Close();
	}
}