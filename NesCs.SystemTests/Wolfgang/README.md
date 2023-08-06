## C64 Wolfgang Lorenz Test Suite

This test suite was created by Wolfgang Lorenz to test the C64 6502/6510 instructions. The test suite itself is public domain. As the NES cpu was a 6502 (without decimal support) some of these tests can be run correctly.

The source code for these tests can be found at Tom Seddon's repository [here](https://github.com/tom-seddon/lorenz-testsuite-beeb/tree/main/ascii-src).

I added the files from testsuite-2.14.tar.gz (which can be found [here](http://www.zimmers.net/anonftp/pub/cbm/crossplatform/emulators/pc64/)) inside the [Resources/](./Resources/) directory and made them uppercase (as in Linux file names are case-sensitive).

## Executing the tests

Simply run it with _dotnet run_ and the name of the file. If the test is successful the name of the test and "OK" will appear on screen.

![image](https://github.com/rpgrca/NesCs/assets/15602473/a520f574-ca45-417e-8bf9-0bddac4e681c)

If the test failed it will show the current and the expected output.

![image](https://github.com/rpgrca/NesCs/assets/15602473/5ffde6a0-2a1a-41ad-bae2-c5e432a626f3)

### Notes
- C64 uses a up/gfx, lo/up mode to display graphics and text. There's no equivalent in ASCII so I wrote a simple method to convert lowercase and uppercase characters and return the original value when not in range. This is enough to display results on screen.
- Tests are not thought to be ran under a NES emulator so several fail, crash or hang. However as so far I only had the instruction set implemented I wanted something "visual".
- Running these tests require [some setup](http://www.softwolves.com/arkiv/cbm-hackers/7/7114.html). I updated the CPU to be able to accept them.

Following is a non-exhaustive list of results obtained from running every test. _Ok_ means the test finished and the result read from screen displays "Ok". _Fail_ means the test completed, execution finished with an apparent error message (as there's no difference between quitting in error or in success). _Crash_ means the application finished by executed code that was not supposed to (usually because it failed to take the right path or reached an unimplemented instruction, in this case I mention the instruction when it's obvios, aka when the cycle counter is below 300 at the moment of crashing). _Freeze_ is similar to _Crash_ except that the application entered in an infinite loop either displaying something on screen or not and must be interrupted. A few test execute the first half correctly, then load another file and freezes when executing the loaded data.

Although I don't intend to write a C64 emulator (for the time being) and thus I'm not likely to correct these mistakes I leave this as future reference of the current status of the CPU. 136 tests pass and 23 others fail making it 159 tests being able to execute the final instruction correctly (regardless of result).

| Instruction | Status      | Notes                                 |
| ----------- | ----------- | ------------------------------------- |
| adca | Fail | Carry flag not set |
| adcax | Fail | Carry flag not set |
| adcay | Fail | Carry flag not set |
| adcb | Fail | Carry flag not set |
| adcix | Fail | Carry flag not set |
| adciy | Fail | Carry flag not set |
| adcz | Fail | Carry flag not set |
| adczx | Fail | Carry flag not set |
| alrb | Crash | |
| ancb | Crash | |
| anda | Ok | |
| andax | Ok | |
| anday | Crash | |
| andb | Ok | |
| andix | Ok | |
| andiy | Crash | |
| andz | Ok | |
| andzx | Crash | Requires decimal point |
| aneb | Crash | |
| arrb | Crash | |
| asla | Ok | |
| aslax | Ok | |
| asln | Ok | |
| aslz | Ok | |
| aslzx | Ok | |
| asoa | Freeze | |
| asoax | Crash | |
| asoay | Ok | |
| asoix | Ok | |
| asoiy | Ok | |
| asoz | Ok | |
| asozx | Ok | |
| axsa | Ok | |
| axsix | Freeze | |
| axsz | Ok | |
| axszy | Ok | |
| bccr | Crash | |
| bcsr | Crash | |
| beqr | Freeze | |
| bita | Ok | |
| bitz | Crash | |
| bmir | Freeze | |
| bner | Freeze | |
| bplr | Ok | |
| branchwrap | Freeze | |
| brkn | Crash | |
| bvcr | Freeze | |
| bvsr | Freeze | |
| clcn | Ok | |
| cldn | Ok | |
| clin | Ok | |
| clvn | Freeze | |
| cmpa | Fail | Decimal flag not set |
| cmpax | Freeze | Executes first half correctly |
| cmpay | Ok | |
| cmpb | Ok | |
| cmpix | Freeze | |
| cmpiy | Ok | |
| cmpz | Crash | |
| cmpzx | Ok | |
| cpuport | Fail | |
| cputiming | Freeze | |
| cpxa | Ok | |
| cpxb | Ok | |
| cpxz | Freeze | |
| cpya | Ok | |
| cpyb | Ok | |
| cpyz | Freeze | |
| dcma | Freeze | |
| dcmax | Freeze | |
| dcmay | Ok | |
| dcmix | Ok | |
| dcmiy | Ok | |
| dcmz | Ok | |
| dcmzx | Ok | |
| deca | Ok | |
| decax | Ok | |
| decz | Ok | |
| deczx | Ok | |
| dexn | Ok | |
| deyn | Ok | |
| eora | Ok | |
| eorax | Crash | |
| eoray | Freeze | Executes first half correctly |
| eorb | Ok | |
| eorix | Ok | |
| eoriy | Crash | |
| eorz | Ok | |
| eorzx | Ok | |
| inca | Ok | |
| incax | Ok | |
| incz | Ok | |
| inczx | Ok | |
| insa | Freeze | |
| insax | Freeze | |
| insay | Ok | |
| insix | Ok | |
| insiy | Crash | Executes first half correctly |
| insz | Ok | |
| inszx | Ok | |
| inxn | Ok | |
| inyn | Ok | |
| jmpi | Ok | |
| jmpw | Ok | |
| jsrw | Ok | |
| lasay | Crash | |
| laxa | Ok | |
| laxay | Ok | |
| laxix | Ok | |
| laxiy | Ok | |
| laxz | Ok | |
| laxzy | Ok | |
| ldaa | Freeze | |
| ldaax | Freeze | |
| ldaay | Ok | |
| ldab | Freeze | |
| ldaix | Freeze | |
| ldaiy | Ok | |
| ldaz | Ok | |
| ldazx | Crash | |
| ldxa | Ok | |
| ldxay | Freeze | |
| ldxb | Ok | |
| ldxz | Ok | |
| ldxzy | Freeze | |
| ldya | Ok | |
| ldyax | Freeze | |
| ldyb | Ok | |
| ldyz | Ok | |
| ldyzx | Freeze | |
| lsea | Ok | |
| lseax | Ok | |
| lseay | Freeze | |
| lseix | Ok | |
| lseiy | Freeze | |
| lsez | Ok | |
| lsezx | Ok | |
| lsra | Ok | |
| lsrax | Ok | |
| lsrn | Crash | |
| lsrz | Ok | |
| lsrzx | Ok | |
| lxab | Crash | |
| mmu | Fail | MMU not implemented at all |
| mmufetch | Freeze | |
| nopa | Ok | |
| nopax | Freeze | |
| nopb | Ok | |
| nopn | Ok | |
| nopz | Ok | |
| nopzx | Ok | |
| oraa | Ok | |
| oraax | Ok | |
| oraay | Crash | |
| orab | Ok | |
| oraix | Ok | |
| oraiy | Ok | |
| oraz | Ok | |
| orazx | Freeze | |
| phan | Ok | |
| phpn | Ok | |
| plan | Ok | |
| plpn | Ok | |
| rlaa | Ok | |
| rlaax | Ok | |
| rlaay | Crash | |
| rlaix | Ok | |
| rlaiy | Crash | |
| rlaz | Ok | |
| rlazx | Ok | |
| rola | Crash | |
| rolax | Ok | |
| roln | Ok | |
| rolz | Freeze | Executes first half correctly |
| rolzx | Ok | |
| rora | Ok | |
| rorax | Freeze | |
| rorn | Ok | |
| rorz | Ok | |
| rorzx | Ok | |
| rraa | Crash | |
| rraax | Fail | Executes first half correctly, then fails to set Negative and Carry flags |
| rraay | Ok | |
| rraix | Crash | |
| rraiy | Freeze | |
| rraz | Ok | |
| rrazx | Freeze | |
| rtin | Ok | |
| rtsn | Ok | |
| sbca | Fail | Result should be 0x99 instead of 0xFF |
| sbcax | Fail | Result should be 0x99 instead of 0xFF |
| sbcay | Fail | Result should be 0x99 instead of 0xFF |
| sbcb | Fail | Result should be 0x99 instead of 0xFF |
| sbcb(eb) | Fail | Result should be 0x99 instead of 0xFF |
| sbcix | Fail | Result should be 0x99 instead of 0xFF |
| sbciy | Fail | Result should be 0x99 instead of 0xFF |
| sbcz | Fail | Result should be 0x99 instead of 0xFF |
| sbczx | Fail | Result should be 0x99 instead of 0xFF |
| sbxb | Crash | Unimplemented $CB _SBX #imm_ |
| secn | Ok | |
| sedn | Ok | |
| sein | Ok | |
| shaay | Crash | Unimplemented $9F _SHA abs, y_ |
| shaiy | Crash | Unimplemented $93 _SHA (zp), y_ |
| shsay | Crash | Unimplemented $9B _TAS abs, y_ |
| shxay | Crash | Unimplemented $9E _SHX abs, y_ |
| shyax | Crash | Unimplemented $9C _SHY abs, x_ |
| staa | Ok | |
| staax | Ok | |
| staay | Ok | |
| staix | Ok | |
| staiy | Ok | |
 | start | Freeze | Starts correctly, then freezes |
| staz | Ok | |
| stazx | Ok | |
| stxa | Ok | |
| stxz | Ok | |
| stxzy | Ok | |
| stya | Ok | |
| styz | Ok | |
| styzx | Ok | |
| taxn | Ok | |
| tayn | Ok | |
| trap1 | Crash | |
| trap10 | Crash | |
| trap11 | Crash | |
| trap12 | Crash | |
| trap13 | Crash | |
| trap14 | Crash | |
| trap15 | Crash | |
| trap16 | Crash | |
| trap17 | Crash | |
| trap2 | Crash | |
| trap3 | Crash | |
| trap4 | Crash | |
| trap5 | Crash | |
| trap6 | Crash | |
| trap7 | Crash | |
| trap8 | Crash | |
| trap9 | Crash | |
| tsxn | Freeze | |
| txan | Ok | |
| txsn | Ok | |
| tyan | Ok | |
| cia1ta | Freeze | |
| cia1tb | Freeze | |
| cia1tb123 | Freeze | |
| cia2ta | Freeze | |
| cia2tb | Freeze | |
| cia2tb123 | Freeze | |
| cpuport3 | Freeze | |
| irq | Fail | IRQ vector not set up |
| nmi | Fail | NMI vector not set up |
| rstrstck.s00 | Freeze | Incompatible format |
| wtdsplys.s00 | Freeze | Incompatible format |
