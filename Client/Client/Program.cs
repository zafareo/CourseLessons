using Grpc.Net.Client;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var conn = GrpcChannel.ForAddress("http://10.10.1.143:5296");
            var client = new SogliqniSaqlashVazirligi.SogliqniSaqlashVazirligiClient(conn);

            SSVRequest request = new()
            {
                Id = "123456789",
                Name = "Boltavoy"
            };

            SSVResponse repo = client.AboutSSV(request);

        }
    }
}