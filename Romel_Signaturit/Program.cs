using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignatureEvaluatorService.Interfaces;
using SignatureModels.Models;
using System.Security.Cryptography.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Roles>(builder.Configuration.GetSection("Roles"));
builder.Services.Configure<Resources>(builder.Configuration.GetSection("Resources"));
// todo
builder.Services.AddSingleton<ISignatureEvaluatorService, SignatureEvaluatorService.SignatureEvaluatorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/EvaluateSignature/{plaintiff}/{defendant}", (ISignatureEvaluatorService Evaluator, string plaintiff, string defendant) =>
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