global using MediatR;
global using AutoMapper;
global using OfficeOpenXml;
global using Newtonsoft.Json;
global using LicenseContext = OfficeOpenXml.LicenseContext;
global using Azure.Storage.Blobs;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using System.Runtime.Serialization;
global using Microsoft.AspNetCore.SignalR;


global using WembleyScada.Api.Application.Mapping;
global using WembleyScada.Api.Application.Dtos;
global using WembleyScada.Api.Application.Messages;
global using WembleyScada.Api.Application.Hubs;
global using WembleyScada.Api.Application.Messages.ErrorDetails;
global using WembleyScada.Api.Application.Commands.Employees.Create;
global using WembleyScada.Api.Application.Commands.Employees.CreateWorkRecord;

global using WembleyScada.Api.Application.Commands.Employees.CreateEmployee;
global using WembleyScada.Api.Application.Commands.Employees.DeleteEmployee;
global using WembleyScada.Api.Application.Commands.Employees.UpdateWorkRecord;
global using WembleyScada.Api.Application.Queries.Stations;
global using WembleyScada.Api.Application.Queries.Employees;
global using WembleyScada.Api.Application.Queries.References;
global using WembleyScada.Api.Application.Queries.Lines;
global using WembleyScada.Api.Application.Queries.Products;
global using WembleyScada.Api.Application.Queries.References.Parameters.ViewModels;
global using WembleyScada.Api.Application.Queries.StationReferences;
global using WembleyScada.Api.Application.Queries.ErrorInformations;
global using WembleyScada.Api.Application.Queries.MachineStatuses;
global using WembleyScada.Api.Application.Queries.ShiftReports;
global using WembleyScada.Api.Application.Queries.References.Parameters;
global using WembleyScada.Api.Application.Queries.ShiftReports.Details;
global using WembleyScada.Api.Application.Queries.ShiftReports.Download;
global using WembleyScada.Api.Application.Queries.ShiftReports.Lastest;
global using WembleyScada.Api.Application.Queries.ShiftReports.ShortenDetail;
global using WembleyScada.Api.Application.Exceptions;
global using WembleyScada.Api.Application.Commands.DeviceReferences;


global using WembleyScada.Domain.SeedWork;
global using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;
global using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
global using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
global using WembleyScada.Domain.AggregateModels.StationReferenceAggregate;
global using WembleyScada.Domain.AggregateModels.StationAggregate;
global using WembleyScada.Domain.AggregateModels.LineAggregate;
global using WembleyScada.Domain.AggregateModels.ProductAggregate;
global using WembleyScada.Domain.AggregateModels.EmployeeAggregate;
global using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

global using WembleyScada.Infrastructure;
global using WembleyScada.Infrastructure.Repositories;
global using WembleyScada.Infrastructure.Communication;


global using Buffer = WembleyScada.Api.Workers.Buffer;


