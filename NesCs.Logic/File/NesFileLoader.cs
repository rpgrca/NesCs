namespace NesCs.Logic.File;

public class NesFileLoader
{
    public static INesFile CreateFrom(string filename, byte[] contents, NesFileOptions options)
    {
        if (contents.Length < 16)
        {
            throw new ArgumentException("Could not find header", nameof(contents));
        }

        if ((contents[7] & 0xC) == 0x8)
        {
            /* TODO: Check that byte 9 does not exceed actual size of ROM image */
            return new Nes20File(filename, contents, options);
        }

        if ((contents[7] & 0xC) == 0x4)
        {
            return new ArchaicINesFile(filename, contents, options);
        }

        if ((contents[7] & 0xC) == 0x0)
        {
            if (contents[12] == 0 && contents[13] == 0 && contents[14] == 0 && contents[15] == 0)
            {
                return new OriginalINesFile(filename, contents, options);
            }
        }

        return new ArchaicINesFile(filename, contents, options);
    }

    public static INesFile CreateFrom(string filename, byte[] contents)
    {
        var options = new NesFileOptions
        {
            LoadHeader = true
        };

        return CreateFrom(filename, contents, options);
    }
}