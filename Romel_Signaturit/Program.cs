using SignatureEvaluator;
using SignatureEvaluator.Interfaces;
using SignatureModels.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Roles>(builder.Configuration.GetSection("Roles"));
builder.Services.Configure<Resources>(builder.Configuration.GetSection("Resources"));
// todo
builder.Services.AddSingleton<ISignatureEvaluatorService, SignatureEvaluatorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/EvaluateSignature/{plaintiff}/{defendant}",(ISignatureEvaluatorService Evaluator, string plaintiff, string defendant) =>
{
    return Evaluator.EvaluateSignature(new SignatureRequest(plaintiff, defendant));
})
.Produces<SignatureResponse>()
.WithName("EvaluateSignature")
.WithOpenApi();

app.MapGet("/SignatureRequirementResponse/{plaintiff}/{defendant}", (ISignatureEvaluatorService Evaluator, string plaintiff, string defendant) =>
{
    return Evaluator.EvaluateSignatureRequirement(new SignatureRequest(plaintiff, defendant));
})
.Produces<SignatureRequirementResponse>()
.WithName("SignatureRequirementResponse")
.WithOpenApi();

app.Run();