## C64 Wolfgang Lorenz Test Suite

This test suite was created by Wolfgang Lorenz to test the C64 6502/6510 instructions. As the NES cpu was a 6502 (without decimal support) some of these tests can be run correctly.

The source code for these tests can be found at Tom Seddon's repository [here](https://github.com/tom-seddon/lorenz-testsuite-beeb/tree/main/ascii-src).

I added the files from testsuite-2.14.tar.gz (which can be found [here](http://www.zimmers.net/anonftp/pub/cbm/crossplatform/emulators/pc64/)) inside the [Resources/](./Resources/) directory and made them uppercase (as in Linux file names are case-sensitive).

## Executing the tests

Simply run it with _dotnet run_ and the name of the file. If the test is successful the name of the test and "OK" will appear on screen.

![image](https://github.com/rpgrca/NesCs/assets/15602473/a520f574-ca45-417e-8bf9-0bddac4e681c)

If the test failed it will show the current and the expected output.

![image](https://github.com/rpgrca/NesCs/assets/15602473/5ffde6a0-2a1a-41ad-bae2-c5e432a626f3)

### Notes
- As the C64 uses carriage return (byte 13) instead of new line (in fact it looks like PETSCII does not have byte 10 at all) (and I'm developing under Linux) I change it to new line to prevent overwriting the output again and again. The tests also use "cursor up" (character 145 in PETSCII) which is supposed to move the cursor once, which would prevent extra lines. As I'm simply casting the values and not using the recommended font for displaying, C# just ignores it.
- Tests are not thought to be ran under a NES emulator so several fail, crash or hang. However as so far I only had the instruction set implemented I wanted something "visual".
- Running these tests require [some setup](http://www.softwolves.com/arkiv/cbm-hackers/7/7114.html). I updated the CPU to be able to accept them.
