## Integration tests

In order to execute the integration tests you need to clone the [ProcessorTests](https://github.com/TomHarte/ProcessorTests.git) repository from [Tom Hart](https://github.com/TomHarte) one directory above this project (that is, clone it outside this project), otherwise the base path for the files will have to be redefined in [ProcessorFileTestDataAttribute.cs](../NesCs.Tests.Common/ProcessorFileTestDataAttribute.cs#L11) will have to be redefined.

Only the files found inside [nes6502](https://github.com/TomHarte/ProcessorTests/tree/main/nes6502) are required.
