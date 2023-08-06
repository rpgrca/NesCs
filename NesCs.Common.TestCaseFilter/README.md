## Test case picker

This program is used to select a few test cases from Tom Harte's [Processor Tests](https://github.com/TomHarte/ProcessorTests/tree/main/nes6502) for the NES. Simply execute it passing the file from where generate a sample and it will output a static method that can be used for a _MemberData_ attribute in Xunit.

The selection itself is purely based in flag changes between the initial setup and the final result of the scenario as usually those are the most complex situations. The cases selected by this program are the ones I used to develop the solution. Later I would execute integration tests against the full 10000 cases and if there were special cases that were not taken into account (mostly address wrapping) I manually add them to the method in the unit test.
