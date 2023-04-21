using Microsoft.Extensions.Options;
using SignatureEvaluatorService.Interfaces;
using SignatureModels.Models;
using System.Net.WebSockets;

namespace SignatureEvaluatorService
{
    public class SignatureEvaluatorService : ISignatureEvaluatorService
    {
        private Roles? Roles { get; set; }
        private Resources? Resources { get; set; }
        public SignatureEvaluatorService(IOptions<Roles> _roles, IOptions<Resources> _resources)
        {
            Roles = _roles.Value;
            Resources = _resources.Value;
        }
        public SignatureResponse EvaluateSignature(SignatureRequest _signatures)
        {
            var response = new SignatureResponse(_signatures.plaintiff, _signatures.defendant);
            EvaluateWinner(ref response);
            return response;
        }

        public SignatureRequirementResponse EvaluateSignatureRequirement(SignatureRequest _signatures)
        {
            var response = new SignatureRequirementResponse(_signatures.plaintiff, _signatures.defendant);
            EvaluateRequirement(ref response);
            return response;
        }

        private void EvaluateRequirement(ref SignatureRequirementResponse signatureCase)
        {
            var plaintiffScore = 0;
            var defendantScore = 0;
            var plaintiff = signatureCase?.GetImplicatedParts()?.GetPlaintiff()?.GetSign();
            var defendant = signatureCase?.GetImplicatedParts()?.GetDefendant()?.GetSign();
            foreach (var splitted in plaintiff)
            {
                plaintiffScore += CaseRole(splitted);
            }
            foreach (var splitted in defendant)
            {
                defendantScore += CaseRole(splitted);
            }
            signatureCase?.GetImplicatedParts().SetPlaintiffRating(plaintiffScore);
            signatureCase?.GetImplicatedParts().SetDefendantRating(defendantScore);
            var result = Resources.Errors.MissingHashtag;
            if (plaintiff.ToLower().Contains('#'))
            {
                result = CheckDiference(plaintiffScore, defendantScore);
            }
            else if (defendant.ToLower().Contains('#'))
            {
                result = CheckDiference(defendantScore, plaintiffScore);
            }
            signatureCase?.SetRequirements(result);
        }

        private string CheckDiference(int lesser, int higher)
        {
            var diferenceResult = Resources.Errors.BeginResponseRequirements;
            var diference = higher - lesser;

            if (diference < 0)
                return Resources.Errors.HigherSignature;

            var divisionAndModule = GetDivisionAndModule(diference, Roles.King.Score);
            ConcatCorrectSign(divisionAndModule.GetValueOrDefault("Loop"), Roles.King.Abbreviation, ref diferenceResult);
            if (divisionAndModule.GetValueOrDefault("Reminder") >= 0)
            {
                divisionAndModule = GetDivisionAndModule(divisionAndModule.GetValueOrDefault("Reminder"), Roles.Notary.Score);
                ConcatCorrectSign(divisionAndModule.GetValueOrDefault("Loop"), Roles.Notary.Abbreviation, ref diferenceResult);
                if (divisionAndModule.GetValueOrDefault("Reminder") == 0)
                {
                    ConcatCorrectSign(1, Roles.Validator.Abbreviation, ref diferenceResult);
                }
                else
                {
                    ConcatCorrectSign(divisionAndModule.GetValueOrDefault("Reminder"), Roles.Validator.Abbreviation, ref diferenceResult);
                }
            }
            return diferenceResult;
        }

        private Dictionary<string, int> GetDivisionAndModule(int quotient, int divisor)
        {
            return new Dictionary<string, int>() {
                { "Loop",(quotient / divisor) },
                { "Reminder",(quotient % divisor) }
            };
        }

        private void ConcatCorrectSign(int loop, char sign, ref string diferenceResult)
        {
            if (loop > 0)
            {
                diferenceResult += String.Concat(Enumerable.Repeat(sign, loop));
            }
        }

        private void EvaluateWinner(ref SignatureResponse signatureCase)
        {
            var plaintiffScore = 0;
            var defendantScore = 0;
            var plaintiff = signatureCase?.GetImplicatedParts()?.GetPlaintiff()?.GetSign();
            var defendant = signatureCase?.GetImplicatedParts()?.GetDefendant()?.GetSign();
            foreach (var splitted in plaintiff)
            {
                plaintiffScore += CaseRole(splitted);
            }
            foreach (var splitted in defendant)
            {
                defendantScore += CaseRole(splitted);
            }
            var winner = Resources.Winners.Draw;
            if (plaintiffScore > defendantScore)
            {
                winner = Resources.Winners.Plaintiff;
            }
            else if (plaintiffScore < defendantScore)
            {
                winner = Resources.Winners.Defendant;
            }
            signatureCase?.GetImplicatedParts().SetPlaintiffRating(plaintiffScore);
            signatureCase?.GetImplicatedParts().SetDefendantRating(defendantScore);
            signatureCase?.SetWinner(winner);
        }

        private int CaseRole(char role)
        {
            var result = 0;
            if (role == Roles.King.Abbreviation)
            {
                result = Roles.King.Score;
            }
            else if (role == Roles.Notary.Abbreviation)
            {
                result = Roles.Notary.Score;
            }
            else if (role == Roles.Validator.Abbreviation)
            {
                result = Roles.Validator.Score;
            }

            return result;
        }
    }
}