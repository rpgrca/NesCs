## C64 Wolfgang Lorenz Test Suite

This test suite was created by Wolfgang Lorenz to test the C64 6502/6510 instructions. As the NES cpu was a 6502 (without decimal support) some of these tests can be run correctly.

The source code for these tests can be found at Tom Seddon's repository [here](https://github.com/tom-seddon/lorenz-testsuite-beeb/tree/main/ascii-src).

I added the files from testsuite-2.14.tar.gz (which can be found [here](http://www.zimmers.net/anonftp/pub/cbm/crossplatform/emulators/pc64/)) inside the [Resources/](./Resources/) directory and made them uppercase (as in Linux they are case-sensitive when loading).

## Executing the tests

Simply run it with _dotnet run_ and the name of the file. If the test is successful the name of the test and "OK" will appear on screen.

If the test failed it will show the current and the expected output.

As the C64 uses carriage return (character 13) for new lines (character 10) (and I'm developing under Linux) I change it to new line to prevent overwriting the output again and again.