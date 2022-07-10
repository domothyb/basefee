# Historical base fee and gas usage

Neatly packaged into csv files of 100k blocks each with the basefee indicated in weis. I plan to sporadically update with new CSVs, mostly for my own purposes.

Data was extracted with a simple [Nethermind RPC plugin](https://docs.nethermind.io/nethermind/ethereum-client/plugins):

* Clone and build [Nethermind](https://github.com/NethermindEth/nethermind)

* Copy `BaseFeePlugin/` in `src/Nethermind`

* Edit `BaseFeePlugin/mainnet.cfg` to add your node's Nethermind db directory

* Execute `run.bat` or `run.sh`

* Wait for the node to fully sync if you don't already have a sync'd nethermind node

* If all works well, you have a Nethermind instance running that exposes custom RPC endpoints:

  * `basefee_writeDataToFile(path, fromBlockNumber, numberOfBlocks)` - Generates a CSV file

  * `basefee_fetchData(fromBlockNumber, numberOfBlock)` - Returns an array of 