## Nestest

_Nestest_ is a rom created to test a NES emulator. It can be run in both interactive (starting at 0xC004) or batch (0xC000) mode. The ROM will execute several tests and will return in memory addresses 0x02 and 0x03 the [last error code](http://www.qmtpro.com/~nes/misc/nestest.txt) found, or 0 if successful.

As by the time I wanted to run this program I didn't have visuals done I had to implement every illegal instruction it also tested (as it's not possible to skip tests in batch mode). It also reaches the final RTS according to the [golden log](https://www.qmtpro.com/~nes/misc/nestest.log) although it doesn't count cycles still (and without a PPU the count will never be exact anyway).

To run the program execute _dotnet run_ with the path to the nestest.nes ROM. It should display some information regarding the ROM and then determine whether the run ended correctly or failed.

![image](https://github.com/rpgrca/NesCs/assets/15602473/e2c47300-659e-42a8-a217-01dd87ac60cf)

### Current status ###
As of this time the CPU emulation matches the _golden log_ perfectly, including cycle count and PPU timing (still without graphics). The initial status of the flags does not match (which is not necessary because NES starts with P in 0x34 differing in the irrelevant X and D flags). Initial setup is still not done but run is delayed by 7 cycles to match initialization code.