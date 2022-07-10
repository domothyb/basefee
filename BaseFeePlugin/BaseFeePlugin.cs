using System.Threading.Tasks;
using Nethermind.Api;
using Nethermind.Api.Extensions;
using Nethermind.JsonRpc.Modules;

namespace BaseFeePlugin
{
    public class BaseFeePlugin : INethermindPlugin
    {
        public string Name => "Base fee plugin";
        public string Description => "Extract historical base fee (and gas usage) from block headers via JSON RPC";
        public string Author => "github.com/domothyb";

        private INethermindApi? _api;
        
        public Task Init(INethermindApi api)
        {
            _api = api;
            return Task.CompletedTask;
        }

        public Task InitNetworkProtocol()
        {
            return Task.CompletedTask;
        }

        public Task InitRpcModules()
        {
            var (getFromAPi, _) = _api!.ForRpc;
            
            BaseFeeRpcModule baseFeeRpcModule = new(getFromAPi.BlockTree!, getFromAPi.LogManager);
            getFromAPi.RpcModuleProvider?.Register(new SingletonModulePool<IBaseFeeRpcModule>(baseFeeRpcModule));
            return Task.CompletedTask;
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}
