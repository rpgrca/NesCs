## Tests status

Current status of tests being executed in the integration testing section of the pipeline.

| Test | Status | Notes |
| - | - | - |
| cpu_dummy_writes/cpu_dummy_writes_ppumem.nes | Ok |  |
| cpu_dummy_writes/cpu_dummy_writes_oam.nes | Ok | |
| cpu_dummy_reads/cpu_dummy_reads.nes | Fail | No output but hangs at 0xE60F which is error in fceux |
| cpu_exec_space/test_cpu_exec_space_ppuio.nes | Ok | |
| cpu_exec_space/test_cpu_exec_space_apu.nes | Fail | Must implement APU, landed at $0234, fail to obey predetermined path #2 |
| cpu_reset/ram_after_reset.nes | Ok | |
| cpu_reset/registers.nes | Ok | |
| cpu_timing_test6/cpu_timing_test.nes | Fail | No output, should implement text output |
| dmc_tests/buffer_retained.nes | Ok | Reaches end with same registers as fceux |
| dmc_tests/latency.nes | Ok | Reaches end with same registers as fceux |
| dmc_tests/status.nes | Fail | Reaches end with different registers as fceux |
| dmc_tests/status_irq.nes | Ok | Reaches end with same registers as fceux |
| instr_misc/rom_singles/01-abs_x_wrap.nes | Ok | |
| instr_misc/rom_singles/02-branch_wrap.nes | Ok | |
| instr_misc/rom_singles/03-dummy_reads.nes | Fail | STA abs,x 03-dummy_reads Failed #4 |
| instr_test-v3/rom_singles/01-implied.nes | Ok | |
| instr_test-v3/rom_singles/02-immediate.nes | Fail | Looks like 0x0B must be implemented |
| instr_test-v3/rom_singles/03-zero_page.nes | Ok | |
| instr_test-v3/rom_singles/04-zp_xy.nes| Ok | |
| instr_test-v3/rom_singles/05-absolute.nes | Ok | |
| instr_test-v3/rom_singles/06-abs_xy.nes | Fail | Crash, or must implement 0x9C |
| instr_test-v3/rom_singles/07-ind_x.nes | Ok | |
| instr_test-v3/rom_singles/08-ind_y.nes | Ok | |
| instr_test-v3/rom_singles/09-branches.nes | Ok | |
| instr_test-v3/rom_singles/10-stack.nes | Ok | |
| instr_test-v3/rom_singles/11-jmp_jsr.nes | Ok | |
| instr_test-v3/rom_singles/12-rts.nes | Ok | |
| instr_test-v3/rom_singles/13-rti.nes | Ok | |
| instr_test-v3/rom_singles/14-brk.nes | Ok | |
| instr_test-v3/rom_singles/15-special.nes | Ok | |
| instr_test-v5/rom_singles/01-basics.nes | Ok | |
| instr_test-v5/rom_singles/02-implied.nes | Ok | |
| instr_test-v5/rom_singles/03-immediate.nes | Fail | Looks like 0x0B must be implemented |
| instr_test-v5/rom_singles/04-zero_page.nes | Ok | |
| instr_test-v5/rom_singles/05-zp_xy.nes | Ok | |
| instr_test-v5/rom_singles/06-absolute.nes | Ok | |
| instr_test-v5/rom_singles/07-abs_xy.nes | Fail | Crash, or must implement 0x9C |
| instr_test-v5/rom_singles/08-ind_x.nes | Ok | |
| instr_test-v5/rom_singles/09-ind_y.nes | Ok | |
| instr_test-v5/rom_singles/10-branches.nes | Ok | |
| instr_test-v5/rom_singles/11-stack.nes | Ok | |
| instr_test-v5/rom_singles/12-jmp_jsr.nes | Ok | |
| instr_test-v5/rom_singles/13-rts.nes | Ok | |
| instr_test-v5/rom_singles/14-rti.nes | Ok | |
| instr_test-v5/rom_singles/15-brk.nes | Ok | |
| instr_test-v5/rom_singles/16-special.nes | Ok | |
| oam_read/oam_read.nes | Ok | |
| oam_stress/oam_stress.nes | Ok | |
| ppu_open_bus/ppu_open_bus.nes | Ok | |
| ppu_read_buffer/test_ppu_read_buffer.nes | Fail | Should implement graphics, hangs after displaying Testing basic PPU memory I/O. |
| ppu_vbl_nmi/rom_singles/01-vbl_basics.nes | Fail | Shows error 4, can't pass |
| instr_timing/rom_singles/1-instr_timing.nes | Fail | Does nothing after displaying Takes about 25 seconds. Doesn't time the 8 branches and 12 illegal instructions. |
| branch_timing_tests/1.Branch_Basics.nes | Fail | No output at all |
| branch_timing_tests/2.Backward_Branch.nes | Fail | No output at all |
| branch_timing_tests/3.Forward_Branch.nes | Fail | No output at all |
| dmc_dma_during_read4/dma_2007_write.nes | Fail | Loops at BIT 0x10 / BNE |
| dmc_dma_during_read4/dma_2007_read.nes | Fail | Loops at BIT 0x10 / BNE |
| dmc_dma_during_read4/double_2007_read.nes | Fail | Loops at STA 0, LDA 0, JMP |
| sprdma_and_dmc_dma/sprdma_and_dmc_dma.nes | Fail | Must implement dma, shows only first line |
| vbl_nmi_timing/1.frame_basics.nes | Fail | No output at all |
