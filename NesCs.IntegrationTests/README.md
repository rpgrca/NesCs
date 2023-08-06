## Integration tests

In order to execute the integration tests you need to clone the [ProcessorTests](https://github.com/TomHarte/ProcessorTests.git) repository from [Tom Hart](https://github.com/TomHarte) one directory above this project (that is, clone it outside this project), otherwise the base path for the files will have to be redefined in [ProcessorFileTestDataAttribute.cs](../NesCs.Common.Tests/ProcessorFileTestDataAttribute.cs#L11) will have to be redefined.

Only the files found inside [nes6502](https://github.com/TomHarte/ProcessorTests/tree/main/nes6502) are required. Note that cloning that repository might require up to 11gb of space.

For every implemented instructions 10000 different combinations are loaded from disk and tested. As this is slow it should only be tested a few times per day if at all (as the unit tests already have a few samples of these scenarios per instruction). In my computer 2_310_000 tests (231 instructions, 25 were not required) take 1 m 26 s but your mileage may vary.
