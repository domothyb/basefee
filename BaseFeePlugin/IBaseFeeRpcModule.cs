using Nethermind.Int256;
using Nethermind.JsonRpc;
using Nethermind.JsonRpc.Modules;

namespace BaseFeePlugin
{
    [RpcModule("BaseFee")]
    public interface IBaseFeeRpcModule : IRpcModule
    {
        [JsonRpcMethod(Description = "Retrieves base fee and gas data from block headers of specified range.")]
        ResultWrapper<string> basefee_writeDataToFile(string path, long fromBlockNumber, long numberOfBlocks);
        
        [JsonRpcMethod(Description = "Retrieves base fee from block headers of specified range.")]
        ResultWrapper<UInt256[]> basefee_fetchData(long fromBlockNumber, long numberOfBlocks);
    }
}
