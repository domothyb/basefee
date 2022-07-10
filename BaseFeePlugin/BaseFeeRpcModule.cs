using System.IO;
using Nethermind.Blockchain;
using Nethermind.Blockchain.Find;
using Nethermind.Core;
using Nethermind.Int256;
using Nethermind.JsonRpc;
using Nethermind.JsonRpc.Modules;
using Nethermind.Logging;

namespace BaseFeePlugin
{
    public class BaseFeeRpcModule : IBaseFeeRpcModule
    {
        private readonly IBlockTree _blockTree;
        private ILogger _logger;

        public BaseFeeRpcModule(IBlockTree blockTree, ILogManager logManager)
        {
            _blockTree = blockTree;
            _logger = logManager.GetClassLogger();
        }

        public ResultWrapper<string> basefee_writeDataToFile(string path, long fromBlockNumber, long numberOfBlocks)
        {
            if (File.Exists(path))
                return ResultWrapper<string>.Fail($"Path {path} already exists");
            if(fromBlockNumber < 12965000)
                return ResultWrapper<string>.Fail($"No base fee data before London hard fork (Block 12,965,000)");

            long number = fromBlockNumber;

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Block number,Time,Gas used,Gas limit,Base fee per gas");
                while (number < fromBlockNumber + numberOfBlocks)
                {
                    BlockHeader? block = _blockTree.SearchForHeader(new BlockParameter(number)).Object;

                    if (block == null)
                        break;

                    sw.WriteLine($"{block.Number},{block.Timestamp},{block.GasUsed},{block.GasLimit},{block.BaseFeePerGas}");
                    number++;
                }
            }

            return ResultWrapper<string>.Success($"Successfully wrote data to {path} from block {fromBlockNumber} to {number}");
        }

        public ResultWrapper<UInt256[]> basefee_fetchData(long fromBlockNumber, long numberOfBlocks)
        {
            if(fromBlockNumber < 12965000)
                return ResultWrapper<UInt256[]>.Fail($"No base fee data before London hard fork (Block 12,965,000)");
            
            UInt256[] baseFees = new UInt256[numberOfBlocks];

            for (int i = 0; i < numberOfBlocks; i++)
            {
                BlockHeader? block = _blockTree.SearchForHeader(new BlockParameter(fromBlockNumber + i)).Object;

                if (block == null)
                    break;
                baseFees[i] = block.BaseFeePerGas;
            }

            return ResultWrapper<UInt256[]>.Success(baseFees);
        }
    }
}
