using Newtonsoft.Json;

namespace AVM.ClientGenerator.ABI.ARC32
{
    [JsonConverter(typeof(DefaultArgumentConverter))]
    public abstract class DefaultArgumentSpec
    {
        public ArgumentDefaultSource Source { get; set; }
        public abstract string Summary { get; }
    }

    public class AbiMethodDefaultArgument : DefaultArgumentSpec
    {
        public ARC4.MethodDescription Data { get; set; }

        public override string Summary => $"Default value must be sourced from ABI method {Data.ARC4MethodSignature}";
    }

    public class StateDefaultArgument : DefaultArgumentSpec
    {
        public string Data { get; set; }

        public override string Summary => $"Default value must be sourced from {Source} using key {Data}";
    }

    public class ConstantDefaultArgument : DefaultArgumentSpec
    {
        public object Data { get; set; }

        public override string Summary => $"Default value must be the constant '{Data}'";
    }
}
