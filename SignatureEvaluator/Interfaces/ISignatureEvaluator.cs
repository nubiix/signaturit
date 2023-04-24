using SignatureModels.Models;

namespace SignatureEvaluator.Interfaces
{
    public interface ISignatureEvaluatorService
    {
        public SignatureResponse EvaluateSignature(SignatureRequest _signatures);
        public SignatureRequirementResponse EvaluateSignatureRequirement(SignatureRequest _signatures);
    }
}
