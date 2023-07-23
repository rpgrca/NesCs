namespace NesCs.Logic.File;

public class NesFileLoader
{
    public static INesFile CreateFrom(string filename, byte[] contents)
    {
        if (contents.Length < 16)
        {
            throw new ArgumentException("Could not find header", nameof(contents));
        }

        if ((contents[7] & 0xC) == 0x8)
        {
            /* TODO: Check that byte 9 does not exceed actual size of ROM image */
            return new Nes20File(filename, contents);
        }

        if ((contents[7] & 0xC) == 0x4)
        {
            return new ArchaicINesFile(filename, contents);
        }

        if ((contents[7] & 0xC) == 0x0)
        {
            if (contents[12] == 0 && contents[13] == 0 && contents[14] == 0 && contents[15] == 0)
            {
                return new OriginalINesFile(filename, contents);
            }
        }

        return new ArchaicINesFile(filename, contents);
    }
}