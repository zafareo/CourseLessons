using Grpc.Core;
using Server.Protos;

namespace DropQovunService.Services
{
    public class QovunService : Qovun.QovunBase
    {
        public override Task<QovunResponse> DropQovun(QovunRequest request, ServerCallContext context)
        {
            return Task.FromResult(new QovunResponse
            {
                IsQovuned = true,
                ProjectName = "ssv",
                QovunedSection = "controller",
                Qovuner = "Zafarello"
            });
        }
    }
}
